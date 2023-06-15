using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPoint : MonoBehaviour
{
    private PilihMakanManager pimanmanager;
    public int healthDecreaseAmount = 1; // Jumlah HP yang berkurang saat memilih makanan tidak sehat

    private void Start()
    {
        pimanmanager = FindObjectOfType<PilihMakanManager>(); // Mendapatkan referensi ke skrip Game Manager
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            pimanmanager.DecreaseHealth(1); // Mengurangi HP jika collider makanan sehat bersentuhan dengan objek "Player"
        }
    }
}
