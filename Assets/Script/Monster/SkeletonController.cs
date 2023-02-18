using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SkeletonController : MonsterController, IUpdateableObject
{
    GameScene gameScene;
    QuestManager quest;
    private void OnEnable()
    {
        UpdateManager.Instance.RegisterUpdateablObject(this);
    }

    private void OnDisable()
    {
        if(UpdateManager.Instance!=null)
            UpdateManager.Instance.DeregisterUpdateableObject(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        gameScene = GameObject.Find("@Scene").GetComponent<GameScene>();
        quest = GameObject.Find("TalkManager").GetComponent<QuestManager>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        questController = GameObject.Find("Human").GetComponent<NPCController>();
        m_wayCtr = GameObject.Find("WayPoint").GetComponent<WaypointController>();
        _attackRange = 3;
        InitState(this, FSMPatrolState.Instance);
        m_navAgent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
        m_SkelltonStat = GetComponent<SkeletonStat>();
       Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }
    public override Vector3 GetRandomPos()
    {
        int point;
        point = Random.Range(0, m_wayCtr.m_waypoints.Length);
        return m_wayCtr.m_waypoints[point].transform.position;
    }
    public override void Move(Vector3 targetPos)
    {
        base.Move(targetPos);
    }

    public override bool IsCloseToTarget(Vector3 targetPos, float range)
    {
        return base.IsCloseToTarget(targetPos, range);
    }
    public override void DrawRay(float range, Color col)
    {
        base.DrawRay(range, col);
    }
    public override bool GetCurrentAnim(string animname)
    {
        return base.GetCurrentAnim(animname);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    public override void Play(string name)
    {
        base.Play(name);
    }
    public override void Rotate(Vector3 targetPos)
    {
        base.Rotate(targetPos);
    }
    public override void SetDamage(int damage, SkillData skillData = null)
    {
        float Damage;
        if (skillData == null)
        {
            Damage = damage - m_SkelltonStat.Defense;
        }
        else
            Damage = skillData.Damage + damage - m_SkelltonStat.Defense;
        m_SkelltonStat.Hp -= Damage;

        if (m_SkelltonStat.Hp <= 0)
        {
            if (CurrentState == PreviousState)
            {
                return;
            }
            else
            {
                if (questController == null)
                {
                    return;
                }
                ChangeState(FSMDieState.Instance);
                questController.isSuccess = true;
                questController.monsterDie++;
                gameScene.CreateMonster();
            }

        }
    }
    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override void Settrigger(string name)
    {
        base.Settrigger(name);
    }

    public override void Stop()
    {
        base.Stop();
    }

    public void OnUpdate()
    {
        FSMUpdate();
        if (quest.questId >= 50)
            gameObject.SetActive(false);
    }
}
