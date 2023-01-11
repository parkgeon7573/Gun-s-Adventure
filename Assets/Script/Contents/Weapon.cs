using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://angliss.cc/animator-animatorstateinfo/

public class Weapon : MonoBehaviour
{
    Animator _animator;
    public enum WeaponType
    {
        Hand, Sowrd
    }
    public WeaponType weapontype;


    private void Start()
    {
        _animator = GetComponentInParent<Animator>();
    }
    public void Use(WeaponType type)
    {
        if (type == WeaponType.Sowrd)
        {
            Swing();
        }
        else if (type == WeaponType.Hand)
        {
            Atk();
        }
    }

    void Swing()
    {
        _animator.SetTrigger("SwordAttack");
    }
    void Atk()
    {
        _animator.SetTrigger("HandAttack");
    }


}