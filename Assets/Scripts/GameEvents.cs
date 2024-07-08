using System;
using UnityEngine;

public class GameEvents : MonoBehaviour {
    public static GameEvents instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public event EventHandler OnPlayerDamage;
    public event EventHandler OnPlayerDeath;
    public event EventHandler OnPlayerInvincibilityPowerUpEnable;
    public event EventHandler OnPlayerInvincibilityPowerUpDisable;

    public void PlayerDamage() {
        OnPlayerDamage?.Invoke(this, EventArgs.Empty);
    }

    public void PlayerDeath() {
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
    }

    public void PlayerInvinciblePowerUpEnable() { 
        OnPlayerInvincibilityPowerUpEnable?.Invoke(this, EventArgs.Empty);
    }

    public void PlayerInvinciblePowerUpDisable() {
        OnPlayerInvincibilityPowerUpDisable?.Invoke(this, EventArgs.Empty);
    }

}
