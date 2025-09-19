using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string sceneName;
    public int sceneIndex;
    public void StartScene()
    {
        if (sceneName != "" && sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
