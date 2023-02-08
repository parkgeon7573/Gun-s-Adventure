using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class BossController : MonsterController, IUpdateableObject
{
    [SerializeField]
    RectTransform bossHealthBar;
    [SerializeField]
    PlayerController player;
    
    public TalkManager talkManager;
    private void OnEnable()
    {
        UpdateManager.Instance.RegisterUpdateablObject(this);
    }

    private void OnDisable()
    {
        UpdateManager.Instance.DeregisterUpdateableObject(this);
    }
    private void LateUpdate()
    {
        bossHealthBar.localScale = new Vector3((float)m_BossStat.Hp / m_BossStat.MaxHp, 1, 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        _attackRange = 5f;
        InitState(this, FSMPatrolState.Instance);
        m_navAgent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
        m_BossStat = GetComponent<BossStat>();
        talkManager = GameObject.Find("TalkManager").GetComponent<TalkManager>();
    }
    public override Vector3 GetRandomPos()
    {
        return _target.position;
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
        m_animator.Play(name);
    }
    public override void Rotate(Vector3 targetPos)
    {
        base.Rotate(targetPos);
    }
    public override void SetDamage(SkillData skillData, int damage)
    {

        float Damage = skillData.Damage + damage - m_BossStat.Defense;
        m_BossStat.Hp -= Damage;
        if (m_BossStat.Hp <= 0)
        {
            m_BossStat.Hp = 0;
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
                player.Heal(100);
                questController.isSuccess = true;
                talkManager.BossDead();
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
    }
}

