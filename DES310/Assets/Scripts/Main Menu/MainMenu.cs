using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu, optionsMenu;
    [SerializeField] private PersistentVariables persistentVariables;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject playButton, optionsBackButton;
    [SerializeField] private SettingsSO defaultSettings;
    [SerializeField] private SoundMixerManager soundMixer;

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
        persistentVariables.exitReason = LevelExitReason.NONE;
        SceneManager.LoadScene("Main Hub");
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
        SoundManager.instance.PlaySound(SoundManager.SFX.ButtonSelect, transform, 1f);
    }
}
