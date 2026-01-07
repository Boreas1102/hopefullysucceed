using UnityEngine;
using System.Collections;

public class CameraFocusTrigger : MonoBehaviour
{
    [Header("镜头设置")]
    public Transform targetCameraPos;    
    public float transitionSpeed = 1.5f; 
    public float waitTime = 3.0f;        

    [Header("音效")]
    public AudioClip triggerSound;

    private Camera mainCamera;
    private bool isTriggered = false;

    private MonoBehaviour cmBrain; 

    void Start()
    {
        mainCamera = Camera.main;
        
        if (mainCamera != null)
        {
            cmBrain = mainCamera.GetComponent("CinemachineBrain") as MonoBehaviour;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            if (targetCameraPos == null) {
                Debug.LogError("请在 Inspector 中拖入 Target Camera Pos！");
                return;
            }
            StartCoroutine(CameraSequence());
        }
    }

    IEnumerator CameraSequence()
    {
        isTriggered = true;

        if (triggerSound != null) AudioSource.PlayClipAtPoint(triggerSound, transform.position);

        if (cmBrain != null) cmBrain.enabled = false;

        Vector3 originalPos = mainCamera.transform.position;
        Quaternion originalRot = mainCamera.transform.rotation;

        yield return StartCoroutine(MoveCamera(targetCameraPos.position, targetCameraPos.rotation));

        yield return new WaitForSeconds(waitTime);

        yield return StartCoroutine(MoveCamera(originalPos, originalRot));

        if (cmBrain != null) cmBrain.enabled = true;

        isTriggered = false;
    }

    IEnumerator MoveCamera(Vector3 targetPos, Quaternion targetRot)
    {
        float elapsed = 0;
        float duration = 1.0f / transitionSpeed;
        Vector3 startPos = mainCamera.transform.position;
        Quaternion startRot = mainCamera.transform.rotation;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / duration);

            mainCamera.transform.position = Vector3.Lerp(startPos, targetPos, t);
            mainCamera.transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            yield return null;
        }
        mainCamera.transform.position = targetPos;
        mainCamera.transform.rotation = targetRot;
    }
}