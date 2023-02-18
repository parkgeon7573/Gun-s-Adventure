using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    PlayerController m_player;
    RaycastHit hit;
    float m_detectDist =5f;
    public bool[] soldOuts;
    public List<Item> stocks = new List<Item>();
    public bool IsCloseToTarget()
    {
        var dir = m_player.transform.position - transform.position;
        if (Physics.Raycast(transform.position + Vector3.up * 0.6f, dir.normalized, out hit, m_detectDist, 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Background")))
        {
            if (hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }
    private void Start()
    {
        stocks.Add(ItemDatabase.instance.itemDB[0]);
        stocks.Add(ItemDatabase.instance.itemDB[1]);
        stocks.Add(ItemDatabase.instance.itemDB[2]);
        stocks.Add(ItemDatabase.instance.itemDB[3]);
        soldOuts = new bool[stocks.Count];
        for (int i = 0; i < soldOuts.Length; i++)
        {
            soldOuts[i] = false;
        }
    }
}
