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
    public float jangkauan; // Jarak deteksi tanah
    public int heart; // Jumlah nyawa karakter
    [SerializeField] private Text objectiveText;
    public int go, totalPoints, objectivePoints;
    [SerializeField] GameObject finishObject;
    [SerializeField] GameObject objectivePlayers;
    Vector2 play; // Posisi checkpoint terakhir
    public bool play_again = false; // Menyimpan informasi apakah karakter dapat memulai dari checkpoint terakhir
    public Text info_heart; // Komponen TextMeshPro untuk menampilkan jumlah nyawa
    Animator anim; // Komponen Animator untuk mengatur animasi karakter
    [SerializeField] AudioSource jumpAudio;
    [SerializeField] AudioSource dieAudio;
    [SerializeField] AudioSource checkpointAudio;
    public GameObject over;
    private bool Button_kiri; //Variabel untuk Button Kiri
    private bool Button_kanan; //Variabel untuk Button Kanan
    private bool Button_lompat;
    [SerializeField] private string Rangkuman;
    public bool canMove = true; // Menyimpan informasi apakah pemain dapat bergerak
    private float playTime;
    private bool gameIsPlaying;
    public static float finalPlayTime;
    private float bestPlayTime;



    void Start()
    {
        play = transform.position;
        over.SetActive(false);
        lompat = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        totalPoints = objectivePlayers.transform.childCount;
        playTime = 0f;
        gameIsPlaying = true;
        bestPlayTime = PlayerPrefs.GetFloat("BestPlayTime", Mathf.Infinity);
    }

    public void Update()
    {
        if (gameIsPlaying)
        {
        playTime += Time.deltaTime;
        Debug.Log("Play Time: " + playTime);
        }

        MethodObjectives();
        go = objectivePlayers.transform.childCount;
        if (go == 0)
        {
            finishObject.SetActive(true);
            objectiveText.text = "Misi selesai";
        }

        if (play_again)
        {
            transform.position = play;
            dieAudio.Play();
            play_again = false;
        }

        anim.SetBool("Jump", !tanah);

        tanah = Physics2D.OverlapCircle(deteksitanah.position, jangkauan, targetlayer);
        info_heart.text = "Nyawa : " + heart.ToString();
        
        if (canMove)
        {
            if (Input.GetKey(KeyCode.D) || Button_kanan)
            {
                Move(1);
            }
            else if (Input.GetKey(KeyCode.A) || Button_kiri)
            {
                Move(-1);
            }
            else
            {
                anim.SetBool("Run", false);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if ((pindah > 0 && !balik) || (pindah < 0 && balik))
            {
                Flip();
            }
        }
        else
        {
            anim.SetBool("Run", false);
        }

        if (heart < 1)
        {
            gameObject.SetActive(false);
            Debug.Log("Player Wafat");

            over.SetActive(true);
            GameOver();
            finalPlayTime = playTime;
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
        if (canMove)
       {
            if (tanah == true)
            {
            float x = lompat.velocity.x;
            lompat.velocity = new Vector2(x, kekuatanlompat);
            jumpAudio.Play();
            Debug.Log("lompat");
            }
       } 
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
            checkpointAudio.Play();
            Debug.Log("Checkpoint");
            StopAllCoroutines();
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            StartCoroutine(loadMiniGames(Rangkuman));
            Destroy(gameObject);
            Debug.Log("Objek destroyed");
            GameOver();
            
            PlayerPrefs.SetInt("FinishTouched", 1);
            Debug.Log("FINISH TOUCH");
            PlayerPrefs.Save();
        }
    }

    public void tekan_kiri()
    {
        Button_kiri = true; // Ketika ditekan
    }

    public void Lepas_kiri()
    {
        Button_kiri = false; // Ketika dilepas
    }

    public void tekan_kanan()
    {
        Button_kanan = true; // Ketika ditekan
    }

    public void lepas_kanan()
    {
        Button_kanan = false; // Ketika dilepas
    }

    public void tekan_lompat()
    {
        Jump();
    }

    public void MethodObjectives()
    {
        objectiveText.text = "Misi: temui dokter " + objectivePoints + "/" + totalPoints;
    }

    public void RestarLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator loadMiniGames(string Name)
    {
        SceneManager.LoadScene(Name, LoadSceneMode.Additive);
        yield return new WaitUntil(() => SceneManager.GetSceneByName(Name).isLoaded);
    }

    private void GameOver()
    {
    gameIsPlaying = false;
    PlayerPrefs.SetFloat("PlayTime", playTime);

    if (playTime < bestPlayTime)
        {
            bestPlayTime = playTime;
            PlayerPrefs.SetFloat("BestPlayTime", bestPlayTime);
        }
    }

}