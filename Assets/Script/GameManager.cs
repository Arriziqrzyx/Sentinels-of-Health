using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject[] foodPrefabs; // Array berisi prefabs makanan yang tersedia
    public Transform spawnPoint; // Titik spawning makanan
    public float spawnInterval = 2f; // Interval waktu antara spawning makanan
    private float spawnTimer = 0f; // Timer untuk menghitung interval spawning
    private int spawnedFoodCount = 0; // Jumlah makanan yang telah di-spawned

    // public int maxCollisionCount = 3; // Jumlah maksimum sentuhan yang diizinkan sebelum game over
    // private int collisionCount = 0; // Jumlah sentuhan yang terjadi
    public int score = 0; // Skor permainan
    [SerializeField] TMP_Text scoreText; // Referensi ke UI Text untuk menampilkan skor
    private int unhealthyFoodCount = 0; // Jumlah makanan tidak sehat yang telah diambil
    [SerializeField] int maxHealth = 5; // Jumlah HP maksimum
    public int currentHealth; // Jumlah HP saat ini
    [SerializeField] float gameDuration = 30f; // Durasi permainan dalam detik
    public float timer; // Waktu yang tersisa dalam detik

    private void Start()
    {
        timer = gameDuration; // Mengatur timer awal sesuai dengan durasi permainan
        currentHealth = maxHealth; // Mengatur HP awal sesuai dengan HP maksimum
    }

    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnFood();
            spawnTimer = 0f;
        }

        timer -= Time.deltaTime; // Mengurangi waktu berdasarkan delta time

        if (timer <= 0f)
        {
            GameOver();
        }
    }

    // public void FoodCollision()
    // {
    //     collisionCount++; // Menambah jumlah collision saat terjadi collision dengan makanan
    //     Debug.Log("Collision Count: " + collisionCount);

    //     // if (collisionCount >= maxCollisionCount)
    //     // {
    //     //     GameOver(); // Memanggil fungsi GameOver jika jumlah collision mencapai batas
    //     // }
    // }

    private void SpawnFood()
    {
        // Memilih prefab makanan secara acak dari array foodPrefabs
        GameObject foodPrefab = foodPrefabs[Random.Range(0, foodPrefabs.Length)];

        // Spawn makanan baru di spawnPoint
        Instantiate(foodPrefab, spawnPoint.position, Quaternion.identity);

        spawnedFoodCount++;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd; // Menambahkan atau mengurangi skor sesuai dengan scoreToAdd
        // Debug.Log("Score: " + score);
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
        // Menjalankan logika game over
        Debug.Log("Game Oveeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeer");
        Time.timeScale = 0;
        // Misalnya, mengubah scene ke scene game over
        // SceneManager.LoadScene("GameOverScene");
    }
}
