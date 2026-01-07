using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionController : MonoBehaviour
{
    // 这个方法绑定在 MainMenu 的开始按钮上
    public void StartGame()
    {
        Time.timeScale = 1f; // 确保时间恢复
        SceneManager.LoadScene("Demo"); // 替换为你的场景名
    }

    // 在 Demo 场景中，把这个脚本挂在一个空物体上
    void Awake()
    {
        // 1. 强制重置环境
        Time.timeScale = 1f;
        
        // 2. 彻底接管鼠标（这是解决视角卡死的关键）
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 3. 打印日志确认运行
        Debug.Log("Demo Scene Loaded: Environment Reset.");
    }

    void Update()
    {
        // 增加一个紧急退出键，方便调试
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}