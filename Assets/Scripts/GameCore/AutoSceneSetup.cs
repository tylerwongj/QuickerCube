using UnityEngine;

public class AutoSceneSetup : MonoBehaviour
{
    [Header("Level Generation")]
    public int levelSize = 50;
    public int collectibleCount = 25;
    
    [Header("Materials (Optional - will create colored materials if empty)")]
    public Material groundMaterial;
    public Material collectibleMaterial;
    public Material playerMaterial;
    public Material wallMaterial;
    public Material platformMaterial;
    
    [Header("Input System")]
    public UnityEngine.InputSystem.InputActionAsset inputActions;
    
    private GameObject player;
    
    void Start()
    {
        SetupLevel();
        SetupPlayer();
        SetupCamera();
        SpawnCollectibles();
        SetupScoreSystem();
    }
    
    void SetupLevel()
    {
        // Create ground plane
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ground.name = "Ground";
        ground.transform.position = new Vector3(0, -1, 0);
        ground.transform.localScale = new Vector3(levelSize, 1, levelSize);
        ground.tag = "Ground";
        
        // Apply ground material
        Renderer groundRenderer = ground.GetComponent<Renderer>();
        if (groundMaterial != null)
            groundRenderer.material = groundMaterial;
        else
            groundRenderer.material.color = Color.green;
        
        // Create some walls
        CreateWall(new Vector3(levelSize/2, 2, 0), new Vector3(1, 4, levelSize));
        CreateWall(new Vector3(-levelSize/2, 2, 0), new Vector3(1, 4, levelSize));
        CreateWall(new Vector3(0, 2, levelSize/2), new Vector3(levelSize, 4, 1));
        CreateWall(new Vector3(0, 2, -levelSize/2), new Vector3(levelSize, 4, 1));
        
        // Create more platforms for bigger level
        CreatePlatform(new Vector3(10, 2, 10), new Vector3(4, 0.5f, 4));
        CreatePlatform(new Vector3(-15, 3, -15), new Vector3(5, 0.5f, 5));
        CreatePlatform(new Vector3(20, 1.5f, -8), new Vector3(3, 0.5f, 3));
        CreatePlatform(new Vector3(-8, 4, 18), new Vector3(3, 0.5f, 3));
        CreatePlatform(new Vector3(15, 2.5f, 12), new Vector3(4, 0.5f, 4));
        CreatePlatform(new Vector3(-20, 2, 5), new Vector3(3, 0.5f, 3));
        CreatePlatform(new Vector3(0, 3, -20), new Vector3(6, 0.5f, 6));
        CreatePlatform(new Vector3(-12, 1.5f, -8), new Vector3(3, 0.5f, 3));
    }
    
    void SetupPlayer()
    {
        // Find existing cube or create one
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            player = GameObject.CreatePrimitive(PrimitiveType.Cube);
            player.name = "PlayerCube";
            player.tag = "Player";
            
            // Add Rigidbody
            Rigidbody rb = player.AddComponent<Rigidbody>();
            rb.mass = 1f;
            
            // Add movement script
            player.AddComponent<CubeMovement>();
            
            // Add Player Input and configure it
            var playerInput = player.AddComponent<UnityEngine.InputSystem.PlayerInput>();
            
            // Try to find InputSystem_Actions if not assigned
            if (inputActions == null)
            {
                inputActions = Resources.Load<UnityEngine.InputSystem.InputActionAsset>("InputSystem_Actions");
                if (inputActions == null)
                {
                    // Try to find it in Assets folder
                    var foundActions = Resources.FindObjectsOfTypeAll<UnityEngine.InputSystem.InputActionAsset>();
                    if (foundActions.Length > 0)
                    {
                        inputActions = foundActions[0];
                    }
                }
            }
            
            // Assign the input actions
            if (inputActions != null)
            {
                playerInput.actions = inputActions;
                playerInput.notificationBehavior = UnityEngine.InputSystem.PlayerNotifications.SendMessages;
                Debug.Log("Input System configured automatically!");
            }
            else
            {
                Debug.LogWarning("InputSystem_Actions asset not found! Please assign it manually in the AutoSceneSetup component.");
            }
        }
        
        // Position player
        player.transform.position = new Vector3(0, 2, 0);
        
        // Apply player material
        Renderer playerRenderer = player.GetComponent<Renderer>();
        if (playerMaterial != null)
            playerRenderer.material = playerMaterial;
        else
            playerRenderer.material.color = Color.blue;
    }
    
    void SetupCamera()
    {
        // Find main camera
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            // Add camera follow script
            CameraFollow cameraFollow = mainCamera.GetComponent<CameraFollow>();
            if (cameraFollow == null)
            {
                cameraFollow = mainCamera.gameObject.AddComponent<CameraFollow>();
            }
            cameraFollow.target = player.transform;
        }
    }
    
    void SpawnCollectibles()
    {
        for (int i = 0; i < collectibleCount; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(-levelSize/2 + 2, levelSize/2 - 2),
                2f,
                Random.Range(-levelSize/2 + 2, levelSize/2 - 2)
            );
            
            GameObject collectible = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            collectible.name = "Collectible";
            collectible.tag = "Collectible";
            collectible.transform.position = randomPos;
            collectible.transform.localScale = Vector3.one * 0.5f;
            
            // Make collectibles yellow
            Renderer collectibleRenderer = collectible.GetComponent<Renderer>();
            if (collectibleMaterial != null)
                collectibleRenderer.material = collectibleMaterial;
            else
                collectibleRenderer.material.color = Color.yellow;
            
            // Add collectible behavior
            collectible.AddComponent<Collectible>();
            
            // Add rotation animation
            collectible.AddComponent<RotateObject>();
        }
    }
    
    void CreateWall(Vector3 position, Vector3 scale)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.name = "Wall";
        wall.transform.position = position;
        wall.transform.localScale = scale;
        
        Renderer wallRenderer = wall.GetComponent<Renderer>();
        if (wallMaterial != null)
            wallRenderer.material = wallMaterial;
        else
            wallRenderer.material.color = Color.gray;
    }
    
    void SetupScoreSystem()
    {
        // Add score system to this GameObject
        if (GetComponent<SimpleScoreSystem>() == null)
        {
            gameObject.AddComponent<SimpleScoreSystem>();
            Debug.Log("Score system added!");
        }
    }
    
    void CreatePlatform(Vector3 position, Vector3 scale)
    {
        GameObject platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
        platform.name = "Platform";
        platform.transform.position = position;
        platform.transform.localScale = scale;
        platform.tag = "Ground";
        
        Renderer platformRenderer = platform.GetComponent<Renderer>();
        if (platformMaterial != null)
            platformRenderer.material = platformMaterial;
        else
            platformRenderer.material.color = Color.cyan;
    }
}