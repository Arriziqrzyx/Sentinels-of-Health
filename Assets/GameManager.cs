using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Komponen Image yang ingin diaktifkan di menu
    public Image finishObjectMenuImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Metode untuk mengaktifkan atau menonaktifkan komponen Image
    public void SetFinishObjectActive(bool isActive)
    {
        if (finishObjectMenuImage != null)
        {
            finishObjectMenuImage.enabled = isActive;
            // Tambahan operasi lain jika diperlukan saat mengaktifkan atau menonaktifkan komponen Image
            // ...
        }
    }
}
