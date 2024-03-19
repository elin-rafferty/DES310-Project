using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum BuffType
{
    NO_OVERHEAT,
    SPEED_BOOST,
    SLOW_TIME,
    DAMAGE_BOOST
}

[Serializable]
public class Buff
{
    public BuffType type;
    public float time;
}
