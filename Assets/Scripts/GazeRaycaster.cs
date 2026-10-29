using UnityEngine;

public class GazeRaycaster : MonoBehaviour
{
    public float rayDistance = 50f;

    private ObjectController currentActivator;
    private CollectibleController currentCollectible;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ObjectController newActivator = null;
        CollectibleController newCollectible = null;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance))
        {
            newActivator = hit.collider.GetComponentInParent<ObjectController>();
            newCollectible = hit.collider.GetComponentInParent<CollectibleController>();
        }
        if (newActivator != currentActivator)
        {
            currentActivator = newActivator;
            if (currentActivator != null)
                currentActivator.OnPointerEnter();
        }

        if (newCollectible != currentCollectible)
        {
            if (currentCollectible != null)
                currentCollectible.OnPointerExit();

            currentCollectible = newCollectible;
            if (currentCollectible != null)
                currentCollectible.OnPointerEnter();
        }
    }
}
