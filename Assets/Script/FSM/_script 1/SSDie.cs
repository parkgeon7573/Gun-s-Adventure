//==================================================================
using UnityEngine;
//==================================================================
public class SSDie : FSMSingleton<SSDie>, IFSMState<SimpleStateManager>
{
    public void Enter(SimpleStateManager e)
    {
        Debug.Log(" -- SampleStateDie Enter ");
        e.ResetTimer();
    }

    public void Execute(SimpleStateManager e)
    {
        if (e._IsTimePass)
        {
            Debug.Log(" -- SampleStateDie Execute ");
            e.ResetTimer();
        }
        e.SetTimePass();

        if (Input.GetKeyDown(KeyCode.Space))
            e.ChangeState(SSInit.Instance);
    }

    public void Exit(SimpleStateManager e)
    {
        Debug.Log(" -- SampleStateDie Exit ");
    }
}
//=============================================================================================================