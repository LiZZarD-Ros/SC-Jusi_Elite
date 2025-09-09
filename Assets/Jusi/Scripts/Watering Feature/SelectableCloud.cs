using UnityEngine;

public class SelectableCloud : MonoBehaviour
{
   // Global "currently selected cloud"
    public static SelectableCloud Selected { get; private set; }
    public SpriteRenderer spriteRenderer; // assign your field sprite here
    private Color originalColor;
    private CloudSpawner spawner;
    [Header("Optional Selection Visual")]
    public SpriteRenderer selectionGlow; // assign an outline/glow sprite (can be null)

    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }
    void Start()
    {
        spawner = FindObjectOfType<CloudSpawner>();
        SetGlow(false);
    }

    // Works for mouse + touch if there's a Collider2D on this GameObject
    void OnMouseDown()
    {
        // Only highlight if a cloud is selected
        if (SelectableCloud.Selected != null && spriteRenderer != null)
        {
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.6f); // fade
        }
        // Toggle selection: tap again to deselect
        if (Selected == this)
        {
            Deselect();
        }
        else
        {
            Select();
        }
    }

    private void Select()
    {
        // Turn off previous selection visual if any
        if (Selected != null && Selected != this)
            Selected.SetGlow(false);

        Selected = this;
        SetGlow(true);
        Debug.Log("Cloud selected.");
        // Reset highlight immediately
        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;
    }

    private void Deselect()
    {
        SetGlow(false);
        if (Selected == this) Selected = null;
        Debug.Log("Cloud deselected.");
    }

    void OnMouseExit()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }
    public void Consume()
    {
        // consume & respawn a new one later
        if (spawner != null)
            spawner.RespawnCloud();

        // Clear selection if this is the selected cloud
        if (Selected == this) Selected = null;

        Destroy(gameObject);
    }

    private void OnDisable()
    {
        if (Selected == this) Selected = null;
    }

    private void SetGlow(bool on)
    {
        if (selectionGlow != null)
            selectionGlow.enabled = on;
    }
}
