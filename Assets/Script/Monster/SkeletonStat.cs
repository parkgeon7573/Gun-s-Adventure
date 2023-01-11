using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStat : Stat
{
    float _idleDuration;
    float _attackSpeed;

    public float IdleDuration { get { return _idleDuration; } set { _idleDuration = value; } }
    public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }
    private void Start()
    {
        _speed = 10;
        _level = 1;
        _hp = 50;
        _maxHp = 50;
        _attack = 10;
        _defense = 10;
        _idleDuration = 3f;
        _attackSpeed = 0.7f;
    }
}
