using UnityEngine;

public class MoveCloud : MonoBehaviour
{
   private Camera cam;
    private bool isDragging = false;
    private Vector3 offset;
    private CloudSpawner spawner;

    void Start()
    {
        cam = Camera.main;
        spawner = FindObjectOfType<CloudSpawner>();
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouseInput();
#endif

#if UNITY_ANDROID || UNITY_IOS
        HandleTouchInput();
#endif
    }

    // -------- Mouse Input --------
    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
            TryStartDrag(GetMouseWorldPos());

        else if (Input.GetMouseButton(0) && isDragging)
            Drag(GetMouseWorldPos());

        else if (Input.GetMouseButtonUp(0) && isDragging)
            EndDrag();
    }

    // -------- Touch Input --------
    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));

            if (touch.phase == TouchPhase.Began)
                TryStartDrag(touchPos);

            else if (touch.phase == TouchPhase.Moved && isDragging)
                Drag(touchPos);

            else if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && isDragging)
                EndDrag();
        }
    }

    // -------- Drag Logic --------
    void TryStartDrag(Vector3 inputPos)
    {
        Collider2D hit = Physics2D.OverlapPoint(inputPos);
        if (hit != null && hit.gameObject == gameObject)
        {
            offset = transform.position - inputPos;
            isDragging = true;
        }
    }

    void Drag(Vector3 inputPos)
    {
        transform.position = inputPos + offset;
    }

    void EndDrag()
    {
        isDragging = false;
    }

    // Helper
    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = 10f; 
        return cam.ScreenToWorldPoint(mousePoint);
    }

    // Called when dropped onto a field
    public void Consume()
    {
        if (spawner != null)
            spawner.RespawnCloud();

        Destroy(gameObject);
    }
}
