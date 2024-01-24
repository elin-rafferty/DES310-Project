using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour, ICollectable
{
    public static event ItemCollected OnItemCollected;
    public delegate void ItemCollected(ItemData item);
    public ItemData collectableData;

    public void Collect()
    {
        OnItemCollected?.Invoke(collectableData);
        Destroy(gameObject);
    }
}   
