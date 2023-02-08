using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Weapon/Sword", order = int.MaxValue)]
public class ItemWeaponEft : ItemEffect
{
    public override bool ExecuteRole()
    {
        PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        player.HasWeapon(1);
        return true;
    }

}
