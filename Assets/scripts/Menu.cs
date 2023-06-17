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
    public Toggle vol;
    public AudioMixer mixer;

    private void Start()
    {
        vol.isOn = PlayerPrefs.GetInt("Volume", 1)==1;
        SetVolume(PlayerPrefs.GetInt("Volume", vol.isOn?1:0)==1);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        if (FindObjectOfType<RunManager>())
        {
            RunManager.CurrentRun = null;
            RunManager.currentlevel = 0;
        }

            SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        if (FindObjectOfType<RunManager>())
        {
            RunManager.CurrentRun = null;
            RunManager.currentlevel = 0;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public virtual void OptionsMenu()
    {
        Time.timeScale = Time.timeScale==0?1:0;
        options.SetActive(!options.activeSelf);
    }

    public void SetVolume(bool on)
    {
        mixer.SetFloat("Volume", on?0:100);
        PlayerPrefs.SetInt("Volume", on?1:0);
    }
}
