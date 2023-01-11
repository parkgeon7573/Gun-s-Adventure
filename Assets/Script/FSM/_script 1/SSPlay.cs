//==================================================================
using UnityEngine;
//==================================================================
public class SSPlay : FSMSingleton<SSPlay>, IFSMState<SimpleStateManager>
{
    public void Enter(SimpleStateManager e)
    {
        Debug.Log(" -- SampleStatePlay Enter ");
        e.ResetTimer();
    }

    public void Execute(SimpleStateManager e)
    {        
        if (e._IsTimePass)
        {
            Debug.Log(" -- SampleStatePlay Execute ");
            e.ResetTimer();
        }
        e.SetTimePass();

        if (Input.GetKeyDown(KeyCode.Space))
            e.ChangeState(SSDie.Instance);
    }

    public void Exit(SimpleStateManager e)
    {
        Debug.Log(" -- SampleStatePlay Exit ");
    }
}
//==================================================================