using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBreadCrumbs : MonoBehaviour
{
    [SerializeField] GameObject BreadCrumbPrefab;
    private Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnBreadCrumb", 1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBreadCrumb()
    {
        if (transform.position != lastPos)
        {
            Instantiate(BreadCrumbPrefab, transform.position, transform.rotation);
        }
        lastPos = transform.position;
    }
}
