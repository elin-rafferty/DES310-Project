using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSightCheck : MonoBehaviour
{
    private bool lineOfSight;

    public bool isLineOfSight(GameObject target)
    {
        // Cast ray from enemy to traget
        RaycastHit2D[] ray = Physics2D.RaycastAll(transform.position, target.transform.position - transform.position);
        foreach (RaycastHit2D collision in ray)
        {
            if (collision.collider.gameObject.tag == target.tag)
            {
                // If first collision target, has LOS
                lineOfSight = true;
                break;
            }
            else if (collision.collider.gameObject.tag == gameObject.tag)
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

        return lineOfSight;
    }
}
