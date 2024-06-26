using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerticalDoor : MonoBehaviour
{
    [SerializeField] InventorySO inventory;
    [SerializeField] SettingsSO settings;
    [SerializeField] Canvas canvas;
    [SerializeField] Text text;
    [SerializeField] GameObject doorTop, doorBottom;
    [SerializeField] bool isOpen = false, respondToTrigger = true;
    [SerializeField] float openSpeed = 1.5f, closeSpeed = 1.5f;
    bool animating = false;
    [SerializeField] bool locked = false;
    [SerializeField] ItemSO key;
    [SerializeField] Image keyImage;
    bool allowUnlocking = false;
    Vector3 moveAmount = new Vector3(0, 1.92f, 0);
    Vector3 offset = new Vector3(0, 0.96f, 0);
    InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        if (isOpen)
        {
            doorBottom.transform.position = transform.position - offset - moveAmount;
            doorTop.transform.position = transform.position + offset + moveAmount;
        }
        if (key != null)
        {
            keyImage.sprite = key.ItemImage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (animating)
        {
            // Move doors
            doorBottom.transform.position += moveAmount * Time.deltaTime * (isOpen ? -openSpeed : closeSpeed);
            doorTop.transform.position += moveAmount * Time.deltaTime * (isOpen ? openSpeed : -closeSpeed);
            // Check if door is fully open
            if (isOpen && doorBottom.transform.position.y <= transform.position.y - offset.y - moveAmount.y && doorTop.transform.position.y >= transform.position.y + offset.y + moveAmount.y)
            {
                // Lock in place
                doorBottom.transform.position = transform.position - offset - moveAmount;
                doorTop.transform.position = transform.position + offset + moveAmount;
                animating = false;
            }
            // Check if door is fully closed
            else if (!isOpen && doorBottom.transform.position.y >= transform.position.y - offset.y && doorTop.transform.position.y <= transform.position.y + offset.y)
            {
                // Lock in place
                doorBottom.transform.position = transform.position - offset;
                doorTop.transform.position = transform.position + offset;
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
                    keyImage.gameObject.SetActive(false);
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
                text.text = "Press " + (settings.Controls == 0 ? "E" : "A") + " to open";
                allowUnlocking = true;
            }
            else
            {
                text.text = "Requires " + key.Name + " to open";
                keyImage.gameObject.SetActive(true);
            }
        }
        else if (key == null && collision.gameObject.CompareTag("Player") && locked)
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
            keyImage.gameObject.SetActive(false);
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
            SoundManager.instance.PlaySound(SoundManager.SFX.DoorOpen, transform, 0.5f, true);
        }
    }

    public void Close()
    {
        if (isOpen)
        {
            isOpen = false;
            animating = true;
            SoundManager.instance.PlaySound(SoundManager.SFX.DoorClosed, transform, 0.5f, true);
        }
    }
}
