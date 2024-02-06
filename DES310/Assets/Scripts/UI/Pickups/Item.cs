using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    private bool trigger = false;
    [SerializeField]
    InventorySO inventoryItems;

    public int MyProperty { get; set; }

    [field: SerializeField]
    public ItemSO InventoryItem { get; private set; }

    [field: SerializeField]
    public int Quantity { get; set; } = 1;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private float duration = 0.3f;

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
        if (Input.GetKeyDown(KeyCode.E) && trigger == true)
        {
            inventoryItems.AddItem(InventoryItem, 1);
            Destroy(gameObject);
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
