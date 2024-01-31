using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadCrumbList : MonoBehaviour
{
    [SerializeField] GameObject BreadCrumbPrefab;
    public List<GameObject> breadCrumbs = new();
    public int oldestCrumbIndex = 0;

    private float crumbCount = 50;
    private float crumbInterval = 0.1f;
    private float crumbTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < crumbCount; i++)
        {
            GameObject breadCrumb = Instantiate(BreadCrumbPrefab, transform.position, transform.rotation);
            breadCrumbs.Add(breadCrumb);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(crumbTimer >= crumbInterval)
        {
            crumbTimer = 0;

            breadCrumbs[oldestCrumbIndex].transform.position = transform.position;

            if (oldestCrumbIndex == crumbCount - 1)
            {
                oldestCrumbIndex = 0;
            }
            else
            {
                oldestCrumbIndex++;
            }
        }

        crumbTimer += Time.deltaTime;
    }
}