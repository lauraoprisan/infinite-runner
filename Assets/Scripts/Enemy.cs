using UnityEngine;

public class Enemy : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision) {
        // Check if the collided object has the Player component
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null && player.isPlayerAlive) {
            player.DamagePlayer();
        }
    }
}
