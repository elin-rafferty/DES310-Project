using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Vent_Script : MonoBehaviour
{
    public List<string> sceneNames = new();
    public float timeBeforeOpen = 2;
    public Persistent_Variables persistentVariables;
    public Input_Manager inputManager;
    private bool trigger = false;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Canvas canvas;
    [SerializeField] Text text;
    [SerializeField] Settings_SO settings;
    [SerializeField] Inventory_SO inventory;
    [SerializeField] List<Item_SO> keys = new();

    private void Start()
    {
        text.text = "Press " + (settings.Controls == 0 ? "E" : "X") + " to enter";
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
            text.text = "Press " + (settings.Controls == 0 ? "E" : "X") + " to enter";
        }
        // Load new scene
        if (inputManager.GetButtonDown("Interact") && trigger && timeBeforeOpen == 0)
        {
            foreach (Item_SO key in keys)
            {
                while (inventory.RemoveItem(key));
            }
            // Modifier Load Screen
            if (sceneNames[0] != "Main_Hub" && loadingScreen)
            {
                Modifiers_Enum newModifier;
                bool found;
                do
                {
                    found = false;
                    newModifier = (Modifiers_Enum)Random.Range(1, 6);

                    foreach (Modifiers_Enum m in persistentVariables.modifier)
                    {
                        if (m == newModifier)
                        {
                            found = true;
                        }
                    }
                }
                while (found == true || persistentVariables.modifier.Count == 7);
                persistentVariables.modifier.Add(newModifier);
                loadingScreen.SetActive(true);
            }
            persistentVariables.exitReason = Level_Exit_Reason.VENT_EXIT;
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
