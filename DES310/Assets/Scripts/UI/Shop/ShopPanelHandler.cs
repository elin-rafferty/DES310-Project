using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanelHandler : MonoBehaviour
{
    [SerializeField] Text title, description;
    [SerializeField] Image image;
    [SerializeField] BuyItem buyItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePanel();
    }

    void UpdatePanel()
    {
        title.text = buyItem.itemToBuy.Name;
        description.text = buyItem.itemToBuy.Description + "\n\nCosts: " + buyItem.currencyItem.Name + " x " + buyItem.cost;
        image.sprite = buyItem.itemToBuy.ItemImage;
    }
}
