using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMAttackState : FSMSingleton<FSMAttackState>, IFSMState<MonsterController>
{
    public void Enter(MonsterController monsterController)
    {
        monsterController._lastAttackTime = 0f;
    }

    public void Execute(MonsterController monsterController)
    {
        monsterController.Stop();
        if (Time.time > monsterController._lastAttackTime + MonsterController._AttackTime)
        {
            if (monsterController.GetRanDom())
                monsterController.Play("Attack");
            else monsterController.Play("Attack2");
            monsterController._lastAttackTime = Time.time;
        }


        if (!monsterController.IsCloseToTarget(monsterController._target.position, monsterController._attackRange))
        {
            monsterController.ChangeState(FSMTraceState.Instance);
        }
    }

    public void Exit(MonsterController monsterController)
    {
        Debug.Log(" -- FSMStateAttack Exit ");
    }
}
