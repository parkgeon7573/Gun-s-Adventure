using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;
    private int slotCnt;
    public List<Item> items = new List<Item>();
    public int SlotCnt
    {
        get => slotCnt;
        set {
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }
            
    }

    // Start is called before the first frame update
    void Start()
    {
        slotCnt = 8;
    }
    public bool AddItem(Item _item)
    {
        if(items.Count < SlotCnt) 
        {
            items.Add(_item);
            if(onChangeItem != null)
                onChangeItem.Invoke();
            return true;
        }
        return false;
    }
    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);
        onChangeItem.Invoke();
    }
}
