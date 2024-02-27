using Inventory.Model;
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
    
}