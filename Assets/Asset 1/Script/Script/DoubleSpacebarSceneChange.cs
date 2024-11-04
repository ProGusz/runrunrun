using UnityEngine;
using UnityEngine.SceneManagement;

public class DoubleSpacebarSceneChange : MonoBehaviour
{
    public string targetSceneName = "SampleScene";
    public float doublePressTime = 0.5f; // Time window for double press

    private float lastPressTime;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float timeSinceLastPress = Time.time - lastPressTime;

            if (timeSinceLastPress <= doublePressTime)
            {
                // Double press detected
                LoadTargetScene();
            }

            lastPressTime = Time.time;
        }
    }

    void LoadTargetScene()
    {
        Debug.Log("Double spacebar press detected. Loading scene: " + targetSceneName);
        SceneManager.LoadScene(targetSceneName);
    }
}