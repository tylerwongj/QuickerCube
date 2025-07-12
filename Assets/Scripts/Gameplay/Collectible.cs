using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Collectible Settings")]
    public int pointValue = 10;
    public AudioClip collectSound;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectItem(other.gameObject);
        }
    }
    
    void CollectItem(GameObject player)
    {
        // Add to score
        SimpleScoreSystem.AddScore(pointValue);
        
        // Play sound effect
        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }
        
        // Visual feedback
        Debug.Log($"Collected item worth {pointValue} points!");
        
        // Destroy the collectible
        Destroy(gameObject);
    }
    
    void Start()
    {
        // Make it a trigger so player can pass through
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = true;
        }
    }
}