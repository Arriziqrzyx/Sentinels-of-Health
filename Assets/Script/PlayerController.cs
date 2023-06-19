using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public int kecepatan; // Kecepatan gerakan horizontal
    public int kekuatanlompat; // Kekuatan lompatan
    public bool balik; // Menyimpan informasi arah hadap karakter
    public int pindah; // Menyimpan arah gerakan karakter
    Rigidbody2D lompat; // Komponen Rigidbody2D untuk lompatan
    public bool tanah; // Menyimpan informasi apakah karakter berada di tanah
    public LayerMask targetlayer; // Layer yang dikategorikan sebagai tanah
    public Transform deteksitanah; // Posisi deteksi tanah
    public float jangkauan; // Jarak deteksi tanah
    public int heart; // Jumlah nyawa karakter
    Vector2 play; // Posisi checkpoint terakhir
    public bool play_again = false; // Menyimpan informasi apakah karakter dapat memulai dari checkpoint terakhir
    [SerializeField] TMP_Text info_heart; // Komponen TextMeshPro untuk menampilkan jumlah nyawa
    Animator anim; // Komponen Animator untuk mengatur animasi karakter
    private NPC interactableNPC; // NPC yang dapat berinteraksi dengan pemain
    private bool isDialogActive; // Menyimpan informasi apakah dialog sedang berlangsung

    void Start()
    {
        play = transform.position;
        lompat = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (play_again == true)
        {
            transform.position = play;
            play_again = false;
        }

        if (tanah == false)
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }

        tanah = Physics2D.OverlapCircle(deteksitanah.position, jangkauan, targetlayer);
        info_heart.text = "Life : " + heart.ToString();

        // Cek apakah dialog sedang berlangsung
        if (!isDialogActive)
        {
            // Gerakan karakter hanya saat dialog tidak aktif
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector2.right * kecepatan * Time.deltaTime);
                pindah = -1;
                if (tanah == true)
                {
                    anim.SetBool("Run", true);
                }
                else
                {
                    anim.SetBool("Run", false);
                }
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector2.left * kecepatan * Time.deltaTime);
                pindah = 1;
                if (tanah == true)
                {
                    anim.SetBool("Run", true);
                }
                else
                {
                    anim.SetBool("Run", false);
                }
            }
            else
            {
                anim.SetBool("Run", false);
            }

            // Lompat karakter
            if (tanah == true && Input.GetKeyDown(KeyCode.Space))
            {
                float x = lompat.velocity.x;
                lompat.velocity = new Vector2(x, kekuatanlompat);
            }

            // Mengubah arah karakter
            if (pindah > 0 && !balik)
            {
                flip();
            }
            else if (pindah < 0 && balik)
            {
                flip();
            }
        }

        // Menghentikan karakter jika nyawa habis
        if (heart < 1)
        {
            gameObject.SetActive(false);
            Debug.Log("Player Wafat");
        }

        // Mengecek input pemain untuk memulai dialog dengan NPC
        if (Input.GetKeyDown(KeyCode.E) && interactableNPC != null && !isDialogActive)
        {
            interactableNPC.StartDialog();
            isDialogActive = true;
        }
    }

    void flip()
    {
        balik = !balik;
        Vector3 Player = transform.localScale;
        Player.x *= -1;
        transform.localScale = Player;
    }

    // Mengatur NPC yang dapat berinteraksi dengan pemain
    public void SetInteractableNPC(NPC npc)
    {
        interactableNPC = npc;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Pengecekan saat karakter menemukan checkpoint
        if (other.gameObject.tag == "Checkpoint")
        {
            play = other.transform.position;
            Debug.Log("Checkpoint");
            StopAllCoroutines();
        }
    }

    // Mematikan dialog dan mengaktifkan kembali kontrol pemain
    public void EndDialog()
    {
        isDialogActive = false;
    }
}
