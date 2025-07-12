using UnityEngine;
using UnityEngine.InputSystem;

public class MovementDebugger : MonoBehaviour
{
    void Start()
    {
        Debug.Log("=== MOVEMENT DEBUGGER STARTED ===");
        CheckPlayerSetup();
    }
    
    void Update()
    {
        // Test basic input detection
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W key detected (old Input system)");
        }
        
        // Check if any keys are being pressed
        if (Input.anyKeyDown)
        {
            Debug.Log($"Some key pressed: {Input.inputString}");
        }
    }
    
    [ContextMenu("Check Player Setup")]
    public void CheckPlayerSetup()
    {
        Debug.Log("=== CHECKING PLAYER SETUP ===");
        
        // Find all CubeMovement objects
        CubeMovement[] cubeMovements = FindObjectsByType<CubeMovement>(FindObjectsSortMode.None);
        
        Debug.Log($"Found {cubeMovements.Length} CubeMovement objects");
        
        foreach (CubeMovement cube in cubeMovements)
        {
            GameObject player = cube.gameObject;
            Debug.Log($"\nPlayer: {player.name}");
            Debug.Log($"  - Position: {player.transform.position}");
            Debug.Log($"  - Active: {player.activeInHierarchy}");
            Debug.Log($"  - Has CubeMovement: ✅");
            
            // Check components
            PlayerInput playerInput = player.GetComponent<PlayerInput>();
            Debug.Log($"  - Has PlayerInput: {(playerInput != null ? "✅" : "❌")}");
            
            if (playerInput != null)
            {
                Debug.Log($"    - Actions assigned: {(playerInput.actions != null ? "✅" : "❌")}");
                Debug.Log($"    - Behavior: {playerInput.notificationBehavior}");
                Debug.Log($"    - Enabled: {playerInput.enabled}");
                
                if (playerInput.actions != null)
                {
                    Debug.Log($"    - Actions name: {playerInput.actions.name}");
                }
            }
            
            Rigidbody rb = player.GetComponent<Rigidbody>();
            Debug.Log($"  - Has Rigidbody: {(rb != null ? "✅" : "❌")}");
            
            if (rb != null)
            {
                Debug.Log($"    - Mass: {rb.mass}");
                Debug.Log($"    - Use Gravity: {rb.useGravity}");
                Debug.Log($"    - Is Kinematic: {rb.isKinematic}");
            }
        }
        
        Debug.Log("=== END PLAYER CHECK ===");
    }
    
    [ContextMenu("Add Simple Movement Test")]
    public void AddSimpleMovementTest()
    {
        CubeMovement[] cubeMovements = FindObjectsByType<CubeMovement>(FindObjectsSortMode.None);
        
        foreach (CubeMovement cube in cubeMovements)
        {
            SimpleMovementTest test = cube.GetComponent<SimpleMovementTest>();
            if (test == null)
            {
                cube.gameObject.AddComponent<SimpleMovementTest>();
                Debug.Log($"Added SimpleMovementTest to {cube.gameObject.name}");
            }
        }
    }
}