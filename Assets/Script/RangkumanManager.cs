using UnityEngine;
using UnityEngine.UI;

public class RangkumanManager : MonoBehaviour
{
    public Button[] buttons;
    public Button finishButton;
    public Text statusText;

    private int clickedButtonsCount;

    private void Start()
    {
        // Menginisialisasi jumlah tombol yang diklik menjadi 0
        clickedButtonsCount = 0;

        // Menonaktifkan tombol "Selesai" pada awal permainan
        finishButton.interactable = false;

        // Menambahkan fungsi OnClick pada setiap tombol
        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i; // Menggunakan variabel lokal agar tidak terjadi perubahan nilai pada fungsi OnClick

            buttons[i].onClick.AddListener(() =>
            {
                OnButtonClick(buttonIndex);
            });
        }

        // Memperbarui status teks
        UpdateStatusText();
    }

    private void OnButtonClick(int buttonIndex)
    {
        // Menambah jumlah tombol yang diklik
        clickedButtonsCount++;

        // Menonaktifkan tombol yang diklik
        buttons[buttonIndex].interactable = false;

        // Memperbarui status teks
        UpdateStatusText();

        // Memeriksa apakah semua tombol sudah diklik
        if (clickedButtonsCount >= buttons.Length)
        {
            // Mengaktifkan tombol "Selesai"
            finishButton.interactable = true;

            // Mengubah teks status menjadi "Selesai. Silahkan klik tombol next"
            statusText.text = "Selesai. Silahkan klik tombol next";
        }
    }

    private void UpdateStatusText()
    {
        // Menampilkan teks status
        statusText.text = "Tekan semua tombol (" + clickedButtonsCount + " / " + buttons.Length + ")";
    }
}
