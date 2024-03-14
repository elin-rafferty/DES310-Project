using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCloseInventoryButton : MonoBehaviour
{
    [SerializeField] EventHandler eventHandler;
    [SerializeField] GameObject closeInventoryButton;
    bool isInventoryOpen = false;
    // Start is called before the first frame update
    void Awake()
    {
        eventHandler.InventoryChangeState.AddListener(InventoryStateChangeResponse);
    }

    // Update is called once per frame
    void Update()
    {
        closeInventoryButton.SetActive(isInventoryOpen);
    }

    void InventoryStateChangeResponse(bool isOpen)
    {
        isInventoryOpen = isOpen;
    }
}
