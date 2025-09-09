using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudPrefab;
    public Transform spawnPoint;
    public float respawnDelay = 5f;

    void Start()
    {
        SpawnCloud();
    }

    public void SpawnCloud()
    {
        Instantiate(cloudPrefab, spawnPoint.position, Quaternion.identity);
    }

    public void RespawnCloud()
    {
        Invoke(nameof(SpawnCloud), respawnDelay);
    }
}
