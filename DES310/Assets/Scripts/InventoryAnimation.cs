using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAnimation : MonoBehaviour
{

    Animator anim;
    bool InventoryOpen = false;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InventoryOpen != true)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                anim.Play("Open");
                InventoryOpen = true;
            }
        }

        else if (InventoryOpen != false)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                anim.Play("Close");
                InventoryOpen = false;
            }
        }

    }

}
