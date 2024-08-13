using UnityEngine;

public class TripleShootingEnemy : BaseEnemy {
    public GameObject projectile;
    public float shootingInterval = 0.5f; 
    public float projectileSpeed = 7f;  
    public float diagonalAngle = 30f;
    private float shootTimer;

    private void Start() {
        shootTimer = shootingInterval;
    }

    private void Update() {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0) {
            Shoot();
            shootTimer = shootingInterval;  
        }
    }

    private void Shoot() {
        if (projectile != null) {
            Vector3 shootPosition = transform.position;

            SpawnProjectile(shootPosition, Vector2.left);

            SpawnProjectile(shootPosition, Quaternion.Euler(0, 0, diagonalAngle) * Vector2.left);

            SpawnProjectile(shootPosition, Quaternion.Euler(0, 0, -diagonalAngle) * Vector2.left);
        } else {
            Debug.LogWarning("Projectile is not set on " + gameObject.name);
        }
    }

    private void SpawnProjectile(Vector3 position, Vector2 direction) {
        GameObject newProjectile = Instantiate(projectile, position, Quaternion.identity);
        MoveForward moveScript = newProjectile.GetComponent<MoveForward>();
        if (moveScript != null) {
            moveScript.SetSpeed(projectileSpeed);
            moveScript.SetDirection(direction);
        } else {
            Debug.LogWarning("MoveForward script not found on projectile: " + newProjectile.name);
        }
    }
}