using UnityEngine;

public class SelectableCloud : MonoBehaviour
{
    public static SelectableCloud Selected;

    [Header("Cloud Settings")]
    public SpriteRenderer spriteRenderer;
    public Color selectedColor = Color.cyan;
    private Color originalColor;

    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    void OnMouseDown()
    {
        if (Selected == this)
        {
            Selected = null;
            if (spriteRenderer != null)
                spriteRenderer.color = originalColor;
        }
        else
        {
            if (Selected != null && Selected.spriteRenderer != null)
                Selected.spriteRenderer.color = Selected.originalColor;

            Selected = this;
            if (spriteRenderer != null)
                spriteRenderer.color = selectedColor;
        }
    }

    public void Consume()
    {
        Debug.Log("Cloud consumed after watering.");
        Selected = null;
        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;
    }
}
