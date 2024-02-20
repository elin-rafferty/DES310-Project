using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VentScript : MonoBehaviour
{
    public string sceneName;
    public PersistentVariables persistentVariables;
    private bool trigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Set trigger true on collision with item
        trigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Set trigger false on exit with item
        trigger = false;
    }

    void Update()
    {
        // Pick up item
        if (Input.GetKeyDown(KeyCode.E) && trigger == true)
        {
            persistentVariables.exitReason = LevelExitReason.VENT_EXIT;
            SceneManager.LoadScene(sceneName);
        }
    }
}
