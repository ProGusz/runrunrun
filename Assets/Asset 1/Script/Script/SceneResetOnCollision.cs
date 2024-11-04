using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneResetOnCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ResetScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            ResetScene();
        }
    }

    private void ResetScene()
    {
        Debug.Log("Player hit! Resetting scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}