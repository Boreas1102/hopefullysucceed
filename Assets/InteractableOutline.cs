using UnityEngine;

public class InteractableOutline : MonoBehaviour
{
    private Outline outline;

    void Awake()
    {
        outline = GetComponent<Outline>();
        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
        }

        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.yellow; 
        outline.OutlineWidth = 5f;
        outline.enabled = false; 
    }

    public void ShowOutline(bool state)
    {
        if (outline != null)
        {
            outline.enabled = state;
        }
    }
}