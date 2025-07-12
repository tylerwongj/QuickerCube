using UnityEngine;

public class SimpleScoreSystem : MonoBehaviour
{
    [Header("Score Settings")]
    public int currentScore = 0;
    public int totalCollectibles = 0;
    
    private static SimpleScoreSystem instance;
    
    void Awake()
    {
        // Singleton pattern - only one score system
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    
    void Start()
    {
        // Count total collectibles in scene
        totalCollectibles = FindObjectsOfType<Collectible>().Length;
        Debug.Log($"Total collectibles in level: {totalCollectibles}");
    }
    
    public static void AddScore(int points)
    {
        if (instance != null)
        {
            instance.currentScore += points;
            instance.CheckWinCondition();
        }
    }
    
    public static int GetScore()
    {
        return instance != null ? instance.currentScore : 0;
    }
    
    public static int GetTotalCollectibles()
    {
        return instance != null ? instance.totalCollectibles : 0;
    }
    
    void CheckWinCondition()
    {
        int remainingCollectibles = FindObjectsOfType<Collectible>().Length;
        
        if (remainingCollectibles == 0)
        {
            Debug.Log($"🎉 YOU WIN! Final Score: {currentScore}");
            Debug.Log("All collectibles collected!");
        }
    }
    
    void OnGUI()
    {
        // Simple on-screen score display
        GUI.skin.label.fontSize = 20;
        GUI.skin.label.normal.textColor = Color.white;
        
        // Score
        GUI.Label(new Rect(10, 10, 200, 30), $"Score: {currentScore}");
        
        // Progress
        int remaining = FindObjectsOfType<Collectible>().Length;
        int collected = totalCollectibles - remaining;
        GUI.Label(new Rect(10, 40, 250, 30), $"Collected: {collected}/{totalCollectibles}");
        
        // Win message
        if (remaining == 0 && totalCollectibles > 0)
        {
            GUI.skin.label.fontSize = 30;
            GUI.skin.label.normal.textColor = Color.yellow;
            GUI.Label(new Rect(Screen.width/2 - 100, Screen.height/2 - 50, 200, 100), "YOU WIN!");
        }
    }
}