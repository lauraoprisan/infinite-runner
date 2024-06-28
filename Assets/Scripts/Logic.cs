using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Logic : MonoBehaviour
{
    public Player player;
    public GameObject gameOverScreen;
    public Text healthText;
    private int playerHealth;

    private void Start() {
        AssignHealthValue();
        player.OnPlayerDamage += Player_OnPlayerDamage;
        player.OnPlayerDeath += Player_OnPlayerDeath;
}

    private void Player_OnPlayerDeath(object sender, System.EventArgs e) {
        gameOverScreen.SetActive(true);
    }

    private void Player_OnPlayerDamage(object sender, System.EventArgs e) {
        AssignHealthValue();
        Debug.Log("subscriber to damaged called");
    }

    private void AssignHealthValue() {
        playerHealth = player.health;
        healthText.text = playerHealth.ToString();
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOverScreen.SetActive(false);
    }
}
