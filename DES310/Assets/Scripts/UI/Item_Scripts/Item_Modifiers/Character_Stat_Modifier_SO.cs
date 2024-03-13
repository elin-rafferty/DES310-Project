using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character_Stat_Modifier_SO : ScriptableObject
{
    public abstract void AffectCharacter(GameObject character, float val);
}
