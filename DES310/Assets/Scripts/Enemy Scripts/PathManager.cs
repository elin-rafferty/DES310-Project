using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    List<GameObject> breadCrumbs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        breadCrumbs.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        breadCrumbs.Remove(collision.gameObject);
    }
}
