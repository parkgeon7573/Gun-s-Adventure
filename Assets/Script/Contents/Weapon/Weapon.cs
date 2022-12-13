using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponType
    {
        Hand, Sowrd
    }
    public WeaponType weapontype;

    public int damage;
    [SerializeField]
    TrailRenderer AtkArea;
    public void Use(WeaponType type)
    {
        if(type == WeaponType.Sowrd)
        {
            Swing();
        }
        else if(type == WeaponType.Hand)
        {
            atk();
        }
    }

    void Swing()
    {
        Debug.Log("SwordAttack!");
        
    }
    void atk()
    {
        Debug.Log("HandAttack!");
    }
}
