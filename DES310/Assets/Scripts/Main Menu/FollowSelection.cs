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
        transform.position = eventSystem.currentSelectedGameObject.transform.position + new Vector3(250, 0, 0);
    }
}
