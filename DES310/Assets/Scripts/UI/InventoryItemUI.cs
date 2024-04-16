using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using Inventory.Model;

namespace Inventory.UI
{
    public class InventoryItemUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        // https://www.youtube.com/watch?v=DS5Ss9SFHbs&list=PLcRSafycjWFegXSGBBf4fqIKWkHDw_G8D&index=7
        public ItemSO item;
        public int quantity;

        [SerializeField]
        public Image itemImage;

        [SerializeField]
        private Text quantityTxt;

        [SerializeField]
        private Image borderImage;

        public event Action<InventoryItemUI> OnItemClicked, OnItemDropped, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick, OnMouseHover;
        public event Action OnMouseStopHover;

        public bool empty = true;



        public void Awake()
        {
            ResetData();
            Deselect();

        }

        public void ResetData()
        {
            itemImage.gameObject.SetActive(false);
            empty = true;
        }

        public void Deselect()
        {
            borderImage.enabled = false;
        }

        public void SetData(Sprite sprite, int quantity)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            quantityTxt.text = quantity + "";
            empty = false;
        }

        public void Select()
        {
            // Play Select Sound
            SoundManager.instance.PlaySound(SoundManager.SFX.ButtonSelect, transform, 1f);

            borderImage.enabled = true;
        }

        // https://youtu.be/geq7lQSBDAE?si=nyx-AL0fl1oXcjaf
        public InventoryItemUI(ItemSO item)
        {
            AddToStack();
        }

        public InventoryItemUI()
        {
            item = null;
            quantity = 0;
        }

        public void AddToStack()
        {
            quantity++;
        }

        public void RemoveFromStack()
        {
            quantity--;
        }

        public void OnPointerClick(PointerEventData pointerData)
        {
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                
            }
            else
            {
                OnItemClicked?.Invoke(this);
                OnRightMouseBtnClick?.Invoke(this);
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

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnMouseHover?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnMouseStopHover?.Invoke();
        }
    }
}