using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SelectField : MonoBehaviour
{
    [Header("Field Settings")]
    public string fieldName = "Field 1";
    public int baseYield = 5;

    [Header("FX")]
    public ParticleSystem rainPrefab; // assign a rain prefab (Play On Awake = false)
    public SpriteRenderer spriteRenderer; // assign your field sprite here

    private bool rainBonusActive = false;
    private Color originalColor;
    private Color blackHighlight = new Color(0f, 0f, 0f, 0.9f); // black with 90% opacity

    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    void OnMouseEnter()
    {
        // Only highlight if a cloud is selected
        if (SelectableCloud.Selected != null && spriteRenderer != null)
        {
            // Blend original color with black highlight
            spriteRenderer.color = Color.Lerp(originalColor, blackHighlight, 0.5f);
        }
    }

    void OnMouseExit()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }

    void OnMouseDown()
    {
        if (SelectableCloud.Selected == null)
        {
            Debug.Log($"{fieldName}: No cloud selected.");
            return;
        }

        // Apply rain bonus
        rainBonusActive = true;

        // Spawn rain particle above the field
        if (rainPrefab != null)
        {
            Vector3 spawnPos = transform.position + Vector3.up * 1.5f; // adjust height to look good
            ParticleSystem rain = Instantiate(rainPrefab, spawnPos, Quaternion.identity);
            rain.Play();

            // Auto-destroy particle after it finishes
            Destroy(rain.gameObject, rain.main.duration + rain.main.startLifetime.constantMax);
        }
        else
        {
            Debug.LogWarning($"{fieldName}: No rainPrefab assigned!");
        }

        Debug.Log($"{fieldName}: Rain applied, next harvest doubled!");

        // Consume the cloud
        SelectableCloud.Selected.Consume();

        // Reset highlight immediately
        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;
    }

    public int Harvest()
    {
        int yield = baseYield;
        if (rainBonusActive)
        {
            yield *= 2;
            rainBonusActive = false;
        }

        Debug.Log($"{fieldName} harvested: {yield}");
        return yield;
    }
}
