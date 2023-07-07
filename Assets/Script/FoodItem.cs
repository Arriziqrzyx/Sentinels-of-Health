using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    [SerializeField] bool isHealthy; // Menandakan apakah makanan tersebut sehat atau tidak sehat
    [SerializeField] float speed = 5f; // Kecepatan pergerakan makanan
    [SerializeField] int healthDecreaseAmount = 1; // Jumlah HP yang berkurang saat memilih makanan tidak sehat
    private Rigidbody2D rb;
    private PilihMakanManager pimanmanager;

    private void Start()
    {
        pimanmanager = FindObjectOfType<PilihMakanManager>(); // Mendapatkan referensi ke skrip Game Manager

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
            pimanmanager.AddScore(10); // Menambahkan skor +10 jika makanan sehat diambil
        }
        else
        {
            pimanmanager.AddUnhealthyFood(); // Tambahkan makanan tidak sehat ke GameManager
            pimanmanager.DecreaseHealth(healthDecreaseAmount); // Mengurangi HP jika memilih makanan tidak sehat
        }
        Destroy(gameObject); // Menghapus makanan dari permainan setelah dipilih
    }

}
