using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    [SerializeField] bool isHealthy;
    [SerializeField] float speed = 5f;
    [SerializeField] int healthDecreaseAmount = 1;

    private Rigidbody2D rb;
    private PilihMakanManager pimanmanager;

    private void Start()
    {
        pimanmanager = FindObjectOfType<PilihMakanManager>();

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0f);
    }

    private void Update()
    {
        if (transform.position.x < -12f)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        if (isHealthy)
        {
            pimanmanager.AddScore(10);
        }
        else
        {
            pimanmanager.AddUnhealthyFood();
            pimanmanager.DecreaseHealth(healthDecreaseAmount);
        }

        gameObject.SetActive(false);
    }
}
