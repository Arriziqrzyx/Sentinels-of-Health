using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pop : MonoBehaviour
{
    public GameObject Pesan;
    public PlayerController playerController;
    public float disableMoveTime = 10f; // Waktu nonaktifkan pemain untuk bergerak setelah bersentuhan dengan objek "pop"
    private bool hasPlayerTouched = false; // Menyimpan informasi apakah pemain sudah tersentuh sebelumnya

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!hasPlayerTouched)
            {
                hasPlayerTouched = true;
                playerController.canMove = false;
                StartCoroutine(EnableMovementAfterDelay(disableMoveTime));
            }
            Pesan.SetActive(true);
        }
    }

    public IEnumerator EnableMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerController.canMove = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Pesan.SetActive(false);
        }
    }
}
