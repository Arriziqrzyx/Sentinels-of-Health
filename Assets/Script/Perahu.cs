using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perahu : MonoBehaviour
{
 [SerializeField] float speed; // kecepatan platform
    [SerializeField] int startingPoint; // posisi awal platform
    [SerializeField] Transform[] points; // array transformasi titik-titik

    private int currentIndexPoint; // indeks array
    private bool isMovingRight = true; // pergerakan ke kanan

    private void Start()
    {
        /*
        Mengatur posisi awal platform menggunakan indeks startingPoint
        */
        transform.position = points[startingPoint].position;
    }

    private void Update()
    {
        // Memeriksa jarak antara platform dan titik
        if (Vector2.Distance(transform.position, points[currentIndexPoint].position) < 0.1f)
        {
            if (currentIndexPoint == points.Length - 1)
            {
                isMovingRight = false;
            }
            else if (currentIndexPoint == 0)
            {
                isMovingRight = true;
            }

            if (isMovingRight)
            {
                currentIndexPoint++;
            }
            else
            {
                currentIndexPoint--;
            }

            FlipSprite();
        }

        // Bergerak menuju titik berikutnya
        transform.position = Vector2.MoveTowards(transform.position, points[currentIndexPoint].position, speed * Time.deltaTime);
    }

    private void FlipSprite()
    {
        // Mengubah arah flip horizontal (flip X) objek
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
