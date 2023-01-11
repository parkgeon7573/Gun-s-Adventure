//==================================================================
using UnityEngine;
//==================================================================
public class SSInit : FSMSingleton<SSInit>, IFSMState<SimpleStateManager>
{
    public void Enter(SimpleStateManager e)
    {
        Debug.Log(" -- SampleStateInit Enter ");
        e.ResetTimer();
    }

    public void Execute(SimpleStateManager e)
    {
        if (e._IsTimePass)
        {
            Debug.Log(" -- SampleStateInit Execute ");
            e.ResetTimer();
        }
        e.SetTimePass();

        if (Input.GetKeyDown(KeyCode.Space))
            e.ChangeState(SSPlay.Instance);
    }

    public void Exit(SimpleStateManager e)
    {
        Debug.Log(" -- SampleStateInit Exit ");
    }
}
//==================================================================