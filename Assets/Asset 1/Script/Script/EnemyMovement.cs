using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f; // Public variable for enemy speed

    void Update()
    {
        // Move the enemy to the right
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}