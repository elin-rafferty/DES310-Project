using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    // Settings
    [SerializeField] private SettingsSO settings;
    [SerializeField] private Dropdown graphics;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    // Modifiers
    [SerializeField] private ModifierBehaviour modifierBehaviour;
    [SerializeField] private Slider spawnChanceSlider;
    [SerializeField] private Slider enemySpeedSlider;
    [SerializeField] private Slider enemyDamageSlider;
    [SerializeField] private Slider enemyHealthSlider;
    [SerializeField] private Slider enemyAggroRangeSlider;

    void Start()
    {
        spawnChanceSlider.value = modifierBehaviour.spawnPercentChance;
        enemySpeedSlider.value = modifierBehaviour.enemySpeedMultiplier;
        enemyDamageSlider.value = modifierBehaviour.enemyDamageMultiplier;
        enemyHealthSlider.value = modifierBehaviour.enemyHealthMultiplier;
        if (modifierBehaviour.enemyAggroRangeMultiplier < 1)
        {
            modifierBehaviour.enemyAggroRangeMultiplier = 1;
        }
        enemyAggroRangeSlider.value = modifierBehaviour.enemyAggroRangeMultiplier;

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
    }
}
