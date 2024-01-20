using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader2 : MonoBehaviour
{
    private string sceneToLoad; // Nama scene yang akan dimuat

    public void LoadScene(string sceneName)
    {
        sceneToLoad = sceneName;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Keluar()
    {
        Debug.Log ("KAMU TELAH KELUAR!");
        Application.Quit();
    }
}
