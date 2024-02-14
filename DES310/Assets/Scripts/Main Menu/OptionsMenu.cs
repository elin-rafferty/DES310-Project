using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
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
    }

    public void SetVolume (float volume)
      {
        audioMixer.SetFloat("volume", volume);
      }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);  
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
