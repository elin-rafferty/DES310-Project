using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Persistent Variables")]
public class PersistentVariables : ScriptableObject
{
    public LevelExitReason exitReason = LevelExitReason.NONE;
    public EquippableItemSO equippedItem = null;
    public List<Modifiers> modifier;
    public string lastLevelEntered;
    public int tutorialStage = 0, hotkeySlot1 = -1, hotkeySlot2 = -1;
    public bool hasForcedResolution = false;

    public void OnEnable()
    {
        exitReason = LevelExitReason.NONE;
        equippedItem = null;
        modifier = new();
        lastLevelEntered = "Communal Level";
        tutorialStage = 0;
        hotkeySlot1 = -1;
        hotkeySlot2 = -1;
        hasForcedResolution = false;
    }
}