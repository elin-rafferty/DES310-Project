using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopStockRandomiser : MonoBehaviour
{
    [SerializeField] List<BuyItem> buyItems = new();
    [SerializeField] ShopStockTable stockTable;

    private void Awake()
    {
        List<ShopOffer> offers = stockTable.GenerateOffers();
        for (int i = 0; i < offers.Count; i++)
        {
            buyItems[i].itemToBuy = offers[i].itemToBuy;
            buyItems[i].quantity = offers[i].quantityToBuy;
            buyItems[i].cost = offers[i].cost;
            buyItems[i].currencyItem = offers[i].currencyItem;
        }
    }
}
