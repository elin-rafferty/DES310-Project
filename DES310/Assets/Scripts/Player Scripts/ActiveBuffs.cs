using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ActiveBuffs : ScriptableObject
{
    [SerializeField] public List<Buff> activeBuffs = new();

    private void OnEnable()
    {
        ResetBuffs();
    }

    public void ReduceTimers(float deltaTime)
    {
        foreach (var buff in activeBuffs)
        {
            buff.time -= deltaTime;
            if (buff.time < 0)
            {
                buff.time = 0;
                activeBuffs.Remove(buff);
                SoundManager.instance.PlaySound(SoundManager.SFX.PowerupDeactivated, GameObject.FindGameObjectWithTag("Player").transform, 1.0f);
            }
        }
    }

    public void ResetBuffs()
    {
        activeBuffs.Clear();
    }

    public void ApplyBuff(BuffType buffType, float time)
    {
        bool foundBuff = false;
        foreach (var buff in activeBuffs)
        {
            if (buff.type == buffType)
            {
                buff.time = time;
                foundBuff = true;
            }
        }
        if (!foundBuff)
        {
            Buff newBuff = new();
            newBuff.type = buffType;
            newBuff.time = time;
            activeBuffs.Add(newBuff);
        }
        SoundManager.instance.PlaySound(SoundManager.SFX.PowerupActivated, GameObject.FindGameObjectWithTag("Player").transform, 1.0f);
    }

    public bool IsBuffActive(BuffType buff)
    {
        foreach (var activeBuff in activeBuffs)
        {
            if (activeBuff.type == buff)
            {
                return activeBuff.time > 0;
            }
        }
        return false;
    }
}
