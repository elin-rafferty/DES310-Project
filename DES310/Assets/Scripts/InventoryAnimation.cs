using Inventory.Model;
using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;

public class InventoryAnimation : MonoBehaviour
{

    Animator anim;
    public bool InventoryOpen = false;
    public EventHandler eventHandler;
    public InputManager inputManager;
    public inventoryMainPage inventoryMainPage;
    public GameObject virtualMouse;
    public VirtualMouseInput virtualMouseInput;
    public GameObject virtualMouseImage;
    public SettingsSO settings;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!InventoryOpen)
        {
            if (inputManager.GetButtonDown("OpenInventory"))
            {
                OpenInventory();
            }
        }

        else if (InventoryOpen)
        {
            if (inputManager.GetButtonDown("OpenInventory") || inputManager.GetButtonDown("Back"))
            {
                CloseInventory();
            }
        }

    }

    public void OpenInventory()
    {
        if (settings.Controls == 1)
        {
            virtualMouse.SetActive(true);
            Vector2 virtualMousePos = virtualMouseInput.virtualMouse.position.value;
            virtualMousePos.x = Screen.width / 2;
            virtualMousePos.y = Screen.height / 2;
            virtualMouseImage.transform.position = virtualMousePos;
            InputState.Change(virtualMouseInput.virtualMouse.position, virtualMousePos);
        }
        anim.Play("Open");
        InventoryOpen = true;
        eventHandler.InventoryChangeState.Invoke(true);
    }

    public void CloseInventory()
    {
        if (settings.Controls == 1)
        {
            Vector2 virtualMousePos = virtualMouseInput.virtualMouse.position.value;
            virtualMousePos.x = Screen.width / 2;
            virtualMousePos.y = Screen.height / 2;
            virtualMouseImage.transform.position = virtualMousePos;
            InputState.Change(virtualMouseInput.virtualMouse.position, virtualMousePos);
        }
        virtualMouse.SetActive(false);
        inventoryMainPage.HideItemAction();
        anim.Play("Close");
        InventoryOpen = false;
        eventHandler.InventoryChangeState.Invoke(false);
    }

}
