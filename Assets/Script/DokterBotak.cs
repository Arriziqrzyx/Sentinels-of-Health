using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DokterBotak : MonoBehaviour
{
    public GameObject Pesan;
    [SerializeField] private string NPc1;
    public PlayerController playerController;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Pesan.SetActive(true);
            anim.SetBool("Interacting", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Pesan.SetActive(false);
            anim.SetBool("Interacting", false);
        }
    }

    public void SceneLoader()
    {
        StartCoroutine(LoadMiniGames(NPc1));
        playerController.objectivePoints++;
        Destroy(gameObject);
        Debug.Log("Object destroyed");
    }

    IEnumerator LoadMiniGames(string Name)
    {
        SceneManager.LoadScene(Name, LoadSceneMode.Additive);
        yield return new WaitUntil(() => SceneManager.GetSceneByName(Name).isLoaded);
    }
}
