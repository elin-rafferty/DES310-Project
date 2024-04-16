using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeyListener : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    [SerializeField] Inventory.Model.InventorySO playerInventory;
    [SerializeField] int hotkeySlot1, hotkeySlot2;
    // Start is called before the first frame update
    void Start()
    {
        hotkeySlot1 = -1;
        hotkeySlot2 = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.GetButtonDown("Hotkey1"))
        {
            Debug.Log("Hotkey 1 pressed");
        }
        if (inputManager.GetButtonDown("Hotkey2"))
        {
            Debug.Log("Hotkey 2 pressed");
        }
    }
}
