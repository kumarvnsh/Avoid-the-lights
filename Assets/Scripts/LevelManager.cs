using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Light Settings")]
    [SerializeField] private GameObject lightPrefab;
    [SerializeField] private int lightCount = 5;
    [SerializeField] private Vector2 lightSpawnRangeX;
    [SerializeField] private Vector2 lightSpawnRangeY;

    [Header("Power-Up Settings")]
    [SerializeField] private List<GameObject> powerUpPrefabs; // List of power-up prefabs
    [SerializeField] private int initialPowerUpCount = 3; // Number of power-ups to spawn initially
    [SerializeField] private Vector2 powerUpSpawnRangeX;
    [SerializeField] private Vector2 powerUpSpawnRangeY;
    [SerializeField] private float powerUpSpawnInterval = 10f; // Time between power-up spawns

    private List<GameObject> activeLights = new List<GameObject>();
    private List<GameObject> activePowerUps = new List<GameObject>();

    private void Start()
    {
        SpawnLights();
        SpawnInitialPowerUps();
        InvokeRepeating(nameof(SpawnPowerUp), powerUpSpawnInterval, powerUpSpawnInterval);
    }

    private void SpawnLights()
    {
        for (int i = 0; i < lightCount; i++)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(lightSpawnRangeX.x, lightSpawnRangeX.y),
                Random.Range(lightSpawnRangeY.x, lightSpawnRangeY.y)
            );

            GameObject light = Instantiate(lightPrefab, spawnPosition, Quaternion.identity);
            activeLights.Add(light);
        }
    }

    private void SpawnInitialPowerUps()
    {
        for (int i = 0; i < initialPowerUpCount; i++)
        {
            SpawnPowerUp();
        }
    }

    private void SpawnPowerUp()
    {
        if (powerUpPrefabs.Count == 0) return;

        // Choose a random power-up prefab
        GameObject randomPowerUp = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Count)];

        // Determine a spawn position
        Vector2 spawnPosition = new Vector2(
            Random.Range(powerUpSpawnRangeX.x, powerUpSpawnRangeX.y),
            Random.Range(powerUpSpawnRangeY.x, powerUpSpawnRangeY.y)
        );

        // Instantiate the power-up
        GameObject powerUp = Instantiate(randomPowerUp, spawnPosition, Quaternion.identity);
        activePowerUps.Add(powerUp);
    }

    private void Update()
    {
        // Clean up destroyed lights
        for (int i = activeLights.Count - 1; i >= 0; i--)
        {
            if (activeLights[i] == null)
            {
                activeLights.RemoveAt(i);
            }
        }

        // Clean up destroyed power-ups
        for (int i = activePowerUps.Count - 1; i >= 0; i--)
        {
            if (activePowerUps[i] == null)
            {
                activePowerUps.RemoveAt(i);
            }
        }
    }
}
