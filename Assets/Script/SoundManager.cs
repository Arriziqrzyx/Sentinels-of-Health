using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider BGM;
    [SerializeField] Slider SFX;
    public static SoundManager Instance { get; set; }

    private void Awake()
    {
        // Inisialisasi singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        float db;

        // Mengatur nilai awal slider berdasarkan nilai mixer
        if (audioMixer.GetFloat("BGMVol", out db))
            BGM.value = (db + 80) / 80;

        if (audioMixer.GetFloat("SFXVol", out db))
            SFX.value = (db + 80) / 80;
    }

    public void SetBGMVolume(float volume)
    {
        volume = volume * 80 - 80;
        audioMixer.SetFloat("BGMVol", volume);
        PlayerPrefs.SetFloat("BGMVol", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        volume = volume * 80 - 80;
        audioMixer.SetFloat("SFXVol", volume);
        PlayerPrefs.SetFloat("SFXVol", volume);
        PlayerPrefs.Save();
    }
}
