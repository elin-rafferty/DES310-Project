using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] PersistentVariables persistentVariables;
    [SerializeField] PlayerMovement playermovement;
    [SerializeField] Health health;
    [SerializeField] InventorySO inventory;

    [SerializeField] List<GameObject> tutorials;
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
            tutorials[0].SetActive(true);
            Cursor.visible = false;
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
                // Start Enemy Tutorial
                playermovement.enabled = true;
                tutorials[persistentVariables.tutorialStage - 1].SetActive(false);
                tutorials[persistentVariables.tutorialStage].SetActive(true);
                break;
            case 2:
                // Start UI Tutorial
                tutorials[persistentVariables.tutorialStage - 1].SetActive(false);
                tutorials[persistentVariables.tutorialStage].SetActive(true);
                break;
            case 3:
                // Start Crate Tutorial
                tutorials[persistentVariables.tutorialStage - 1].SetActive(false);
                tutorials[persistentVariables.tutorialStage].SetActive(true);
                break;
            case 4:
                // Start Vent Tutorial
                tutorials[persistentVariables.tutorialStage - 1].SetActive(false);
                tutorials[persistentVariables.tutorialStage].SetActive(true);
                break;
            case 5:
                // End Tutorial
                tutorials[persistentVariables.tutorialStage - 1].SetActive(false);
                gameObject.SetActive(false);
                break;
        }
    }
}
