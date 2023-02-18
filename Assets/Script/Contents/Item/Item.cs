using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField]
    Define.Item itemtype;

    public int value;
    public string itemName;
    public int itemCost;
    public Sprite itemImage;

    public List<ItemEffect> efts;
    public bool Use()
    {
        bool isUsed = false;
        if (efts != null)
        {
            foreach (ItemEffect eft in efts)
            {
                isUsed = eft.ExecuteRole();
            }
        }
        

        return isUsed;
    }

}
