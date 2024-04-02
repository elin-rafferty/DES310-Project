using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTutorialManager : MonoBehaviour
{
    [SerializeField] TutorialManager tutorialManager;
    [SerializeField] InputManager inputManager;
    [SerializeField] SettingsSO settings;
    [SerializeField] Canvas openInventoryCanvas, inventoryDescriptionCanvas, healCanvas, closeInventoryCanvas;
    [SerializeField] Text openInventoryText, inventoryDescriptionText, healText, closeInventoryText;
    [SerializeField] InventoryAnimation inventoryAnimation;
    [SerializeField] AgentWeapon agentWeapon;
    [SerializeField] Health playerHealth;

    int inventoryTutorialStage = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (inventoryTutorialStage)
        {
            case 0:
                PromptOpenInventory();
                break;
            case 1:
                InventoryDescription();
                break;
            case 2:
                UseHealthPack();
                break;
            case 3:
                PromptCloseInventory();
                break;
            case 4:
                tutorialManager.IncrementTutorialStage();
                gameObject.SetActive(false);
                break;
        }
    }

    void PromptOpenInventory()
    {
        if (inputManager.GetButtonDown("OpenInventory"))
        {
            inventoryAnimation.OpenInventory();
            inventoryAnimation.allowInventoryClose = false;
            inventoryTutorialStage++;
            openInventoryCanvas.gameObject.SetActive(false);
        } else
        {
            openInventoryCanvas.gameObject.SetActive(true);
            openInventoryText.text = "Press " + (settings.Controls == 0 ? "TAB" : "Y") + " to open your inventory.";
        }
    }

    void InventoryDescription()
    {
        if (agentWeapon.weapon == null)
        {
            if (settings.Controls == 0)
            {
                Cursor.visible = true;
            }
            inventoryDescriptionCanvas.gameObject.SetActive(true);
            //inventoryDescriptionText.text = "This is the inventory\nhere you can see the items you've scavenged\n\nYou can also equip or use items from here\n\n" + (settings.Controls == 0 ? "Right click " : "Press X ") + "on the pistol, then " + (settings.Controls == 0 ? "left click " : "press A ") + "on the equip button to equip it";
            inventoryDescriptionText.text = (settings.Controls == 0 ? "Right Click " : "Press X ") + "on the pistol, then " + (settings.Controls == 0 ? "Left Click " : "press A ") + "on the equip button to equip it.";
        } else
        {
            inventoryDescriptionCanvas.gameObject.SetActive(false);
            inventoryTutorialStage++;
        }
    }

    void UseHealthPack()
    {
        if (playerHealth.currentHealth < 100)
        {
            healCanvas.gameObject.SetActive(true);
            healText.text = "You seem hurt. " + (settings.Controls == 0 ? "Right Click " : "Press X ") + "on the health pack, then " + (settings.Controls == 0 ? "Left Click " : "press A ") + "on the use button to use it.";
        }
        else 
        {
            healCanvas.gameObject.SetActive(false);
            inventoryAnimation.allowInventoryClose = true;
            inventoryTutorialStage++;
        }
    }

    void PromptCloseInventory()
    {
        if (inventoryAnimation.InventoryOpen)
        {
            closeInventoryCanvas.gameObject.SetActive(true);
            closeInventoryText.text = "Close the inventory with " + (settings.Controls == 0 ? "Tab" : "Y or B") + ", or by pressing the cross.";
        } else
        {
            closeInventoryCanvas.gameObject.SetActive(false);
            inventoryTutorialStage++;
        }
    }
}
