using UnityEngine;
using TMPro;

public class PilihMakanManager : MonoBehaviour
{
    [SerializeField] GameObject[] foodPrefabs; // Array berisi prefabs makanan yang tersedia
    [SerializeField] Transform spawnPoint; // Titik spawning makanan
    [SerializeField] float spawnInterval = 1f; // Interval waktu antara spawning makanan
    [SerializeField] TMP_Text scoreText; // Referensi ke UI Text untuk menampilkan skor
    [SerializeField] int maxHealth = 5; // Jumlah HP maksimum
    [SerializeField] float gameDuration = 60f; // Durasi permainan dalam detik
    [SerializeField] TMP_Text healthText; // Referensi ke UI Text untuk menampilkan HP
    [SerializeField] TMP_Text gameTimerText; // Referensi ke UI Text untuk menampilkan timer
    private float spawnTimer = 0f; // Timer untuk menghitung interval spawning
    private int spawnedFoodCount = 0; // Jumlah makanan yang telah di-spawned
    private int unhealthyFoodCount = 0; // Jumlah makanan tidak sehat yang telah diambil
    private float gameTimer; // Waktu yang tersisa dalam detik
    private int currentHealth; // Jumlah HP saat ini
    private int score = 0; // Skor permainan
    private bool gameStarted = false;

    // Inisialisasi nilai awal saat permainan dimulai
    private void Start()
    {
        gameTimer = gameDuration; // Mengatur timer awal sesuai dengan durasi permainan
        currentHealth = maxHealth; // Mengatur HP awal sesuai dengan HP maksimum
        UpdateHealthText(); // Memperbarui tampilan HP awal
        UpdateTimerText(); // Memperbarui tampilan timer awal
    }

    // Update yang berjalan setiap frame
    private void Update()
    {
        if (!gameStarted)
        {
            return; // Hentikan eksekusi fungsi jika permainan belum dimulai
        }   
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnFood();
            spawnTimer = 0f;
        }

        gameTimer -= Time.deltaTime; // Mengurangi waktu berdasarkan delta time

        if (gameTimer <= 0f)
        {
            GameOver();
        }

        UpdateTimerText(); // Memperbarui tampilan timer setiap frame
    }

    public void StartGame()
    {
        gameStarted = true;
    }

    public void OnStartButtonPressed()
    {
        StartGame();
    }

    // Mengurangi HP berdasarkan jumlah yang diberikan
    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            GameOver();
        }

        UpdateHealthText(); // Memperbarui tampilan HP setelah HP berkurang
    }

    // Memperbarui tampilan HP pada UI Text
    public void UpdateHealthText()
    {
        healthText.text = "HP: " + currentHealth.ToString(); // Mengubah teks pada UI Text sesuai dengan HP saat ini
    }

    // Memperbarui tampilan timer pada UI Text
    public void UpdateTimerText()
    {
        // Mengonversi waktu dalam detik menjadi format menit:detik
        int gameMinutes = Mathf.Clamp(Mathf.FloorToInt(gameTimer / 60f), 0, 99);
        int gameSeconds = Mathf.Clamp(Mathf.FloorToInt(gameTimer % 60f), 0, 59);

        gameTimerText.text = string.Format("{0:00}:{1:00}", gameMinutes, gameSeconds); // Mengubah teks pada UI Text untuk durasi permainan
    }

    // Melakukan spawning makanan
    private void SpawnFood()
    {
        // Memilih prefab makanan secara acak dari array foodPrefabs
        GameObject foodPrefab = foodPrefabs[Random.Range(0, foodPrefabs.Length)];

        // Spawn makanan baru di spawnPoint
        Instantiate(foodPrefab, spawnPoint.position, Quaternion.identity);

        spawnedFoodCount++;
    }

    // Menambahkan skor berdasarkan jumlah yang diberikan
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score.ToString();
    }

    // Menambahkan jumlah makanan tidak sehat
    public void AddUnhealthyFood()
    {
        unhealthyFoodCount++;
    }

    // Mendapatkan jumlah makanan tidak sehat
    public int GetUnhealthyFoodCount()
    {
        return unhealthyFoodCount;
    }

    // Logika yang dijalankan ketika permainan berakhir
    public void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f;
    }
}
