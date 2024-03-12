using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu, optionsMenu;
    [SerializeField] private PersistentVariables persistentVariables;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject playButton, optionsBackButton;
    [SerializeField] private SettingsSO settings;
    [SerializeField] private InventorySO inventory;
    [SerializeField] private List<WeaponProperties> weaponProperties;

    private void OnEnable()
    {
        if (settings.Controls == 0)
        {
            Cursor.visible = true;
        } else
        {
            eventSystem.SetSelectedGameObject(playButton);
        }
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(playButton);
        }
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
        eventSystem.SetSelectedGameObject(playButton);
        Time.timeScale = 1f;
    }

    public void OpenOptionsMenu()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        eventSystem.SetSelectedGameObject(optionsBackButton);
    }

    public void ReturnToMenu() 
    {
        inventory.WipeInventory();
        foreach (WeaponProperties weapon in weaponProperties)
        {
            weapon.weaponUpgrades = WeaponUpgrades.CreateInstance<WeaponUpgrades>();
        }
        persistentVariables.exitReason = LevelExitReason.NONE;
        persistentVariables.lastLevelEntered = "";
        SceneManager.LoadScene("Main Menu");
    }

    public void PlaySelectSound()
    {
        SoundManager.instance.PlaySound(SoundManager.SFX.ButtonSelect, transform, 1f);
    }
}
