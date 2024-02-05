using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class LineOfSightCheck : MonoBehaviour
{
    private bool lineOfSight;

    public bool isLineOfSight(GameObject target, GameObject source)
    {
        // Cast ray from enemy to traget
        RaycastHit2D[] ray = Physics2D.RaycastAll(source.transform.position, target.transform.position - source.transform.position);
        foreach (RaycastHit2D collision in ray)
        {
            if (target.CompareTag(collision.collider.gameObject.tag))
            {
                // If first collision target, has LOS
                lineOfSight = true;
                break;
            }
            else if (collision.collider.gameObject.CompareTag("Enemy"))
            {
                // Ignore objects with same tag colliders
                continue;
            }
            else
            {
                // If any collision before target enemy, hasn't got LOS
                lineOfSight = false;
                break;
            }
        }

        if (lineOfSight)
        {
            Debug.DrawRay(source.transform.position, target.transform.position - source.transform.position, Color.green);
        }
        else
        {
            Debug.DrawRay(source.transform.position, target.transform.position - source.transform.position, Color.red);
        }

        return lineOfSight;
    }
}
