using UnityEngine;

public class DetectCollision : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {

        if (gameObject.name.Contains("EnemyProjectile")) {
            HandleEnemyProjectileCollision(collision);
        } else {
            HandlePlayerProjectileCollision(collision);
        }
    }

    private void HandleEnemyProjectileCollision(Collider2D collision) {
        int playerLayer = LayerMask.NameToLayer("Player");
       
        
        if (collision.gameObject.layer == playerLayer) {
            GameEvents.instance.PlayerDamage();
            Destroy(gameObject);
        }
    }

    private void HandlePlayerProjectileCollision(Collider2D collision) {
        int destroyableLayer = LayerMask.NameToLayer("Destroyable");
        if (collision.gameObject.layer == destroyableLayer) {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}