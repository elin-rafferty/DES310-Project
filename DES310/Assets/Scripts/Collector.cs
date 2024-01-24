using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collison)
    {
        ICollectable collectable = collison.GetComponent<ICollectable>();
        if (collectable != null)
        {
            collectable.Collect();
        }
    }

}
