using UnityEngine;

public class SimpleMovementTest : MonoBehaviour
{
    public float testSpeed = 5f;
    
    void Update()
    {
        // Basic movement test using old Input system
        Vector3 movement = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            movement.z = 1f;
            Debug.Log("W pressed - moving forward");
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.z = -1f;
            Debug.Log("S pressed - moving backward");
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement.x = -1f;
            Debug.Log("A pressed - moving left");
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x = 1f;
            Debug.Log("D pressed - moving right");
        }
        
        if (movement != Vector3.zero)
        {
            transform.position += movement * testSpeed * Time.deltaTime;
            Debug.Log($"Moving to: {transform.position}");
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed!");
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
            }
        }
    }
}