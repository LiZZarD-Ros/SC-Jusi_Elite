using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class JuiceBox : MonoBehaviour
{
    [SerializeField] private string juiceType; // e.g., "Mango", "Orange"

    private void OnMouseDown()
    {
        // Notify manager
        if (JuiceBoxManager.Instance != null)
            JuiceBoxManager.Instance.AddJuice(juiceType);

        // Destroy this juice box
        Destroy(gameObject);
    }
}
