using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu, optionsMenu;
    [SerializeField] private Persistent_Variables persistentVariables;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject playButton, optionsBackButton;
    [SerializeField] private Settings_SO defaultSettings;
    [SerializeField] private Sound_Mixer_Manager soundMixer;

    private void Start()
    {
        Cursor.visible = true;
        eventSystem.SetSelectedGameObject(playButton);

        soundMixer.SetMasterVolume(defaultSettings.MasterVolume);
        soundMixer.SetSFXVolume(defaultSettings.SFXVolume);
        soundMixer.SetMusicVolume(defaultSettings.MusicVolume);
    }

    private void Update()
    {
        if (eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(playButton);
        }
    }

    public void PlayGame()
    {
        persistentVariables.exitReason = Level_Exit_Reason.NONE;
        SceneManager.LoadScene("Main_Hub");
        Time.timeScale = 1.0f;
    }

    public void OpenOptionsMenu()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
        eventSystem.SetSelectedGameObject(optionsBackButton);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    public void PlaySelectSound()
    {
        Sound_Manager.instance.PlaySound(Sound_Manager.SFX.ButtonSelect, transform, 1f);
    }
}
