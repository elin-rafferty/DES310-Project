using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HubTutorialManager : MonoBehaviour
{
    [SerializeField] PersistentVariables persistentVariables;
    [SerializeField] GameObject inventoryTutorial;
    [SerializeField] PlayerMovement playermovement;
    [SerializeField] Health health;

    [SerializeField] InventorySO inventory;
    [SerializeField] List<ItemSO> tutorialItems = new();
    [SerializeField] List<int> itemQuantities = new();

    private void Awake()
    {
        for (int i = 0; i < tutorialItems.Count; i++)
        {
            inventory.AddItem(tutorialItems[i], itemQuantities[i]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (persistentVariables.tutorialStage == 0)
        {
            health.Damage(20);

            playermovement.enabled = false;
            inventoryTutorial.SetActive(true);
            UnityEngine.Cursor.visible = false;
        } else
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementTutorialStage()
    {
        persistentVariables.tutorialStage++;
        switch (persistentVariables.tutorialStage)
        {
            case 1:
                playermovement.enabled = true;
                gameObject.SetActive(false);
                break;
        }
    }
}
