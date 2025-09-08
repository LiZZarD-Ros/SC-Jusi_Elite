using UnityEngine;

public class UpgradeableBuilding : MonoBehaviour
{
    [SerializeField] private Sprite[] levelSprites; // 5 sprites for levels 1–5
    [SerializeField] private SpriteRenderer spriteRenderer;

    private int currentLevel = 1;
    private const int maxLevel = 5;

    private void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateSprite();
    }

    public bool Upgrade()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel++;
            UpdateSprite();
            return true;
        }
        return false;
    }

    private void UpdateSprite()
    {
        if (currentLevel - 1 < levelSprites.Length)
        {
            spriteRenderer.sprite = levelSprites[currentLevel - 1];
        }
    }

    public int GetLevel()
    {
        return currentLevel;
    }
}
