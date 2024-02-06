using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCheckable
{
    bool IsAggro { get; set; }
    bool IsWithinAttackRange { get; set; }

    void SetAggroStatus(bool aggroStatus);
    void SetWithinAttackRange(bool withinAttackRange);
}
