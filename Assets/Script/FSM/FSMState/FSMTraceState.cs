using UnityEngine;
public class FSMTraceState : FSMSingleton<FSMTraceState>, IFSMState<MonsterController>
{
    public void Enter(MonsterController monsterController)
    {
            Debug.Log(" -- SampleStateTrace Enter ");
    }

    public void Execute(MonsterController monsterController)
    {
        if (monsterController.IsCloseToTarget(monsterController._target.position, monsterController._traceRange))
        {
            monsterController.Move(monsterController._target.position);
            monsterController.Rotate(monsterController._target.position);

            if (monsterController.IsCloseToTarget(monsterController._target.position, monsterController._attackRange))
            {
                monsterController.Stop();
                monsterController.ChangeState(FSMAttackState.Instance);
            }                
        }
        else
            monsterController.ChangeState(FSMPatrolState.Instance);

        monsterController.DrawRay(monsterController._attackRange, Color.yellow);
    }
    public void Exit(MonsterController monsterController)
    {
        Debug.Log(" -- FSMStateTrace Exit ");
    }
}