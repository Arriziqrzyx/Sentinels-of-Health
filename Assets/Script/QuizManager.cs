using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;


public class QuizManager : MonoBehaviour
{
    public Question[] questions; // Array untuk menyimpan pertanyaan
    private int currentQuestionIndex; // Indeks pertanyaan saat ini
    public TMP_Text questionText; // Teks pertanyaan
    public Button[] answerButtons; // Tombol opsi jawaban
    public GameObject successPanel; // Panel hasil "Berhasil"
    public GameObject failPanel; // Panel hasil "Kalah"
    public TMP_Text resultWinText; // Teks hasil akhir
    public TMP_Text resultLoseText; // Teks hasil akhir
    [SerializeField] private string Scene;
    private int correctAnswersCount; // Jumlah jawaban benar

    private void Start()
    {
        currentQuestionIndex = 0;
        correctAnswersCount = 0;
        LoadQuestion(currentQuestionIndex);
    }

    private void LoadQuestion(int questionIndex)
    {
        // Memuat pertanyaan dan opsi jawaban ke antarmuka pengguna
        questionText.text = questions[questionIndex].question;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = questions[questionIndex].answers[i];
        }
    }

    public void AnswerButtonClicked(int buttonIndex)
    {
        // Memeriksa jawaban yang dipilih oleh pemain
        if (buttonIndex == questions[currentQuestionIndex].correctAnswerIndex)
        {
            Debug.Log("Jawaban benar!");
            correctAnswersCount++;
        }
        else
        {
            Debug.Log("Jawaban salah!");
        }

        // Pindah ke pertanyaan selanjutnya
        currentQuestionIndex++;

        if (currentQuestionIndex < questions.Length)
        {
            LoadQuestion(currentQuestionIndex);
        }
        else
        {
            ShowResult();
        }
    }

    private void ShowResult()
    {   
        if (correctAnswersCount == questions.Length)
        {
            successPanel.SetActive(true); // Tampilkan panel "Berhasil"
            resultWinText.text = "Semua jawaban benar!";
        }
        else
        {
            failPanel.SetActive(true); // Tampilkan panel "Kalah"
            resultLoseText.text = "Jawaban benar: " + correctAnswersCount + " / " + questions.Length;
        }
    }

    IEnumerator LoadGame(string Name)
    {
        SceneManager.LoadScene(Name, LoadSceneMode.Additive);
        yield return new WaitUntil(() => SceneManager.GetSceneByName(Name).isLoaded);
    }

    public void PlayAgain()
    {
        StartCoroutine(LoadGame(Scene));
    }


    public void BackToGameplay()
    {
        SceneManager.UnloadSceneAsync(Scene);
    }

}
