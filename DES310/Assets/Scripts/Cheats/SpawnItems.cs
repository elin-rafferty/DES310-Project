using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    [SerializeField] List<ItemSO> items = new();
    [SerializeField] InventorySO inventory;

    StringDetector stringDetector;
    // Start is called before the first frame update
    void Start()
    {
        stringDetector = GetComponent<StringDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stringDetector.active)
        {
            stringDetector.active = false;
            foreach (ItemSO item in items)
            {
                inventory.AddItem(item, item.MaxStackSize);
            }
        }
    }
}
