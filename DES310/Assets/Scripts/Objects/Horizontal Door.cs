using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HorizontalDoor : MonoBehaviour
{
    [SerializeField] InventorySO inventory;
    [SerializeField] SettingsSO settings;
    [SerializeField] Canvas canvas;
    [SerializeField] Text text;
    [SerializeField] GameObject doorLeft, doorRight;
    [SerializeField] bool isOpen = false, respondToTrigger = true;
    [SerializeField] float openSpeed = 1.5f, closeSpeed = 1.5f;
    bool animating = false;
    [SerializeField] bool locked = false;
    [SerializeField] ItemSO key;
    bool allowUnlocking = false;
    Vector3 moveAmount = new Vector3(1.92f, 0, 0);
    Vector3 offset = new Vector3(0.96f, 0, 0);
    InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        if (isOpen)
        {
            doorLeft.transform.position = transform.position - offset - moveAmount;
            doorRight.transform.position = transform.position + offset +  moveAmount;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (animating)
        {
            // Move doors
            doorLeft.transform.position += moveAmount * Time.deltaTime * (isOpen ? -openSpeed : closeSpeed);
            doorRight.transform.position += moveAmount * Time.deltaTime * (isOpen ? openSpeed : -closeSpeed);
            // Check if door is fully open
            if (isOpen && doorLeft.transform.position.x <= transform.position.x - offset.x - moveAmount.x && doorRight.transform.position.x >= transform.position.x + offset.x + moveAmount.x)
            {
                // Lock in place
                doorLeft.transform.position = transform.position - offset - moveAmount;
                doorRight.transform.position = transform.position + offset + moveAmount;
                animating = false;
            }
            // Check if door is fully closed
            else if (!isOpen && doorLeft.transform.position.x >= transform.position.x - offset.x && doorRight.transform.position.x <= transform.position.x + offset.x)
            {
                // Lock in place
                doorLeft.transform.position = transform.position - offset;
                doorRight.transform.position = transform.position + offset;
                animating = false;
            }
        }
        if (allowUnlocking)
        {
            if (inputManager.GetButtonDown("Interact"))
            {
                if (inventory.RemoveItem(key))
                {
                    Unlock();
                    Open();
                    canvas.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (respondToTrigger && (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player")))
        {
            Open();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (key != null && collision.gameObject.CompareTag("Player") && locked)
        {
            canvas.gameObject.SetActive(true);
            if (inventory.HasItem(key))
            {
                text.text = "Press " + (settings.Controls == 0 ? "E" : "X") + " to open";
                allowUnlocking = true;
            } else
            {
                text.text = "Requires " + key.Name + " to open";
            }
        } else if (key == null && collision.gameObject.CompareTag("Player") && locked)
        {
            canvas.gameObject.SetActive(true);
            text.text = "Locked";
        }
        if (respondToTrigger && (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player")))
        {
            Open();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && locked)
        {
            canvas.gameObject.SetActive(false);
            allowUnlocking = false;
        }
        if (respondToTrigger && (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player")))
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
