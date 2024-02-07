using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum playerstates
{
    silly,idle,devious,genocidal,george
}
public class meow : MonoBehaviour
{
    playerstates state = playerstates.silly;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case playerstates.silly:
                blehhhh();
                break;
        }
    }

    private void blehhhh()
    {
        
    }
}
