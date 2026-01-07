using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadDemoScene()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene("Demo");
    }
}