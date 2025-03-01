using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles spawning of lights and power-ups in the level.
/// </summary>
public class LevelManager : MonoBehaviour
{
    [Header("Light Settings")]
    [SerializeField] private GameObject lightPrefab;
    [SerializeField] private int lightCount = 5;
    [SerializeField] private Vector2 lightSpawnRangeX;
    [SerializeField] private Vector2 lightSpawnRangeY;

    [Header("Power-Up Settings")]
    [SerializeField] private List<GameObject> powerUpPrefabs;
    [SerializeField] private int initialPowerUpCount = 3;
    [SerializeField] private Vector2 powerUpSpawnRangeX;
    [SerializeField] private Vector2 powerUpSpawnRangeY;
    [SerializeField] private float powerUpSpawnInterval = 10f;

    private List<GameObject> activeLights = new List<GameObject>();
    private List<GameObject> activePowerUps = new List<GameObject>();
    private bool isSpawningPowerUps = true;

    private void Start()
    {
        // Spawn initial lights and power-ups
        SpawnLights();
        SpawnInitialPowerUps();

        // Start coroutine to spawn power-ups at intervals
        StartCoroutine(SpawnPowerUpRoutine());
    }

    private IEnumerator SpawnPowerUpRoutine()
    {
        while (isSpawningPowerUps)
        {
            yield return new WaitForSeconds(powerUpSpawnInterval);
            SpawnPowerUp();
        }
    }

    /// <summary>
    /// Spawns the specified number of lights in random positions.
    /// </summary>
    private void SpawnLights()
    {
        for (int i = 0; i < lightCount; i++)
        {
            Vector2 spawnPos = new Vector2(
                Random.Range(lightSpawnRangeX.x, lightSpawnRangeX.y),
                Random.Range(lightSpawnRangeY.x, lightSpawnRangeY.y)
            );

            GameObject lightObj = Instantiate(lightPrefab, spawnPos, Quaternion.identity);
            activeLights.Add(lightObj);
        }
    }

    /// <summary>
    /// Spawns the initial set of power-ups.
    /// </summary>
    private void SpawnInitialPowerUps()
    {
        for (int i = 0; i < initialPowerUpCount; i++)
        {
            SpawnPowerUp();
        }
    }

    /// <summary>
    /// Spawns a random power-up from the powerUpPrefabs list.
    /// </summary>
    private void SpawnPowerUp()
    {
        if (powerUpPrefabs.Count == 0) return;

        GameObject randomPowerUp = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Count)];
        Vector2 spawnPos = new Vector2(
            Random.Range(powerUpSpawnRangeX.x, powerUpSpawnRangeX.y),
            Random.Range(powerUpSpawnRangeY.x, powerUpSpawnRangeY.y)
        );

        GameObject powerUpObj = Instantiate(randomPowerUp, spawnPos, Quaternion.identity);
        activePowerUps.Add(powerUpObj);
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
