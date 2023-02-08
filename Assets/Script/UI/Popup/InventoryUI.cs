using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour, IUpdateableObject
{
    Inventory inven;
    public GameObject invectoryPanel;
    bool activeInvectory = false;

    public Slot[] slots;
    public Transform slotHolder;

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
        inven.onSlotCountChange += SlotChange;
        inven.onChangeItem += RedrawSlotUI;
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

    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInvectory = !activeInvectory;
            invectoryPanel.SetActive(activeInvectory);
        }
    }
   public void AddSlot()
    {
        inven.SlotCnt++;
    }
    void RedrawSlotUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for (int i = 0; i < inven.items.Count; i++) 
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
            
        }
    }
}
