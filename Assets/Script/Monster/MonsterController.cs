//==================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//==================================================================
public class MonsterController : FSM<MonsterController>
{
    //------------------------------
    [SerializeField]
    WaypointController m_wayCtr;
    [SerializeField]
    NPCController questController;
    Animator m_animator;
    NavMeshAgent m_navAgent;
    SkeletonStat m_stat;
    float stopDist = 0.5f;
    public Transform _target;
    [ HideInInspector ]
    public Vector3 _targetPos = Vector3.zero;
    //------------------------------
    public float _rotSpeed = 10f;

    public float _traceRange = 10f;

    public float _attackRange = 0.8f;
    public const float _AttackTime = 0.5f;
    public float _lastAttackTime = 0f;
    public int m_curWayPoint = 0;
    //------------------------------
    void Start()
    {
        InitState(this, SampleStatePatrol.Instance);
        m_navAgent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
        m_stat = GetComponent<SkeletonStat>();
        Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform); 
    }
    //------------------------------
    void Update() { FSMUpdate(); }
    //------------------------------
    public Vector3 GetRandomPos()
    {
        if (m_curWayPoint > m_wayCtr.m_waypoints.Length - 1)
        {
            m_curWayPoint = 0;
        }
        return m_wayCtr.m_waypoints[m_curWayPoint].transform.position;

    }// public Vector3 GetRandomPos()
    //------------------------------
    private void OnTriggerEnter(Collider other) {
        
        if( other.CompareTag("feature"))
            ChangeState( SampleStatePatrol.Instance);

    }// private void OnTriggerEnter(Collider other)
    //------------------------------
    public bool IsCloseToTarget( Vector3 targetPos, float range )
    {
        float dist = Vector3.SqrMagnitude(transform.position - targetPos);

        if (dist < range * range)
        {
            return true;            
        }
        return false;

    }// public bool IsCloseToTarget( Vector3 targetPos, float range )
    //------------------------------
    public void Move(Vector3 targetPos)
    {
        m_navAgent.SetDestination(targetPos);
        Play("Walk");
        m_navAgent.stoppingDistance = stopDist;
    }
    //------------------------------
    public void Rotate( Vector3 targetPos )
    {
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, _rotSpeed * Time.deltaTime);

    }// public void Rotate( Vector3 targetPos )
    //------------------------------
    public void Play(string name)
    {
        m_animator.SetTrigger(name);
        
    }
    public void DrawRay( float range, Color col )
    {
        var dir = (_target.transform.position - transform.position).normalized;
        dir.y = 0f;
        Debug.DrawRay( transform.position + Vector3.up * 0.6f, dir * range, col);
    }

    public void SetDamage(SkillData skillData, int damage)
    {
        float Damage = skillData.Damage + damage - m_stat.Defense;
        m_stat.Hp -= Damage;
        
        if (m_stat.Hp <= 0)
        {
            if (CurrentState == PreviousState)
            {
                return;
            }
            else
            {
                ChangeState(SampleStateDie.Instance);
                questController.isSuccess = true;
                questController.monsterDie++;
            }
           
        }            
    }

    #region Unity Animation Event Methods

    void AnimEvent_SkeletonAtk()
    {
        PlayerController player = _target.GetComponent<PlayerController>();
        player.SetDamage(m_stat.Attack);
    }
    void AnimEvent_Foot()
    {

    }
    void AnimEvent_Die()
    {
        this.gameObject.SetActive(false);
    }
    #endregion
}