using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FollowSelection : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = eventSystem.currentSelectedGameObject.transform.position + new Vector3(250, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (eventSystem.currentSelectedGameObject != null)
        {
            transform.position = eventSystem.currentSelectedGameObject.transform.position + new Vector3(250, 0, 0);
        } else
        {
            transform.position = new Vector3(10000, 10000);
        }
    }
}
