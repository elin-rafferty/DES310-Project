using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopTrigger : MonoBehaviour
{
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
        // Load shop menu
        if (Input.GetKeyDown(KeyCode.E) && trigger == true)
        {
            // Put code to do that here
            Debug.Log("The shop will open here eventually");
        }
    }
}
