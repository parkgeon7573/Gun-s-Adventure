//==================================================================
using UnityEngine;
//==================================================================
public class SampleStatePatrol : FSMSingleton<SampleStatePatrol>,IFSMState<MonsterController>
{
    //--------------------------------
    public void Enter(MonsterController e) { e._targetPos = e.GetRandomPos(); }
    //--------------------------------
    public void Execute(MonsterController e)
    {
        if( e._target != null) {

            if( e.IsCloseToTarget(e._target.position, e._traceRange ) )
                e.ChangeState(SampleStateTrace.Instance);
            else
            {
                e.Move(e._targetPos);
                e.Rotate(e._targetPos);

                if (e.IsCloseToTarget(e._targetPos, 1f))
                {
                    e.m_curWayPoint++;
                    e._targetPos = e.GetRandomPos();
                }
                    
            }

        }// if( e._target != null)
        else {
            
            e.Move(e._targetPos);
            e.Rotate(e._targetPos);

            if (e.IsCloseToTarget(e._targetPos, 0.5f))
            {
                e.m_curWayPoint++;
                e._targetPos = e.GetRandomPos();
            }

        }// if( e._target != null)
        e.DrawRay(e._traceRange, Color.green);

    }// public void Execute(SampleStateManager e)
    //--------------------------------
    public void Exit(MonsterController e) { Debug.Log(" -- SampleStatePatrol Exit "); }
    //--------------------------------

}// public class SampleStatePatrol : FSMSingleton<SampleStatePatrol>,IFSMState<SampleStateManager>
 //==================================================================