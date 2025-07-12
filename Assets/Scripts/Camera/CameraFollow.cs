using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -10);
    public float followSpeed = 5f;
    public float rotationSpeed = 2f;
    
    [Header("Look Settings")]
    public bool lookAtTarget = true;
    public float lookSmoothing = 3f;
    
    void Start()
    {
        // If no target assigned, try to find player
        if (target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
                target = player.transform;
        }
        
        // Set initial position
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
    
    void LateUpdate()
    {
        if (target == null) return;
        
        // Follow target position
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        
        // Look at target
        if (lookAtTarget)
        {
            Vector3 direction = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, lookSmoothing * Time.deltaTime);
        }
    }
    
    // Call this to set a new target
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}