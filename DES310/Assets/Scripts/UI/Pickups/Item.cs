using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Item : MonoBehaviour
{
    private bool trigger = false;
    [SerializeField]
    InventorySO inventoryItems;

    public int MyProperty { get; set; }

    [field: SerializeField]
    public ItemSO InventoryItem { get; set; }

    [field: SerializeField]
    public int Quantity { get; set; } = 1;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private float duration = 0.3f;

    public bool hasDestination = false;
    public Vector2 destination = Vector2.zero;
    private float moveSpeed = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Set trigger true on collision with item
        trigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Set trigger false on exit with item
        trigger = false;
    }

    void Update()
    {
        // Pick up item
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton2)) && trigger && !hasDestination)
        {
            // Play Pickup Sound
            SoundManager.instance.PlaySound(SoundManager.SFX.ItemPickUp, transform, 1f);

            Destroy(gameObject);
            inventoryItems.AddItem(InventoryItem, Quantity);
        }
    }

    private void FixedUpdate()
    {

        // Move towards destination
        if (hasDestination)
        {
            Vector2 position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Vector2 distance = destination - position;
            Vector2 movement = distance.normalized * moveSpeed * Time.deltaTime;
            if (distance.sqrMagnitude > movement.sqrMagnitude)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + movement.x, gameObject.transform.position.y + movement.y, gameObject.transform.position.z);
            } else
            {
                gameObject.transform.position = new Vector3(destination.x, destination.y, gameObject.transform.position.z);
                hasDestination = false;
            }
        }
    }

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = InventoryItem.ItemImage;
    }

    internal void DestoryItem()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }

    private IEnumerator AnimateItemPickup()
    {
        audioSource.Play();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;
        }
        transform.localScale = endScale;
        Destroy(gameObject);
    }

}
