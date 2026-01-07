using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LetterInteract1 : MonoBehaviour
{
    [Header("UI 引用")]
    public GameObject uiPrompt;           
    public CanvasGroup letterCanvasGroup;  
    public RectTransform letterRect;     

    [Header("动画设置")]
    public float animationDuration = 0.5f;
    public float fadeDistance = 50f;

    [Header("描边设置")]
    public Color outlineColor = Color.yellow;
    [Range(0f, 10f)]
    public float outlineWidth = 5f;

    private bool isPlayerInRange = false;
    private bool isReading = false;
    private Vector2 originalPosition;
    private Outline outline;

    void Awake()
    {
        outline = GetComponent<Outline>();
        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
        }
        
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = outlineColor;
        outline.OutlineWidth = outlineWidth;
        outline.enabled = false; 
    }

    void Start()
    {
        if (letterCanvasGroup != null)
        {
            letterCanvasGroup.alpha = 0; 
            letterCanvasGroup.gameObject.SetActive(false);
            originalPosition = letterRect.anchoredPosition;
        }
        else
        {
            Debug.LogError("【错误】LetterCanvasGroup 未关联！请检查 Inspector 面板。");
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isReading) OpenLetter();
            else CloseLetter();
        }

        if (isReading && Input.GetKeyDown(KeyCode.Escape)) CloseLetter();
    }

    public void OpenLetter()
    {
        Debug.Log("【系统】打开信纸界面");
        isReading = true;
        if(uiPrompt != null) uiPrompt.SetActive(false);
        
        if (outline != null) outline.enabled = false;

        letterCanvasGroup.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(FadeIn());
    }

    public void CloseLetter()
    {
        Debug.Log("【系统】关闭信纸界面");
        isReading = false;
        
        if (isPlayerInRange && outline != null) outline.enabled = true;

        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0;
        Vector2 startPos = originalPosition - new Vector2(0, fadeDistance);
        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / animationDuration;
            letterCanvasGroup.alpha = Mathf.Lerp(0, 1, t);
            letterRect.anchoredPosition = Vector2.Lerp(startPos, originalPosition, t);
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float elapsed = 0;
        Vector2 endPos = originalPosition - new Vector2(0, fadeDistance);
        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / animationDuration;
            letterCanvasGroup.alpha = Mathf.Lerp(1, 0, t);
            letterRect.anchoredPosition = Vector2.Lerp(originalPosition, endPos, t);
            yield return null;
        }
        letterCanvasGroup.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(">>> [检测成功] 玩家进入了信纸的检测范围！");
            isPlayerInRange = true;
            
            if (uiPrompt != null && !isReading) uiPrompt.SetActive(true);
            if (outline != null && !isReading) outline.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("<<< [检测成功] 玩家离开了信纸范围。");
            isPlayerInRange = false;
            
            if (uiPrompt != null) uiPrompt.SetActive(false);
            if (outline != null) outline.enabled = false;
            
            if (isReading) CloseLetter();
        }
    }
}