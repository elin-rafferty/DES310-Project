using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAnimation : MonoBehaviour
{

    Animator anim;
    bool InventoryOpen = false;
    public EventHandler eventHandler;
    public InputManager inputManager;

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
            if (inputManager.GetButtonDown("OpenInventory"))
            {
                anim.Play("Open");
                InventoryOpen = true;
                eventHandler.InventoryChangeState.Invoke(true);
            }
        }

        else if (InventoryOpen)
        {
            if (inputManager.GetButtonDown("OpenInventory") || inputManager.GetButtonDown("Back"))
            {
                anim.Play("Close");
                InventoryOpen = false;
                eventHandler.InventoryChangeState.Invoke(false);
            }
        }

    }

}
