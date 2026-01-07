using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ManualMenuController : MonoBehaviour
{
    [Header("UI 设置")]
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuUI.activeSelf)
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

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; 

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        
        Debug.Log("菜单已打开，鼠标已解锁");
    }

    public void ContinueGame()
    {
        if (pauseMenuUI == null) return;

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; 

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("游戏继续，鼠标已重新锁定");
    }

    public void ExitToMain()
    {
        Time.timeScale = 1f; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("正在跳转回主菜单...");
        
        SceneManager.LoadScene("MainMenu"); 
    }
}