using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class finalscore : MonoBehaviour
{
    public TMP_Text timeText;
    public TMP_Text bestTimeText;
    public TMP_Text finalScoreText;
    public TMP_Text highScoreText;

    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0); // Ambil nilai skor akhir dari PlayerPrefs
        finalScoreText.text = "Skor Mini Game: " + finalScore.ToString();

        int highScore = PlayerPrefs.GetInt("HighScore", 0); // Ambil nilai skor tertinggi dari PlayerPrefs
        highScoreText.text = "Skor Mini Game Tertinggi: " + highScore.ToString();



        float playTime = PlayerPrefs.GetFloat("PlayTime", 0f);
        int minutes = Mathf.FloorToInt(playTime / 60f);
        int seconds = Mathf.FloorToInt(playTime % 60f);
        timeText.text = "Durasi Bermain :" + minutes.ToString() + " Menit " + seconds.ToString() + " Detik";

        float bestPlayTime = PlayerPrefs.GetFloat("BestPlayTime", Mathf.Infinity);
        if (bestPlayTime != Mathf.Infinity)
        {
            int bestMinutes = Mathf.FloorToInt(bestPlayTime / 60f);
            int bestSeconds = Mathf.FloorToInt(bestPlayTime % 60f);
            bestTimeText.text = "Durasi Terbaik: " + bestMinutes.ToString() + " Menit " + bestSeconds.ToString() + " Detik";
        }
        else
        {
            bestTimeText.text = "Durasi Terbaik:" + minutes.ToString() + " Menit " + seconds.ToString() + " Detik";
        }
    }
}
