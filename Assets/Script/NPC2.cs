using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC2 : MonoBehaviour
{
    public GameObject Pesan;
    [SerializeField] private string NPc2;
    public PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Pesan.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Pesan.SetActive(false);
        }
    }

    public void SceneLoader() 
    {
        StartCoroutine(loadMiniGames(NPc2));
        playerController.objectivePoints++;
        Destroy(gameObject);
        Debug.Log("Objek destroyed");
    }

    IEnumerator loadMiniGames(string Name)
    {
        SceneManager.LoadScene(Name, LoadSceneMode.Additive);
        yield return new WaitUntil(() => SceneManager.GetSceneByName(Name).isLoaded);
    }

}
