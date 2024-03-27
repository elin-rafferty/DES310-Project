using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionsMenu : MonoBehaviour
{
    // Menu objects
    private EventSystem eventSystem;
    [SerializeField] private GameObject mainMenu, optionsMenu, controlsMenu;
    [SerializeField] private GameObject optionsButton, backButton, controlsButton, controlsBackButton;

    // Settings
    [SerializeField] private SettingsSO settings;
    [SerializeField] private Dropdown graphics;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private TMP_Dropdown controls; 

    // Modifiers
    /*
    [SerializeField] private ModifierBehaviour modifierBehaviour;
    [SerializeField] private Slider spawnChanceSlider;
    [SerializeField] private Slider enemySpeedSlider;
    [SerializeField] private Slider enemyDamageSlider;
    [SerializeField] private Slider enemyHealthSlider;
    [SerializeField] private Slider enemyAggroRangeSlider;*/

    void Start()
    {
        eventSystem = FindAnyObjectByType<EventSystem>();
        /*
        spawnChanceSlider.value = modifierBehaviour.spawnPercentChance;
        enemySpeedSlider.value = modifierBehaviour.enemySpeedMultiplier;
        enemyDamageSlider.value = modifierBehaviour.enemyDamageMultiplier;
        enemyHealthSlider.value = modifierBehaviour.enemyHealthMultiplier;
        if (modifierBehaviour.enemyAggroRangeMultiplier < 1)
        {
            modifierBehaviour.enemyAggroRangeMultiplier = 1;
        }
        enemyAggroRangeSlider.value = modifierBehaviour.enemyAggroRangeMultiplier;
*/
        // Listen for slider value change
        masterVolumeSlider.onValueChanged.AddListener(delegate { SetMasterVolume(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });
        sfxVolumeSlider.onValueChanged.AddListener(delegate { SetSFXVolume(); });
        //graphics.onValueChanged.AddListener(delegate { SetQuality(); });


        // Get Saved Settings
        masterVolumeSlider.value = settings.MasterVolume;
        musicVolumeSlider.value = settings.MusicVolume;
        sfxVolumeSlider.value = settings.SFXVolume;
        //graphics.value = settings.Graphics;
        controls.value = settings.Controls;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            OpenMainMenu();
        }
        if (eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(backButton);
        }
    }

    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        eventSystem.SetSelectedGameObject(optionsButton);
    }

    public void OpenControlsMenu()
    {
        controlsMenu.SetActive(true);
        optionsMenu.SetActive(false);
        eventSystem.SetSelectedGameObject(controlsBackButton);
    }

    #region Volume Slider Functions

    // Change SO if slider changed
    public void SetMasterVolume ()
    {
        settings.MasterVolume = masterVolumeSlider.value;
    }
    public void SetMusicVolume()
    {
        settings.MusicVolume = musicVolumeSlider.value;
    }
    public void SetSFXVolume()
    {
        settings.SFXVolume = sfxVolumeSlider.value;
    }

    #endregion
/*
    public void SetQuality ()
    {
        settings.Graphics = graphics.value;
        //QualitySettings.SetQualityLevel(graphics.value);  
    }

    public void SetSpawnChance(float spawnPercentChance)
    {
        modifierBehaviour.spawnPercentChance = (int)spawnPercentChance;
    }

    public void SetEnemySpeed(float enemySpeed)
    {
        modifierBehaviour.enemySpeedMultiplier = enemySpeed;
    }
    public void SetEnemyDamage(float enemyDamage)
    {
        modifierBehaviour.enemyDamageMultiplier = enemyDamage;
    }
    public void SetEnemyHealth(float enemyHealth)
    {
        modifierBehaviour.enemyHealthMultiplier = enemyHealth;
    }
    public void SetEnemyAggroRange(float enemyAggroRange)
    {
        modifierBehaviour.enemyAggroRangeMultiplier = enemyAggroRange;
    }*/
    public void SetControls(TMP_Dropdown dropDown)
    {
        settings.Controls = dropDown.value;
    }

    public void PlaySelectSound()
    {
        SoundManager.instance.PlaySound(SoundManager.SFX.ButtonSelect, transform, 1f);
    }
}
