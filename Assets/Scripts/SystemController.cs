using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SystemController : MonoBehaviour
{
    public GameObject SettingsMenu;
    public Text SettingsTitle;
    public GameObject ControlMenu;
    public GameObject AudioMenu;

    public Slider Sound;
    public Slider Music;

    // Use this for initialization
    void Start()
    {
        if (Sound) Sound.value = PlayerPrefs.GetFloat("Sound", 0.5f);
        if(Music) Music.value = PlayerPrefs.GetFloat("Music", 0.5f);
        if(SettingsMenu) SettingsMenu.SetActive(false);
        if(ControlMenu) ControlMenu.SetActive(false);
        if(AudioMenu) AudioMenu.SetActive(false);
    }

    public void OnValueChanged()
    {
        PlayerPrefs.SetFloat("Sound", Sound.value);
        PlayerPrefs.SetFloat("Music", Music.value);
    }

    public void ToggleSettingsMenu()
    {
        SettingsTitle.text = "SETTINGS";
        SettingsMenu.SetActive(!SettingsMenu.activeSelf);
    }

    public void EnableAudioMenu()
    {
        SettingsTitle.text = "AUDIO";
        AudioMenu.SetActive(true);
    }

    public void DisableSubMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
}
