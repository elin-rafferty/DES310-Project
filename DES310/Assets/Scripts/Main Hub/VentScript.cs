using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VentScript : MonoBehaviour
{
    public List<string> sceneNames = new();
    public float timeBeforeOpen = 2;
    public PersistentVariables persistentVariables;
    public InputManager inputManager;
    private bool trigger = false;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Canvas canvas;
    [SerializeField] Text text;
    [SerializeField] SettingsSO settings;
    [SerializeField] InventorySO inventory;
    [SerializeField] List<ItemSO> keys = new();

    private void Start()
    {
        text.text = "Press " + (settings.Controls == 0 ? "E" : "A") + " to enter";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set trigger true on collision
            trigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set trigger false on collision end
            trigger = false;
        }
    }

    void Update()
    {
        if (timeBeforeOpen > 0)
        {
            timeBeforeOpen -= Time.deltaTime;
            if (timeBeforeOpen < 0)
            {
                timeBeforeOpen = 0;
            }
        }

        canvas.gameObject.SetActive(trigger && timeBeforeOpen == 0);
        if (canvas.gameObject.activeSelf)
        {
            text.text = "Press " + (settings.Controls == 0 ? "E" : "A") + " to enter";
        }
        // Load new scene
        if (inputManager.GetButtonDown("Interact") && trigger && timeBeforeOpen == 0 && Time.timeScale != 0)
        {
            foreach (ItemSO key in keys)
            {
                while (inventory.RemoveItem(key));
            }
            // Modifier Load Screen
            if (sceneNames[0] != "Main Hub" && loadingScreen)
            {
                Modifiers newModifier;
                newModifier = (Modifiers)Random.Range(1, 6);
                persistentVariables.modifier.Add(newModifier);
                loadingScreen.SetActive(true);
            }
            
            // Check if loading from tutorial
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                persistentVariables.exitReason = LevelExitReason.TUTORIAL;
            }
            else
            {
                persistentVariables.exitReason = LevelExitReason.VENT_EXIT;
            }

            if (sceneNames.Count > 1)
            {
                string newScene = "";
                do
                {
                    newScene = sceneNames[UnityEngine.Random.Range(0, sceneNames.Count)];
                } while (newScene == persistentVariables.lastLevelEntered);
                persistentVariables.lastLevelEntered = newScene;
                SceneManager.LoadScene(newScene);
            }
            else
            {
                SceneManager.LoadScene(sceneNames[0]);
            }
        }
    }
}
