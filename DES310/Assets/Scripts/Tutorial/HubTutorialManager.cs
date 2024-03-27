using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubTutorialManager : MonoBehaviour
{
    [SerializeField] PersistentVariables persistentVariables;
    [SerializeField] GameObject inventoryTutorial;
    [SerializeField] PlayerMovement playermovement;
    // Start is called before the first frame update
    void Start()
    {
        if (persistentVariables.tutorialStage == 0)
        {
            playermovement.enabled = false;
            inventoryTutorial.SetActive(true);
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
                playermovement.enabled = true;
                gameObject.SetActive(false);
                break;
        }
    }
}
