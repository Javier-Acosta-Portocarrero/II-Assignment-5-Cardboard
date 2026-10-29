using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class FirstExercisePlayerMovement : MonoBehaviour
{
    public Transform playerCamera;
    public float speed = 3f;
    public float mouseSensitivity = 0.15f;
    public float touchSensitivity = 0.12f;
    public float controllerSensitivity = 120f;
    public float gravity = -9.81f;

    private CharacterController characterController;
    private float verticalSpeed = 0;
    private float cameraRotation = 0;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        if (playerCamera != null)
        {
            cameraRotation = playerCamera.localEulerAngles.x;
            if (cameraRotation > 180f)
                cameraRotation -= 360f;
        }
    }

    void Update()
    {
        if (playerCamera == null || characterController == null)
            return;

        bool moveForward = false;
        Vector2 lookMovement = Vector2.zero;

#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null)
            moveForward = Keyboard.current.spaceKey.isPressed;

        if (Mouse.current != null && Mouse.current.rightButton.isPressed)
            lookMovement += Mouse.current.delta.ReadValue() * mouseSensitivity;

        if (Gamepad.current != null)
        {
            moveForward |= Gamepad.current.buttonSouth.isPressed;
            lookMovement += Gamepad.current.rightStick.ReadValue() * controllerSensitivity * Time.deltaTime;
        }

        if (Touchscreen.current != null)
        {
            foreach (var touch in Touchscreen.current.touches)
            {
                if (!touch.press.isPressed)
                    continue;

                if (touch.startPosition.ReadValue().x < Screen.width / 2f)
                    moveForward = true;
                else
                    lookMovement += touch.delta.ReadValue() * touchSensitivity;
            }
        }

#elif ENABLE_LEGACY_INPUT_MANAGER
        moveForward = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton0);

        if (Input.GetMouseButton(1))
        {
            lookMovement = new Vector2(
                Input.GetAxis("Mouse X"),
                Input.GetAxis("Mouse Y")
            ) * 3f;
        }

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                continue;

            if (touch.position.x < Screen.width / 2f)
                moveForward = true;
            else
                lookMovement += touch.deltaPosition * touchSensitivity;
        }
#endif

        //RotatePlayer(lookMovement);
        MovePlayer(moveForward);
    }

    void RotatePlayer(Vector2 lookMovement)
    {
        transform.Rotate(0, lookMovement.x, 0);

        cameraRotation -= lookMovement.y;
        cameraRotation = Mathf.Clamp(cameraRotation, -80f, 80f);
        playerCamera.localRotation = Quaternion.Euler(cameraRotation, 0, 0);
    }

    void MovePlayer(bool moveForward)
    {
        Vector3 direction = playerCamera.forward;
        direction.y = 0;
        direction.Normalize();

        Vector3 movement = Vector3.zero;
        if (moveForward)
            movement = direction * speed;

        if (characterController.isGrounded && verticalSpeed < 0)
            verticalSpeed = -2f;

        verticalSpeed += gravity * Time.deltaTime;
        movement.y = verticalSpeed;

        characterController.Move(movement * Time.deltaTime);
    }
}
