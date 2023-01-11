//==================================================================
using UnityEngine;
//==================================================================
public class SampleStateTrace : FSMSingleton<SampleStateTrace>, IFSMState<MonsterController>
{
    public void Enter(MonsterController e)
    {
        Debug.Log(" -- SampleStateTrace Enter ");
    }

    public void Execute(MonsterController e)
    {
        if (e.IsCloseToTarget(e._target.position, e._traceRange))
        {
            e.Move(e._target.position);
            e.Rotate(e._target.position);

            if (e.IsCloseToTarget(e._target.position, e._attackRange))
                e.ChangeState(SampleStateAttack.Instance);
        }
        else
            e.ChangeState(SampleStatePatrol.Instance);

        e.DrawRay(e._attackRange, Color.yellow);
    }

    public void Exit(MonsterController e)
    {
        Debug.Log(" -- SampleStateTrace Exit ");
    }
}
//==================================================================