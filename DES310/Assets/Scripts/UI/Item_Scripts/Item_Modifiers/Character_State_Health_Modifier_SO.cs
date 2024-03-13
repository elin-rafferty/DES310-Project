using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Character_State_Health_Modifier_SO : Character_Stat_Modifier_SO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        Health health = character.GetComponent<Health>();
        if (health != null)
            health.Damage(-(int)val);
    }
}
