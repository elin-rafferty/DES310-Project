using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Follow : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Camera mainCam;

    [SerializeField]
    private Inventory_Item_UI item;
  
    private void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        mainCam = Camera.main;
        item = GetComponentInChildren<Inventory_Item_UI>();
    }

    public void SetData(Sprite sprite, int quantity)
    {
        //item.SetData(sprite, quantity);
    }

    void Update() 
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            Input.mousePosition,
            canvas.worldCamera,
            out position
                );
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val)
    {
        Debug.Log($"Item toggled {val}");
        gameObject.SetActive(val);
    }
}
