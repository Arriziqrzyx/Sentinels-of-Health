using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public bool isHealthy; // Menandakan apakah makanan tersebut sehat atau tidak sehat
    public float speed = 5f; // Kecepatan pergerakan makanan
    public int healthDecreaseAmount = 1; // Jumlah HP yang berkurang saat memilih makanan tidak sehat
    private Rigidbody2D rb;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // Mendapatkan referensi ke skrip Game Manager

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0f); // Mengatur kecepatan awal makanan
    }

    private void Update()
    {
        // Cek apakah makanan sudah mencapai batas kiri layar
        if (transform.position.x < -12f)
        {
            Destroy(gameObject); // Menghapus makanan jika sudah mencapai batas kiri layar
        }
    }

    void OnMouseDown()
    {
        if (isHealthy)
        {
            gameManager.AddScore(10); // Menambahkan skor +10 jika makanan sehat diambil

        }
        else
        {
            gameManager.AddUnhealthyFood(); // Tambahkan makanan tidak sehat ke GameManager

            gameManager.AddScore(-5); // Mengurangi skor -5 jika makanan tidak sehat diambil
            gameManager.DecreaseHealth(healthDecreaseAmount); // Mengurangi HP jika memilih makanan tidak sehat

            // if (gameManager.GetUnhealthyFoodCount() > 2) gameover jika memilih makanan tidak sehat lebih dari 2
            // {
            //     gameManager.GameOver();
            // }
        }
        Destroy(gameObject); // Menghapus makanan dari permainan setelah dipilih
    }

}
