using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour {
    public int Health { get; set; }


    public virtual void TakeDamage(int damage) {
        Health -= damage;
        if (Health <= 0) {
            Die();
        }
    }

    public virtual void Attack() {
        GameEvents.instance.PlayerDamage();
    }

    protected void Die() {
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) {

        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null && player.isPlayerAlive) {
            Attack();
        }
    }
}