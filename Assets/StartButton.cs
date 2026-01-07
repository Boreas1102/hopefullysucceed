using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadDemoScene()
    {
        // 1. 关键：确保加载前时间流速是正常的
        Time.timeScale = 1f;
        
        // 2. 异步加载场景（更稳定）
        SceneManager.LoadScene("Demo");
    }
}