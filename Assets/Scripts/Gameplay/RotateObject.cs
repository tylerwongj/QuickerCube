using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationSpeed = new Vector3(0, 90, 0);
    public bool randomizeSpeed = true;
    
    void Start()
    {
        if (randomizeSpeed)
        {
            rotationSpeed.y = Random.Range(60f, 120f);
        }
    }
    
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}