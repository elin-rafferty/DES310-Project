using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalDoor : MonoBehaviour
{
    [SerializeField] GameObject doorTop, doorBottom;
    [SerializeField] bool isOpen = false, respondToTrigger = true;
    [SerializeField] float openSpeed = 1.5f, closeSpeed = 1.5f;
    bool animating = false;
    [SerializeField] bool locked = false;
    Vector3 moveAmount = new Vector3(0, 1.92f, 0);
    Vector3 offset = new Vector3(0, 0.96f, 0);
    // Start is called before the first frame update
    void Start()
    {
        if (isOpen)
        {
            doorBottom.transform.position = transform.position - offset - moveAmount;
            doorTop.transform.position = transform.position + offset + moveAmount;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (animating)
        {
            doorBottom.transform.position += moveAmount * Time.deltaTime * (isOpen ? -openSpeed : closeSpeed);
            doorTop.transform.position += moveAmount * Time.deltaTime * (isOpen ? openSpeed : -closeSpeed);
            if (isOpen && doorBottom.transform.position.y <= transform.position.y - offset.y - moveAmount.y && doorTop.transform.position.y >= transform.position.y + offset.y + moveAmount.y)
            {
                doorBottom.transform.position = transform.position - offset - moveAmount;
                doorTop.transform.position = transform.position + offset + moveAmount;
                animating = false;
            }
            else if (!isOpen && doorBottom.transform.position.y >= transform.position.y - offset.y && doorTop.transform.position.y <= transform.position.y + offset.y)
            {
                doorBottom.transform.position = transform.position - offset;
                doorTop.transform.position = transform.position + offset;
                animating = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO: Implement keycard prompt
        if (respondToTrigger)
        {
            Open();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (respondToTrigger)
        {
            Close();
        }
    }

    public void Unlock()
    {
        // TODO: Implement keycard logic
        locked = false;
    }

    public void Lock()
    {
        locked = true;
    }

    public void Open()
    {
        if (!isOpen && !locked)
        {
            isOpen = true;
            animating = true;
        }
    }

    public void Close()
    {
        if (isOpen)
        {
            isOpen = false;
            animating = true;
        }
    }
}
