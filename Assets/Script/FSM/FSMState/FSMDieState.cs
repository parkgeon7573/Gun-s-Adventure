using UnityEngine;
public class FSMDieState : FSMSingleton<FSMDieState>, IFSMState<MonsterController>
{
    public void Enter(MonsterController monsterController)
    {
        if (monsterController.GetCurrentAnim("Die"))
        {
            return;
        }
        else
            monsterController.Play("Die");
        Debug.Log(" -- FSMStatemonsterDie Enter ");
    }
    public void Execute(MonsterController monsterController) { }
    public void Exit(MonsterController monsterController) { }
}