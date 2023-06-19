using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField] Button dialogButton; // Tombol dialog
    [SerializeField] GameObject dialogPanel;
    private PlayerController player;

    private void Start()
    {
        // Menyembunyikan tombol dialog saat awal
        dialogButton.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerController>();
            // Menampilkan tombol dialog saat player masuk ke collider NPC
            dialogButton.gameObject.SetActive(true);
            player.SetInteractableNPC(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Menyembunyikan tombol dialog saat player keluar dari collider NPC
            dialogButton.gameObject.SetActive(false);
            player.SetInteractableNPC(null);
            player = null;
        }
    }

    public void StartDialog()
    {
        // Logika untuk memulai dialog dengan NPC
        dialogPanel.SetActive(true);
        Debug.Log("Memulai percakapan dengan NPC");
    }
}
