using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Weapon/Mace", order = int.MaxValue)]
public class ItemMaceEft : ItemEffect
{
    public override bool ExecuteRole()
    {
        PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        player.HasWeapon(2);
        return true;
    }
}
