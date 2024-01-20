using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonShow : MonoBehaviour
{
    [SerializeField] GameObject Button;
    private string sceneToLoad; // Nama scene yang akan dimuat

    private void Start()
    {
        SoundManager.Instance.SetBGMVolume(0f);
        Debug.Log("Pemicu");
        StartCoroutine(Tunggu());
    }

    IEnumerator Tunggu()
    {
        yield return new WaitForSeconds(17.5f);
        Button.SetActive(true);
    }

    public void LoadScene(string sceneName)
    {
        sceneToLoad = sceneName;
        SceneManager.LoadScene(sceneToLoad);
        SoundManager.Instance.SetBGMVolume(1f); // Mengatur volume BGM menjadi 1
    }
}
