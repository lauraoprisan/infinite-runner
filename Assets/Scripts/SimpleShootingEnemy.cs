using UnityEngine;

public class SimpleShootingEnemy : BaseEnemy {
    public GameObject projectile;
    public float shootingInterval = 0.4f;  // Time between shots
    public float projectileSpeed = 10f;  // Speed of the projectile
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
            GameObject newProjectile = Instantiate(projectile, shootPosition, transform.rotation);

            MoveForward moveScript = newProjectile.GetComponent<MoveForward>();
            if (moveScript != null) {
                moveScript.SetSpeed(projectileSpeed);
                moveScript.SetDirection(Vector2.left);  
            } else {
                Debug.LogWarning("MoveForward script not found on projectile: " + newProjectile.name);
            }
        } else {
            Debug.LogWarning("Projectile is not set on " + gameObject.name);
        }
    }
}