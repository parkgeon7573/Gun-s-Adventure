using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Define
{
    public enum WorldObject
    {
        Unknown,
        Player,
        e,
    }

    public enum Item
    {
        Coin,
        Heart,
        Weapon
    }

    public enum WeaponType
    {
        Hand,
        Sword
    }

    public enum Layer
    {
        e = 8,
        Ground = 9,
        Block = 10,
    }


    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }

    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }
}

