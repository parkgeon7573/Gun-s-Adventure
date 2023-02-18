using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour, IUpdateableObject
{
    Inventory inven;
    public Shop shop;
    public ShopSlot[] shopSlots;
    public Transform shopHolder;
    public Button closeShop;
    public GameObject shopUI;
    public GameObject invectoryPanel;
    public Slot[] slots;
    public Transform slotHolder;
    public Text text;
    bool activeInvectory = false;
    bool isStoreActive;

    private void OnEnable()
    {        
        UpdateManager.Instance.RegisterUpdateablObject(this);
    }

    private void OnDisable()
    {
        if (UpdateManager.Instance != null)
            UpdateManager.Instance.DeregisterUpdateableObject(this);
    }
    private void Start()
    {
        inven = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        shopSlots = shopHolder.GetComponentsInChildren<ShopSlot>();
        for (int i = 0; i < shopSlots.Length; i++)
        {
            shopSlots[i].Init(this);
            shopSlots[i].slotnum = i;
        }
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotnum = i;
            if (i < inven.SlotCnt)
                slots[i].GetComponent<Button>().interactable = true;
            else
                slots[i].GetComponent<Button>().interactable = false;
        }
        inven.onSlotCountChange += SlotChange;
        inven.onChangeItem += RedrawSlotUI;
        RedrawSlotUI();
        invectoryPanel.SetActive(activeInvectory);
    }

    private void SlotChange(int val)
    {
        for(int i = 0; i< slots.Length; i++)
        {
            slots[i].slotnum = i;
            if (i < inven.SlotCnt)
                slots[i].GetComponent<Button>().interactable = true;
            else
                slots[i].GetComponent<Button>().interactable = false;
        }
    }
    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for (int i = 0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }
    void mouselock()
    {
        if (activeInvectory || isStoreActive)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void OnUpdate()
    {
        text.text = ItemDatabase.instance.money.ToString();
        InitShop();
        if (Input.GetKeyDown(KeyCode.I) && !isStoreActive)
        {
            activeInvectory = !activeInvectory;
            invectoryPanel.SetActive(activeInvectory);
            Cursor.visible = activeInvectory;
        }
        mouselock();
    }
   public void AddSlot()
    {
        inven.SlotCnt++;
    }
   
    public void InitShop()
    {
        ActiveShop(shop.IsCloseToTarget());
        if (shop.IsCloseToTarget())
        {
            for (int i = 0; i < shop.stocks.Count; i++)
            {
                shopSlots[i].item = shop.stocks[i];
                shopSlots[i].UpdateSlotUI();
            }
        }
    }
    public void Buy(int num)
    {
        shop.soldOuts[num] = true;
    }
    public void ActiveShop(bool isOpen)
    {
        if (!activeInvectory)
        {
            isStoreActive = isOpen;
            Cursor.visible = isOpen;
            shopUI.SetActive(isOpen);
            invectoryPanel.SetActive(isOpen);
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].isShopMode = isOpen;
            }
        }
        if (!isOpen)
        {
            //shop.gameObject.SetActive(false);
            for (int i = 0; i < shopSlots.Length; i++)
            {
                shopSlots[i].RemoveSlot();
            }
        }
    }
    public void SellBtn()
    {
        for (int i = slots.Length; i > 0; i--)
        {
            slots[i - 1].SellItem();
        }
    }
}
