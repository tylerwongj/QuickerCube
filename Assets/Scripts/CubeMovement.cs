using UnityEngine;
using UnityEngine.InputSystem;

public class CubeMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    
    private Rigidbody rb;
    private bool isGrounded = true;
    private Vector2 moveInput;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
    }
    
    void Update()
    {
        HandleMovement();
    }
    
    void HandleMovement()
    {
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed * Time.deltaTime;
        transform.position += movement;
    }
    
    // These functions are called automatically by the Input System
    public void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
        Debug.Log($"Move input: {moveInput}");
    }
    
    public void OnJump(InputValue inputValue)
    {
        if (inputValue.isPressed && isGrounded)
        {
            Debug.Log("Jump input received!");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.5f || collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    
    void OnCollisionStay(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.5f || collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}