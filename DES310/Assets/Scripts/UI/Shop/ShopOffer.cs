using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopOffer
{
    public ItemSO itemToBuy;
    public int quantityToBuy = 1;
    public ItemSO currencyItem;
    public int cost = 1;
    public int weighting = 1;
}
