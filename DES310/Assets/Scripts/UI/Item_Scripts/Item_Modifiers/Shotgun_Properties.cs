using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Properties/Shotgun Properties")]
public class Shotgun_Properties : Weapon_Properties
{
    public float sprayAngle = 20;
    public override void Fire(Vector2 position, Vector2 direction, GameObject owner)
    {
        float leftAngle = Mathf.Atan2(direction.y, direction.x) - sprayAngle * Mathf.Deg2Rad;
        Vector2 leftDirection = new Vector2(Mathf.Cos(leftAngle), Mathf.Sin(leftAngle));
        float rightAngle = Mathf.Atan2(direction.y, direction.x) + sprayAngle * Mathf.Deg2Rad;
        Vector2 rightDirection = new Vector2(Mathf.Cos(rightAngle), Mathf.Sin(rightAngle));
        // Fire
        base.Fire(position, leftDirection, owner);
        base.Fire(position, direction, owner);
        base.Fire(position, rightDirection, owner);
    }
}
