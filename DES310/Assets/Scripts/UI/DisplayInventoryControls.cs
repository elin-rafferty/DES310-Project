using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventoryControls : MonoBehaviour
{
    [SerializeField] SettingsSO settings;
    [SerializeField] EventHandler eventHandler;
    [SerializeField] Text text;
    bool isInventoryOpen = false;
    // Start is called before the first frame update
    void Awake()
    {
        eventHandler.InventoryChangeState.AddListener(SetInventoryState);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInventoryOpen)
        {
            text.text = (settings.Controls == 0 ? "LMB" : "A") + ": Select Item\n" + (settings.Controls == 0 ? "RMB" : "A") + ": Use / Equip Item";
        } else
        {
            text.text = "";
        }
    }

    void SetInventoryState(bool open)
    {
        isInventoryOpen = open;
    }
}
