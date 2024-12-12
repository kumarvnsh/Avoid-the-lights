using UnityEngine;

public class LightSpawner : MonoBehaviour
{
    [SerializeField] private GameObject lightPrefab; // Prefab for light sources
    [SerializeField] private int lightCount = 5; // Number of lights to spawn
    [SerializeField] private Vector2 spawnRangeX; // Min and Max X for spawn
    [SerializeField] private Vector2 spawnRangeY; // Min and Max Y for spawn

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            SpawnLights();
        }
        else
        {
            Debug.LogError("Player not found in the scene.");
        }
    }

    private void SpawnLights()
    {
        for (int i = 0; i < lightCount; i++)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(spawnRangeX.x, spawnRangeX.y),
                Random.Range(spawnRangeY.x, spawnRangeY.y)
            );

            Instantiate(lightPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
