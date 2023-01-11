//==================================================================
using UnityEngine;
//==================================================================
public class SampleStateDie : FSMSingleton<SampleStateDie>, IFSMState<MonsterController>
{
    public void Enter(MonsterController e)
    {
        e.Play("Die");
        e.transform.position += Vector3.down * 5f;
        Debug.Log(" -- SampleStateDie Enter ");
        //e.gameObject.SetActive(false);
    }
    public void Execute(MonsterController e) { }
    public void Exit(MonsterController e) { }
}
//==================================================================