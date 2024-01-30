using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EventHandler - Do Not Make")]
public class EventHandler : ScriptableObject
{
    public UnityEvent<bool> InventoryChangeState;
}
