using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyType {
    public GameObject enemyPrefab;
    public float minSpawnInterval = 0.2f;
    public float maxSpawnInterval = 2f;
    public float initialDelay = 0f; // New field for initial delay
}

public class EnemiesSpawner : MonoBehaviour {
    public List<EnemyType> enemyTypes = new List<EnemyType>();
    public float yRange = 3.8f;
    private Dictionary<EnemyType, float> spawnTimers = new Dictionary<EnemyType, float>();
    private Dictionary<EnemyType, float> nextSpawnIntervals = new Dictionary<EnemyType, float>();
    private Dictionary<EnemyType, float> initialDelayTimers = new Dictionary<EnemyType, float>(); // New dictionary for initial delays
    private bool shouldSpawn = true;
    private float gameTimer = 0f; // Timer to track game time

    void Start() {
        InitializeSpawnTimers();
        GameEvents.instance.OnPlayerDeath += Player_OnPlayerDeath;
    }

    private void OnDestroy() {
        GameEvents.instance.OnPlayerDeath -= Player_OnPlayerDeath;
    }

    private void Player_OnPlayerDeath(object sender, System.EventArgs e) {
        shouldSpawn = !shouldSpawn;
    }

    void Update() {
        if (!shouldSpawn) return;

        gameTimer += Time.deltaTime;

        foreach (var enemyType in enemyTypes) {
            // Check if the initial delay has passed
            if (gameTimer >= enemyType.initialDelay) {
                spawnTimers[enemyType] += Time.deltaTime;

                if (spawnTimers[enemyType] > nextSpawnIntervals[enemyType]) {
                    SpawnEnemy(enemyType);
                    spawnTimers[enemyType] = 0;
                    nextSpawnIntervals[enemyType] = Random.Range(enemyType.minSpawnInterval, enemyType.maxSpawnInterval);
                }
            }
        }
    }

    private void InitializeSpawnTimers() {
        foreach (var enemyType in enemyTypes) {
            spawnTimers[enemyType] = 0;
            nextSpawnIntervals[enemyType] = Random.Range(enemyType.minSpawnInterval, enemyType.maxSpawnInterval);
            initialDelayTimers[enemyType] = enemyType.initialDelay;
        }
    }

    private void SpawnEnemy(EnemyType enemyType) {
        float randomY = Random.Range(-yRange, yRange);
        Instantiate(enemyType.enemyPrefab, new Vector3(transform.position.x, randomY, 0), enemyType.enemyPrefab.transform.rotation);
    }
}