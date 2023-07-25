using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MakanSkor : MonoBehaviour
{
    public GameObject Pesan;
    public TMP_Text finalScoreText;
    public TMP_Text highScoreText;

    private void Start()
    {

    }

    private void Update()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0); // Ambil nilai skor akhir dari PlayerPrefs
        finalScoreText.text = "Skor Terakhir: " + finalScore.ToString();

        int highScore = PlayerPrefs.GetInt("HighScore", 0); // Ambil nilai skor tertinggi dari PlayerPrefs
        highScoreText.text = "Skor Tertinggi: " + highScore.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Pesan.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Pesan.SetActive(false);
        }
    }
}
