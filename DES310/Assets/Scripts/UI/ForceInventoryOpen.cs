using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceInventoryOpen : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inventory.SetActive(true);
    }
}
