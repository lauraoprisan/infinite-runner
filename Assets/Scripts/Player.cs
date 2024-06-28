using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {

    public GameObject projectile;
    [SerializeField] private float attackInterval = 0.15f;
    public int health = 3;
    public bool isPlayerAlive = true;

    public event EventHandler OnPlayerDamage;
    public event EventHandler OnPlayerDeath;

    private float timeSinceLastSHot = 0;
    private int xInitialPosition= -7;
    private int yInitialPosition = 0;



    void Update() {

        timeSinceLastSHot += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && timeSinceLastSHot > attackInterval) {
            Shoot();
            timeSinceLastSHot = 0f;
        }
    }

    private void Shoot() {
        if (projectile != null) {
            Instantiate(projectile, transform.position, transform.rotation);
        }
    }

    public void DamagePlayer() {
        health--;
        OnPlayerDamage?.Invoke(this, EventArgs.Empty);

        if (health <= 0) {
            isPlayerAlive = !isPlayerAlive;
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            transform.position = new Vector2(xInitialPosition, yInitialPosition);
        }
    }

}
