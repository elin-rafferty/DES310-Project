using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Loot Table", menuName = "Data/Loot Table")]
public class LootTable : ScriptableObject
{
    public int lootRolls = 1;
    public int dropchance = 100;
    public float itemQuantityVariation = 0f;
    public List<InventoryItem> items = new();
    public List<int> weightings = new();
    
    public List<InventoryItem> getLoot()
    {
        List<InventoryItem> itemsRolled = new();
        // Check modifier has been set up correctly
        if (items.Count != weightings.Count)
        {
            Debug.Log("Loot table has not been set up correctly. Needs equal number of items to roll and weightings");
        }
        else
        {
            for (int rolls = 0; rolls < lootRolls; rolls++)
            {
                int totalWeights = 0;
                // Create and fill list of weightings for each item with upper and lower bounds
                List<Tuple<int, int>> weightingRanges = new List<Tuple<int, int>>();
                foreach (int weighting in weightings)
                {
                    weightingRanges.Add(new Tuple<int, int>(totalWeights, totalWeights + weighting));
                    totalWeights += weighting;
                }
                // Choose a random number within total weighting range
                int random = UnityEngine.Random.Range(0, totalWeights);
                // Find where that random number landed
                for (int i = 0; i < weightingRanges.Count; i++)
                {
                    if (random >= weightingRanges[i].Item1 && random < weightingRanges[i].Item2)
                    {
                        // Add that item to the rolls
                        InventoryItem newItem = new();
                        newItem.quantity = items[i].quantity;
                        if (itemQuantityVariation != 0f)
                        {
                            float newQuantity = newItem.quantity;
                            newQuantity *= UnityEngine.Random.Range(1 -  itemQuantityVariation, 1 + itemQuantityVariation);
                            newItem.quantity = (int)MathF.Round(newQuantity);
                        }
                        newItem.item = items[i].item;
                        newItem.itemState = items[i].itemState;
                        if (newItem.item != null && newItem.quantity != 0)
                        {
                            itemsRolled.Add(newItem);
                        }
                    }
                }
            }
        }
        return itemsRolled;
    }
}