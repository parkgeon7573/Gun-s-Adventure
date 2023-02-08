using UnityEngine;
public class FSMPatrolState : FSMSingleton<FSMPatrolState>,IFSMState<MonsterController>
{
    public void Enter(MonsterController monsterController) { monsterController._targetPos = monsterController.GetRandomPos(); }
    public void Execute(MonsterController monsterController)
    {
        if(monsterController._targetPos == Vector3.zero)
        {
            monsterController.Move(monsterController._target.position);
        }
        if( monsterController._target != null) {

            if( monsterController.IsCloseToTarget(monsterController._target.position, monsterController._traceRange ) )
                monsterController.ChangeState(FSMTraceState.Instance);
            else
            {
                monsterController.Move(monsterController._targetPos);
                monsterController.Rotate(monsterController._targetPos);
                if (monsterController.IsCloseToTarget(monsterController._targetPos, 1f))
                {
                    monsterController.m_curWayPoint++;
                    monsterController._targetPos = monsterController.GetRandomPos();
                }                    
            }
        }
        else {
            monsterController.Move(monsterController._targetPos);
            monsterController.Rotate(monsterController._targetPos);

            if (monsterController.IsCloseToTarget(monsterController._targetPos, 0.5f))
            {
                monsterController.m_curWayPoint++;
                monsterController._targetPos = monsterController.GetRandomPos();
            }
        }
        monsterController.DrawRay(monsterController._traceRange, Color.green);

    }
    public void Exit(MonsterController monsterController) { Debug.Log(" -- FSMStatePatrol Exit "); }   
}