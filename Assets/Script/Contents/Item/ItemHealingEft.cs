using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Heart/Potion")]
public class ItemHealingEft : ItemEffect
{
    public int healinPoint = 0;
    public override bool ExecuteRole()
    {
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.Heal(healinPoint);
        return true;
    }
}
