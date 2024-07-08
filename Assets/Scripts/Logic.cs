using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logic : MonoBehaviour {
 
    public GameObject gameOverScreen;
    public Text healthText;
    public Text distanceText;
    public Text highscoreText;
    public float speed = 0.5f;
    private float distanceTraveled = 0f;

    private const string HighScoreKey = "Highscore";

    private void Start() {
        UpdateHealthText();
        GameEvents.instance.OnPlayerDamage += OnPlayerDamage;
        GameEvents.instance.OnPlayerDeath += OnPlayerDeath;
        InitializeHighScore();
        
    }

    private void Update() {
        if (Player.Instance.isPlayerAlive) { 
            float distanceThisFrame = speed * Time.deltaTime;
            distanceTraveled += distanceThisFrame;
            distanceText.text = (Math.Floor(distanceTraveled)).ToString();
        }
    }

    private void OnDestroy() {
        GameEvents.instance.OnPlayerDamage -= OnPlayerDamage;
        GameEvents.instance.OnPlayerDeath -= OnPlayerDeath;
    }

    private void OnPlayerDeath(object sender, EventArgs e) {
        gameOverScreen.SetActive(true);
        UpdateHighScore();
        highscoreText.text = HighScoreKey + " " + GetHighScore().ToString();
    }

    private void OnPlayerDamage(object sender, EventArgs e) {
        UpdateHealthText();
    }

    private void UpdateHealthText() {
        healthText.text = Player.Instance.health.ToString();
    }

 

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void InitializeHighScore() {
        if (!PlayerPrefs.HasKey(HighScoreKey)) {
            PlayerPrefs.SetInt(HighScoreKey, 0);
        }
    }

    private int GetHighScore() {
        return PlayerPrefs.GetInt(HighScoreKey);
    }

    private void UpdateHighScore() {
        if (distanceTraveled > GetHighScore()) {
            SetHighScore(Mathf.FloorToInt(distanceTraveled));
        }
    }

    private void SetHighScore(int score) {
        PlayerPrefs.SetInt(HighScoreKey, score);
        PlayerPrefs.Save();  // Ensure the value is saved to disk
    }

}
