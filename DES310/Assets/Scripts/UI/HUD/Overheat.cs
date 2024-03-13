using Inventory.Model;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overheat : MonoBehaviour
{ 

    [SerializeField]
     private Overheat overheat;

    [SerializeField] 
    private EventHandler eventHandler;


    // Start is called before the first frame update
    void Awake()
    {
        overheat.Show();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    { 
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (overheat.isActiveAndEnabled == true)
            {
                overheat.Hide();
                eventHandler.InventoryChangeState.Invoke(false);
            }
            else
            {
                if (overheat.isActiveAndEnabled == false)
                    overheat.Show();
                    eventHandler.InventoryChangeState.Invoke(true);
            }
        }
    }
}
