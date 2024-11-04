using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporter : MonoBehaviour
{
    public string targetSceneName = "video";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TeleportToScene();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TeleportToScene();
        }
    }

    private void TeleportToScene()
    {
        Debug.Log("Teleporting to scene: " + targetSceneName);
        SceneManager.LoadScene(targetSceneName);
    }
}