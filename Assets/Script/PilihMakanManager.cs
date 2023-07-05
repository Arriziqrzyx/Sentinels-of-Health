using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class PilihMakanManager : MonoBehaviour
{
    [SerializeField] GameObject[] foodPrefabs;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spawnInterval = 1f;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] int maxHealth = 5;
    [SerializeField] float gameDuration = 60f;
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text gameTimerText;
    private float spawnTimer = 0f;
    private int spawnedFoodCount = 0;
    private int unhealthyFoodCount = 0;
    private float gameTimer;
    private int currentHealth;
    private int score = 0;
    private bool gameStarted = false;
    [SerializeField] private string Scene;
    [SerializeField] GameObject Continue;

    private List<GameObject> foodObjects = new List<GameObject>();

    private void Start()
    {
        gameTimer = gameDuration;
        currentHealth = maxHealth;
        UpdateHealthText();
        UpdateTimerText();
    }

    private void Update()
    {
        if (!gameStarted)
        {
            return;
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnFood();
            spawnTimer = 0f;
        }

        gameTimer -= Time.deltaTime;

        if (gameTimer <= 0f)
        {
            GameOver();
        }

        UpdateTimerText();
    }

    public void StartGame()
    {
        gameStarted = true;
    }

    public void OnStartButtonPressed()
    {
        StartGame();
    }

    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            GameOver();
        }

        UpdateHealthText();
    }

    public void UpdateHealthText()
    {
        healthText.text = "HP: " + currentHealth.ToString();
    }

    public void UpdateTimerText()
    {
        int gameMinutes = Mathf.Clamp(Mathf.FloorToInt(gameTimer / 60f), 0, 99);
        int gameSeconds = Mathf.Clamp(Mathf.FloorToInt(gameTimer % 60f), 0, 59);

        gameTimerText.text = string.Format("{0:00}:{1:00}", gameMinutes, gameSeconds);
    }

    private void SpawnFood()
    {
        if (gameTimer > 0f)
        {
            GameObject foodPrefab = foodPrefabs[Random.Range(0, foodPrefabs.Length)];
            GameObject foodObject = Instantiate(foodPrefab, spawnPoint.position, Quaternion.identity);
            foodObjects.Add(foodObject);
            spawnedFoodCount++;
        }
        else
        {
            StopSpawning();
        }
    }

    private void StopSpawning()
    {
        spawnTimer = 0f;
        gameStarted = false;
    }

    private void ClearFoodObjects()
    {
        foreach (GameObject foodObject in foodObjects)
        {
            Destroy(foodObject);
        }
        foodObjects.Clear();
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score.ToString();
    }

    public void AddUnhealthyFood()
    {
        unhealthyFoodCount++;
    }

    public int GetUnhealthyFoodCount()
    {
        return unhealthyFoodCount;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        StopSpawning();
        ClearFoodObjects();
        Continue.SetActive(true);
    }

    public void BackToGameplay()
    {
        SceneManager.UnloadSceneAsync(Scene);
    }
}
