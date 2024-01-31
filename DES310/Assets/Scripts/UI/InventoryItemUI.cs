using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class InventoryItemUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler

{
    // https://www.youtube.com/watch?v=DS5Ss9SFHbs&list=PLcRSafycjWFegXSGBBf4fqIKWkHDw_G8D&index=7
    public ItemSO itemData;
    public int stackSize;

    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private TMP_Text quantityTxt;

    [SerializeField]
    private Image borderImage;

    public event Action<InventoryItemUI> OnItemClicked, OnItemDropped, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;

    private bool empty = true;

    public void Awake()
    {
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        empty = true;
    }

    public void Deselect()
    {
        borderImage.enabled = false;
    }

    public void SetData(Sprite sprite, int quantity)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.quantityTxt.text = quantity + "";
        empty = false;
    }

    public void Select()
    {
        borderImage.enabled = true;
    }

    // https://youtu.be/geq7lQSBDAE?si=nyx-AL0fl1oXcjaf
    public InventoryItemUI(ItemSO item)
    {
        itemData = item;
        AddToStack();
    }

    public InventoryItemUI()
    {
        itemData = null;
        stackSize = 0; 
    }

    public void AddToStack()
    {
        stackSize++;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (empty)
            return;
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this); 
    }


    public void OnDrag(PointerEventData eventData)
    {
 
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        OnItemDropped?.Invoke(this);
    }

    
    
}
