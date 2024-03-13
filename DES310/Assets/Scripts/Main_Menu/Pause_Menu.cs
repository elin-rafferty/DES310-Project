using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu, optionsMenu;
    [SerializeField] private Persistent_Variables persistentVariables;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject playButton, optionsBackButton;
    [SerializeField] private Settings_SO settings;
    [SerializeField] private Inventory_SO inventory;
    [SerializeField] private List<Weapon_Properties> weaponProperties;

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
        foreach (Weapon_Properties weapon in weaponProperties)
        {
            weapon.weaponUpgrades = Weapon_Upgrades.CreateInstance<Weapon_Upgrades>();
        }
        persistentVariables.exitReason = Level_Exit_Reason.NONE;
        persistentVariables.lastLevelEntered = "";
        SceneManager.LoadScene("Main Menu");
    }

    public void PlaySelectSound()
    {
        Sound_Manager.instance.PlaySound(Sound_Manager.SFX.ButtonSelect, transform, 1f);
    }
}
