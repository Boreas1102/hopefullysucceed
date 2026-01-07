using UnityEngine;
using System.Collections;

public class TPS_SceneFixer : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1f;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        var inputs = FindFirstObjectByType<StarterAssets.StarterAssetsInputs>();
        if (inputs != null)
        {
            inputs.MoveInput(Vector2.zero);
            inputs.LookInput(Vector2.zero);
            Debug.Log("已成功强行唤醒 Starter Assets 输入系统");
        }

        var vcam = FindFirstObjectByType<Unity.Cinemachine.CinemachineVirtualCamera>();
        if (vcam != null)
        {
            vcam.enabled = false;
            vcam.enabled = true; 
        }
    }
}