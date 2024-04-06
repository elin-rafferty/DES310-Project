using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringDetector : MonoBehaviour
{
    [SerializeField] string targetString;
    private char[] charArray;
    private int count;

    public bool active = false;

    void Start()
    {
        count = 0;
        charArray = targetString.ToCharArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) 
        {
            foreach (char c in Input.inputString) 
            {
                if (charArray[count] == char.ToLowerInvariant(c))
                {
                    count++;

                    if (count == charArray.Length)
                    {
                        active = !active;
                        count = 0;
                    }
                }
                else
                {
                    count = 0;
                }
            }
        }
    }
}
