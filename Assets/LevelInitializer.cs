using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    void Awake()
    {
        // 1. 强制重置时间缩放，防止从暂停菜单跳转过来时游戏静止
        Time.timeScale = 1f;

        // 2. 强制锁定鼠标并隐藏，激活视角控制
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {
        // 如果你使用了 Cinemachine，可以在这里强制更新一次相机状态
        // 确保主相机已经找到了当前的 Virtual Camera
        Debug.Log("Gameplay Scene Initialized: Cursor Locked & Time Resumed.");
    }
}