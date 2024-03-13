using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Persistent_Variables")]

public class Persistent_Variables : ScriptableObject
{
    
    public Level_Exit_Reason exitReason = Level_Exit_Reason.NONE;
    public Equippable_Item_SO equippedItem = null;
    public List<Modifiers_Enum> modifier;
    public string lastLevelEntered;


    public void OnEnable()
    {
        exitReason = Level_Exit_Reason.NONE;
        equippedItem = null;
        modifier = new();
        lastLevelEntered = "";
    }
}