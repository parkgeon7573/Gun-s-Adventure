using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour, IPointerUpHandler
{
    public int slotnum;
    public Item item;
    public Image itemIcon;
    InventoryUI inventoryUI;

    public void Init(InventoryUI Iui)
    {
        inventoryUI = Iui;
    }
    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
    }
    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(item != null)
        {
            if (ItemDatabase.instance.money >= item.itemCost && Inventory.instance.items.Count < Inventory.instance.SlotCnt)
            {
                for (int i = 0; i < inventoryUI.slots.Length; i++)
                {
                    if (inventoryUI.slots[i].item == null)
                        break;
                    if (item.itemName == inventoryUI.slots[i].item.itemName)
                    {
                        return;
                    }
                }
                ItemDatabase.instance.money -= item.itemCost;
                Inventory.instance.AddItem(item);
                inventoryUI.Buy(slotnum);
                UpdateSlotUI();
            }
        }        
    }
}
