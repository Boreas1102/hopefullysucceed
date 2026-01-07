using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 
public class DeathInteractionManager : MonoBehaviour
{
    [Header("引用你的死亡文字")]
    public TextMeshProUGUI deathText; 
    [Header("设置")]
    public string mainMenuSceneName = "MainMenu";

    void Update()
    {
        if (deathText != null && deathText.gameObject.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ReturnToMainMenu();
            }
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("检测到死亡文本显示，执行 ESC 返回主菜单");
        SceneManager.LoadScene(mainMenuSceneName);
    }
}