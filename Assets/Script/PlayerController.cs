using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

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
    private float jangkauan; // Jarak deteksi tanah
    public int heart; // Jumlah nyawa karakter
    [SerializeField] private Text objectiveText;
    public int go, totalPoints, objectivePoints;
    [SerializeField] GameObject finishObject;
    [SerializeField] private GameObject objectivePlayers;
    Vector2 play; // Posisi checkpoint terakhir
    public bool play_again = false; // Menyimpan informasi apakah karakter dapat memulai dari checkpoint terakhir
    public Text info_heart; // Komponen TextMeshPro untuk menampilkan jumlah nyawa
    Animator anim; // Komponen Animator untuk mengatur animasi karakter
    void Start()
    {
        play = transform.position;
        lompat = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        totalPoints = objectivePlayers.transform.childCount;
    }

    public void Update()
    {
        MethodObjectives();
        go = objectivePlayers.transform.childCount;
        if (go == 0)
        {
            finishObject.SetActive(true);
            objectiveText.text = "Objective Complete!";
        }

        if (play_again)
        {
            transform.position = play;
            play_again = false;
        }

        anim.SetBool("Jump", !tanah);

        tanah = Physics2D.OverlapCircle(deteksitanah.position, jangkauan, targetlayer);
        info_heart.text = "Nyawa : " + heart.ToString();
        
        {
            if (Input.GetKey(KeyCode.D))
            {
                Move(1);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Move(-1);
            }
            else
            {
                anim.SetBool("Run", false);
            }

            if (tanah && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if ((pindah > 0 && !balik) || (pindah < 0 && balik))
            {
                Flip();
            }
        }

        if (heart < 1)
        {
            gameObject.SetActive(false);
            Debug.Log("Player Wafat");
        }
    }

    void Move(int direction)
    {
        transform.Translate(Vector2.right * kecepatan * direction * Time.deltaTime);
        pindah = direction;

        if (tanah)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }

    void Jump()
    {
        float x = lompat.velocity.x;
        lompat.velocity = new Vector2(x, kekuatanlompat);
    }

    void Flip()
    {
        balik = !balik;
        Vector3 Player = transform.localScale;
        Player.x *= -1;
        transform.localScale = Player;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            play = other.transform.position;
            Debug.Log("Checkpoint");
            StopAllCoroutines();
        }
    }

    public void MethodObjectives()
    {
        objectiveText.text = "Find All The Objectives " + objectivePoints + "/" + totalPoints;
    }
}
