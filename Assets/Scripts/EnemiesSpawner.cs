using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour {

    public Player player;
    public GameObject enemy;
    public float yRange = 3.8f;
    private float time = 0;
    public float maxSpawnInterval = 2f;
    private float randomSpawnInterval;
    private bool shouldSpawn = true;

    // Start is called before the first frame update
    void Start() {
        // Initialize the first random spawn interval
        randomSpawnInterval = Random.Range(0.2f, maxSpawnInterval);
        if (player != null) { 
        player.OnPlayerDeath += Player_OnPlayerDeath;
        }
    }

    private void Player_OnPlayerDeath(object sender, System.EventArgs e) {
        shouldSpawn = !shouldSpawn;
        Debug.Log("spawner subscription to playerdeath");
    }

    // Update is called once per frame
    void Update() {
        time += Time.deltaTime;
        if (time > randomSpawnInterval && shouldSpawn) {
            SpawnEnemy();
            time = 0;
            // Recalculate the random spawn interval after spawning an enemy
            randomSpawnInterval = Random.Range(0.2f, maxSpawnInterval);
        }
    }

    private void SpawnEnemy() {
            float randomY = Random.Range(-yRange, yRange);
            Instantiate(enemy, new Vector3(transform.position.x, randomY, 0), enemy.transform.rotation);
    }
}
