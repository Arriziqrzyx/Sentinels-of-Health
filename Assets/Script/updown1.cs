using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

 class updown1 : MonoBehaviour
{
    [SerializeField] private GameObject virus1;
    [SerializeField] private GameObject virus2;
    [SerializeField] private GameObject virus3;
    [SerializeField] private GameObject Judul;
    void Start()
    {
        virus1.transform.DOMoveY(4.0f, 2.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutBack);

        virus2.transform.DOMoveY(-2.5f, 2.0f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutBack);

        virus3.transform.DOMoveY(-3.0f, 3.0f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutBack);

        Judul.transform.DOScale(0.9f, 3.0f)
            .SetEase(Ease.InOutBack)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void pauseGame(){
        Time.timeScale = 0;
    }
    public void PlayGame(){
        Time.timeScale = 1;
    }

    public void SceneLoader(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }

        public void Keluar()
        {
            Debug.Log ("KAMU TELAH KELUAR!");
            Application.Quit();
    }
}
