using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPoint : MonoBehaviour
{
    private GameManager gameManager;
    public int healthDecreaseAmount = 1; // Jumlah HP yang berkurang saat memilih makanan tidak sehat

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // Mendapatkan referensi ke skrip Game Manager
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            // gameManager.FoodCollision(); // Memanggil fungsi FoodCollision di skrip Game Manager
            gameManager.DecreaseHealth(1); // Mengurangi HP jika collider makanan sehat bersentuhan dengan objek "Player"
        }
    }
}
