using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "EventHandler - Do Not Make")]
public class EventHandler : ScriptableObject
{
    public UnityEvent<bool> InventoryChangeState;
    public UnityEvent PlayerDeath;
    public UnityEvent<float> PlayerHealthChange;
    public UnityEvent<string> PlaySound;
    public UnityEvent<int> ChangeEnemyCount;
    public UnityEvent LevelEnter;
    public UnityEvent PlayerBulletFired;
    public UnityEvent<float, float> ShakeCamera;
    public Persistent_Variables persistentVariables;

    private void OnEnable()
    {
        PlayerDeath.AddListener(PlayerDeathResponse);
    }

    void PlayerDeathResponse()
    {
        Debug.Log("Player died");
        
        persistentVariables.exitReason = Level_Exit_Reason.DEATH;
        persistentVariables.modifier.Clear();
    }
}
