using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PowerUpType {
    public GameObject powerUpPrefab;
    public float spawnInterval;
  
}

public class PowerUpSpawner : MonoBehaviour {
    public List<PowerUpType> powerUpTypes = new List<PowerUpType>();
    public float yRange = 3.8f;
    private Dictionary<PowerUpType, float> spawnTimers = new Dictionary<PowerUpType, float>();
    private bool shouldSpawn = true;

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


        foreach (var powerUpType in powerUpTypes) {
            spawnTimers[powerUpType] -= Time.deltaTime;
            if (spawnTimers[powerUpType] <= 0) {
                SpawnPowerUp(powerUpType);
                spawnTimers[powerUpType] = powerUpType.spawnInterval;
            }
        }
    }

    private void InitializeSpawnTimers() {
        foreach (var powerUpType in powerUpTypes) {
            spawnTimers[powerUpType] = powerUpType.spawnInterval;
        
        }
    }

    private void SpawnPowerUp(PowerUpType powerUpType) {
        float randomY = Random.Range(-yRange, yRange);
        Instantiate(powerUpType.powerUpPrefab, new Vector3(transform.position.x, randomY, 0), powerUpType.powerUpPrefab.transform.rotation);
    }
}