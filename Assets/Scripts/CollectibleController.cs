using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    public Transform player;
    public Transform playerCamera;
    public string totemTag = "totem";
    public float speed = 2f;
    public float acceleration = 8f;
    public float stoppingDistance = 3.0f;
    public float collectionDistance = 2.5f;

    private ObjectController totemController;
    private Rigidbody rb;
    private bool moving = false;
    private bool gazedAt = false;
    private bool collected = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject totem = GameObject.FindGameObjectWithTag(totemTag);
        totemController = totem.GetComponent<ObjectController>();
        totemController.OnObjectLookedAt += StartMoving;
    }

    // Update is called once per frame
    void Update()
    {
        if (!collected && gazedAt && playerCamera != null && Vector3.Distance(playerCamera.position, transform.position) <= collectionDistance)
        {
            Collect();
        }
    }

    void FixedUpdate()
    {
        if (rb == null)
            return;

        Vector3 desiredVelocity = Vector3.zero;
        if (moving && !collected && playerCamera != null)
        {
            Vector3 target = playerCamera.position;
            target.y = rb.position.y;
            Vector3 direction = target - rb.position;
            float distance = direction.magnitude;

            if (distance > stoppingDistance)
            {
                float desiredSpeed = Mathf.Min(speed, (distance - stoppingDistance) * 2f);
                desiredVelocity = direction.normalized * desiredSpeed;
            }
            else
            {
                moving = false;
                return;
            }  
        }
        Vector3 currentVelocity = rb.linearVelocity;
        currentVelocity.y = 0;

        rb.AddForce((desiredVelocity - currentVelocity) * acceleration, ForceMode.Acceleration);
    }

    void StartMoving()
    {
        moving = true;
    }

    public void OnPointerEnter()
    {
        gazedAt = true;
    }

    public void OnPointerExit()
    {
        gazedAt = false;
    }

    private void Collect()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material material = renderer.material;
            Color color = material.color;
            color.a = 0.2f;
            material.color = color;
        }

        moving = false;
        collected = true;
    }
}
