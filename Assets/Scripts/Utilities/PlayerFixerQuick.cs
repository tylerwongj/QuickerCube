using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFixerQuick : MonoBehaviour
{
    void Start()
    {
        FixPlayerInput();
    }
    
    [ContextMenu("Fix Player Input Now")]
    public void FixPlayerInput()
    {
        // Find all objects with CubeMovement
        CubeMovement[] players = FindObjectsByType<CubeMovement>(FindObjectsSortMode.None);
        
        foreach (CubeMovement player in players)
        {
            Debug.Log($"Checking player: {player.gameObject.name}");
            
            // Check if PlayerInput exists
            PlayerInput playerInput = player.GetComponent<PlayerInput>();
            if (playerInput == null)
            {
                Debug.Log("Adding PlayerInput component");
                playerInput = player.gameObject.AddComponent<PlayerInput>();
            }
            
            // Find and assign InputSystem_Actions
            if (playerInput.actions == null)
            {
                InputActionAsset inputActions = FindInputActions();
                if (inputActions != null)
                {
                    playerInput.actions = inputActions;
                    playerInput.notificationBehavior = PlayerNotifications.SendMessages;
                    Debug.Log($"✅ Fixed input for {player.gameObject.name}");
                }
                else
                {
                    Debug.LogError("❌ InputSystem_Actions not found!");
                }
            }
            else
            {
                Debug.Log($"✅ Input already configured for {player.gameObject.name}");
            }
        }
    }
    
    InputActionAsset FindInputActions()
    {
        // Find all InputActionAssets in the project
        InputActionAsset[] allActions = Resources.FindObjectsOfTypeAll<InputActionAsset>();
        
        foreach (InputActionAsset actions in allActions)
        {
            if (actions.name.Contains("InputSystem"))
            {
                Debug.Log($"Found InputSystem_Actions: {actions.name}");
                return actions;
            }
        }
        
        if (allActions.Length > 0)
        {
            Debug.Log($"Using first available InputActionAsset: {allActions[0].name}");
            return allActions[0];
        }
        
        return null;
    }
}