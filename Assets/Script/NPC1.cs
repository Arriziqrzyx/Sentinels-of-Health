using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NPC1 : MonoBehaviour
{
    PlayerController KomponenPlayer;
    [SerializeField] Button dialogButton; // Tombol dialog
    [SerializeField] GameObject dialogPanel;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;
    private PlayerController playerController;
    [SerializeField] private string Level1;

    // Start is called before the first frame update
    void Start()
    {
        KomponenPlayer = GameObject.Find("Player").GetComponent<PlayerController>();
        dialogButton.onClick.AddListener(PanelDialogAktif);
        textComponent.text = string.Empty;
    }

    // Update is called once per frame


    void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "Player")
        {
            Debug.Log("NPC 1 Masuk");
            dialogButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("NPC 1 Keluar");
            dialogButton.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void PanelDialogAktif()
    {
        dialogPanel.SetActive(true);
        playerController = FindObjectOfType<PlayerController>();
        playerController.enabled = false; // Menonaktifkan komponen PlayerController saat dialog dimulai
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogPanel.SetActive(false);
            Debug.Log("percakapan berhenti");
            // playerController.info_heart.gameObject.SetActive(false);
            StartCoroutine(loadMiniGames(Level1));
            gameObject.SetActive(false);
            playerController.enabled = true; // Mengaktifkan kembali komponen PlayerController setelah dialog selesai
        }
    }

    IEnumerator loadMiniGames(string Name)
    {
        SceneManager.LoadScene(Name, LoadSceneMode.Additive);
        yield return new WaitUntil(() => SceneManager.GetSceneByName(Name).isLoaded);
    }
}
