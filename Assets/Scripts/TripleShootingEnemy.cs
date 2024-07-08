using UnityEngine;

public class TripleShootingEnemy : BaseEnemy {
    public GameObject projectile;
    public float shootingInterval = 0.5f;  // Time between shots
    public float projectileSpeed = 7f;  // Speed of the projectile
    public float diagonalAngle = 30f;  // Angle for diagonal shots
    private float shootTimer;

    private void Start() {
        shootTimer = shootingInterval;  // Start the timer
    }

    private void Update() {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0) {
            Shoot();
            shootTimer = shootingInterval;  // Reset the timer
        }
    }

    private void Shoot() {
        if (projectile != null) {
            Vector3 shootPosition = transform.position;

            // Straight left
            SpawnProjectile(shootPosition, Vector2.left);

            // Diagonally up-left
            SpawnProjectile(shootPosition, Quaternion.Euler(0, 0, diagonalAngle) * Vector2.left);

            // Diagonally down-left
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