using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Persistent Variables")]
public class PersistentVariables : ScriptableObject
{
    public LevelExitReason exitReason = LevelExitReason.NONE;
    public EquippableItemSO equippedItem = null;
    public Modifiers modifier;

    public void OnEnable()
    {
        exitReason = LevelExitReason.NONE;
        equippedItem = null;
        modifier = 0;
    }
}