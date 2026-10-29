
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class FirstExercisePlayerMovement : MonoBehaviour
{
    public Transform playerCamera;
    public float speed = 3f;
    public float gravity = -9.81f;

    private Rigidbody rb;
    private bool mobileMovement = false;
    private float verticalSpeed = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCamera == null)
            return;

        bool moveForward = false;

#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null)
            moveForward = Keyboard.current.spaceKey.isPressed;

        if (Gamepad.current != null)
            moveForward |= Gamepad.current.buttonSouth.isPressed;

        if (Touchscreen.current != null)
        {
            foreach (var touch in Touchscreen.current.touches)
            {
                if (touch.press.isPressed)
                {
                    moveForward = true;
                }
            }
        }

#elif ENABLE_LEGACY_INPUT_MANAGER
        moveForward = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton0);

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                mobileMovement = !mobileMovement;
                break;
            }
        }
#endif
        moveForward |= mobileMovement;
        MovePlayer(moveForward);
    }

    void MovePlayer(bool moveForward)
    {
        if (!moveForward)
            return;

        Vector3 direction = playerCamera.forward;
        direction.y = 0;
        direction.Normalize();

        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
}
