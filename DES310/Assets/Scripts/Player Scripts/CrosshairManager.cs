using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairManager : MonoBehaviour
{
    [SerializeField] GameObject crosshair;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(crosshair);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
