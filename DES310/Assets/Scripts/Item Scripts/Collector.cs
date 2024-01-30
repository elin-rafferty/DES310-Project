using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private bool trigger =  false;
    ICollectable collectable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Set trigger true on collision with item
        collectable = collision.GetComponent<ICollectable>();
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
            if (collectable != null)
            {
                collectable.Collect();
            }
        }
    }
}
