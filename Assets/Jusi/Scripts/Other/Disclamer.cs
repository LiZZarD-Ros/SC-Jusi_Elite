using UnityEngine;
using UnityEngine.SceneManagement;

public class Disclamer : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Main Scene");
        }
    }
}
