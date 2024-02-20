using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Persistent Variables")]
public class PersistentVariables : ScriptableObject
{
    public LevelExitReason exitReason = LevelExitReason.NONE;
}