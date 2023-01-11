using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleStateAttack : FSMSingleton<SampleStateAttack>, IFSMState<MonsterController>
{
    public void Enter(MonsterController e)
    {
        Debug.Log(" -- SampleStateAttack Enter ");
        e._lastAttackTime = 0f;
    }

    public void Execute(MonsterController e)
    {
        if (e.IsCloseToTarget(e._target.position, e._attackRange))
        {
            if (Time.time > e._lastAttackTime + MonsterController._AttackTime)
            {
                e.Play("Attack");
                e._lastAttackTime = Time.time;
            }
        }
        else
            e.ChangeState(SampleStateTrace.Instance);

        e.DrawRay(e._attackRange, Color.red);
    }

    public void Exit(MonsterController e)
    {
        Debug.Log(" -- SampleStateAttack Exit ");
    }
}
