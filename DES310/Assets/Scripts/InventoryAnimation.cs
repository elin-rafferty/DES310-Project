using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAnimation : MonoBehaviour
{

    Animator anim;
    bool InventoryOpen = false;
    public EventHandler eventHandler;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!InventoryOpen)
        {
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                anim.Play("Open");
                InventoryOpen = true;
                eventHandler.InventoryChangeState.Invoke(true);
            }
        }

        else if (InventoryOpen)
        {
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                anim.Play("Close");
                InventoryOpen = false;
                eventHandler.InventoryChangeState.Invoke(false);
            }
        }

    }

}
