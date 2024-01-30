using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairManager : MonoBehaviour
{
    private GameObject crosshair;
    [SerializeField] GameObject crosshairPrefab;
    [SerializeField] EventHandler eventHandler;

    // Start is called before the first frame update
    void Start()
    {
        crosshair = Instantiate(crosshairPrefab);
        crosshair.SetActive(true);

        eventHandler.InventoryChangeState.AddListener(toggleCursor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void toggleCursor(bool value) 
    {

        if (value) 
        { 
            crosshair.SetActive(false); 
        }
        else 
        { 
            crosshair.SetActive(true);
        }
    }
}
