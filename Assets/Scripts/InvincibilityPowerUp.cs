using UnityEngine;

public class InvincibilityPowerUp : MonoBehaviour {
    [SerializeField] private float powerUpDuration = 5f;

    private void OnTriggerEnter2D(Collider2D collision) {
        int playerLayer = LayerMask.NameToLayer("Player");
        if(collision.gameObject.layer == playerLayer) {
            ActivatePowerUp();
            Destroy(gameObject);
        }
    }

    private void ActivatePowerUp() {
        Player.Instance.ActivateInvincibility(powerUpDuration);
        GameEvents.instance.PlayerInvinciblePowerUpEnable();
    }
}
