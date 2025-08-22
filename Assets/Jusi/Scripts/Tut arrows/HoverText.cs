using UnityEngine;
using TMPro;

public class HoverText : MonoBehaviour
{
    [Header("UI Text")]
    [SerializeField] private TMP_Text hoverText;

    private void Start()
    {
        // Make sure the text is hidden initially
        if (hoverText != null)
            hoverText.gameObject.SetActive(false);
    }

    private void OnMouseEnter()
    {
        if (hoverText != null)
            hoverText.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (hoverText != null)
            hoverText.gameObject.SetActive(false);
    }
}
