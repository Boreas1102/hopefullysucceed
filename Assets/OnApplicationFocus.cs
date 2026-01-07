using UnityEngine;
using StarterAssets; 

public class UltimateTPSUnlocker : MonoBehaviour
{
    private StarterAssetsInputs _inputs;
    private ThirdPersonController _controller;

    void Start()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
        _controller = GetComponent<ThirdPersonController>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.anyKey && _inputs != null)
        {
            _inputs.cursorLocked = true;
            _inputs.cursorInputForLook = true;
        }

        if (Time.timeScale < 1f) Time.timeScale = 1f;
    }
}