using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStat : Stat
{
    private void Start()
    {
        _speed = 10;
        _level = 1;
        _hp = 200;
        _maxHp = 200;
        _attack = 25;
        _defense = 25;
    }
}