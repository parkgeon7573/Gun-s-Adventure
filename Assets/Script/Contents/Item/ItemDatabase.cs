using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public int money;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public List<Item> itemDB = new List<Item>();
}
