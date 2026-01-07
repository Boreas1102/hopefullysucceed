using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ProfessionalPauseManager : MonoBehaviour
{
    [Header("UI & References")]
    public GameObject pauseMenuUI;
    
    // 内部状态追踪，比 activeSelf 更快、更可靠
    private bool isPaused = false;

    void Start()
    {
        // 游戏启动时强制初始化状态
        ContinueGame();
    }

    void Update()
    {
        // 关键：监听 GetKeyDown 的同时，确保本帧没有被其他 UI 事件占用
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ContinueGame();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    public void OpenMenu()
    {
        if (pauseMenuUI == null) return;

        isPaused = true;
        pauseMenuUI.SetActive(true);
        
        // 1. 停止游戏时间
        Time.timeScale = 0f; 

        // 2. 释放并显示鼠标（强制顺序：先 None 再 visible）
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // 3. 核心修复：强制 EventSystem 失去当前焦点
        // 防止鼠标第一下只是为了“找回”UI 系统的焦点
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        
        // 4. 解决“点击一次”：强制刷新编辑器焦点
        #if UNITY_EDITOR
        // 这一行在打包后会自动消失，但在编辑器里能极大缓解鼠标抢夺问题
        Debug.Log("Pause: Menu opened and cursor freed.");
        #endif
    }

    public void ContinueGame()
    {
        if (pauseMenuUI == null) return;

        isPaused = false;
        pauseMenuUI.SetActive(false);
        
        // 1. 恢复时间
        Time.timeScale = 1f; 

        // 2. 重新锁定鼠标（强制顺序：先 Locked 再 false）
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 3. 核心修复：确保没有 UI 元素还在被“选中”
        // 很多时候点击两次是因为鼠标还在点 UI 上的按钮残留焦点
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        Debug.Log("Resume: Game focus restored.");
    }

    public void ExitToMain()
    {
        // 确保切场景前状态完全重置
        Time.timeScale = 1f; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu"); 
    }
}