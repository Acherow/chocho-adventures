using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class Menu : MonoBehaviour
{
    public GameObject options;
    public Slider vol;
    public AudioMixer mixer;

    private void Start()
    {
        vol.value = PlayerPrefs.GetFloat("Volume", vol.value);
        SetVolume(PlayerPrefs.GetFloat("masterVolume", vol.value));
    }

    public void StartGame()
    {
        if (FindObjectOfType<RunManager>())
            RunManager.CurrentRun = null;

        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public virtual void OptionsMenu()
    {
        Time.timeScale = 0;
        options.SetActive(!options.activeSelf);
    }

    public void SetVolume(float vol)
    {
        //mixer.SetFloat("Vol", Mathf.Log10(vol) * 20);
        mixer.SetFloat("Volume", vol);
        PlayerPrefs.SetFloat("Volume", vol);
    }
}
