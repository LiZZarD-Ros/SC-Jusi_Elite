using UnityEngine;

public class SelectField : MonoBehaviour
{
     [Header("Field Settings")]
    public string fieldName = "Field 1";
    public int baseYield = 5;

    public ParticleSystem rainPrefab; // assign a rain prefab (Play On Awake = false)
    public SpriteRenderer spriteRenderer; // assign your field sprite here

    private bool rainBonusActive = false;
    private Color originalColor;

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
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.6f); // fade
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
            Vector3 spawnPos = transform.position + Vector3.up * 2.5f; // adjust height to look good
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
