using System;
using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    public GameObject projectile;
    [SerializeField] private float attackInterval = 0.15f;
    public int health = 3;
    public bool isPlayerAlive = true;

    public bool isPlayerInInvincibleMode = false;
    private SpriteRenderer spriteRenderer;
    private Coroutine invincibilityCoroutine;
    private Color originalColor;
    [SerializeField] private float colorChangeInterval = 0.2f;

    private float timeSinceLastShot = 0;
    private readonly Vector2 initialPosition = new Vector2(-7, 0);

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        GameEvents.instance.OnPlayerDamage += Instance_OnPlayerDamage;

        if (spriteRenderer != null) {
            originalColor = spriteRenderer.color;
        }
    }



    void Update() {
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && timeSinceLastShot > attackInterval) {
            Shoot();
            timeSinceLastShot = 0f;
        }
    }

    private void Shoot() {
        if (projectile != null) {
            GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
        MoveForward moveScript = newProjectile.GetComponent<MoveForward>();
        if (moveScript != null) {
            moveScript.SetDirection(Vector2.right);
        } else {
            Debug.LogWarning("MoveForward script not found on projectile: " + newProjectile.name);
        }
        }

    }

    private void Instance_OnPlayerDamage(object sender, EventArgs e) {
        if (!isPlayerAlive || isPlayerInInvincibleMode) return;

        this.health--;
        if (health <= 0) {
            isPlayerAlive = false;
            GameEvents.instance.PlayerDeath();
            transform.position = initialPosition;
        }
    }

    public void ActivateInvincibility(float duration) {
        if (invincibilityCoroutine != null) {
            StopCoroutine(invincibilityCoroutine);
        }
        invincibilityCoroutine = StartCoroutine(InvincibilityRoutine(duration));
    }

    private IEnumerator InvincibilityRoutine(float duration) {
       
        isPlayerInInvincibleMode = true;

        StartCoroutine(ColorChangeRoutine());
        yield return new WaitForSeconds(duration);
        isPlayerInInvincibleMode = false;
        
        if (spriteRenderer != null) {
            spriteRenderer.color = originalColor ;
        }
       
        GameEvents.instance.PlayerInvinciblePowerUpDisable();
    }
    private IEnumerator ColorChangeRoutine() {
        WaitForSeconds wait = new WaitForSeconds(colorChangeInterval);
        while (isPlayerInInvincibleMode) {
            if (spriteRenderer != null) {
                spriteRenderer.color = UnityEngine.Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);
            }
            yield return wait;
        }
    }
}






