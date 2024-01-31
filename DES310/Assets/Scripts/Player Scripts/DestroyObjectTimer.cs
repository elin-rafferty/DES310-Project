using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectTimer : MonoBehaviour
{
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 3)
        {
            Destroy(gameObject);
        }
        time += Time.deltaTime;
    }
}
