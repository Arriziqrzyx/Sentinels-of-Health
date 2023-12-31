using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonShow : MonoBehaviour
{
    [SerializeField] GameObject Button;

    private void Start()
    {
        SoundManager.Instance.musicSource.mute = true;
        Debug.Log("Trigger");
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(17.5f);
         Button.SetActive(true);
    }

    public void backToMain(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        SoundManager.Instance.musicSource.mute = false;
    }
}
