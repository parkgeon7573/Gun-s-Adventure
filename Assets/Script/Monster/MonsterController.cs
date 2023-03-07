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
    protected WaypointController m_wayCtr;
    [SerializeField]
    protected NPCController questController;
    protected Animator m_animator;
    protected NavMeshAgent m_navAgent;
    protected SkeletonStat m_SkelltonStat;
    protected BossStat m_BossStat;
    public float stopDist = 0.8f;
    public Transform _target;
    [HideInInspector]
    public Vector3 _targetPos = Vector3.zero;
    public float _rotSpeed = 10f;

    public float _traceRange = 10f;

    public float _attackRange = 3f;
    public float _stopAtkRange = 3f;
    public const float _AttackTime = 3f;
    public float _lastAttackTime = 0f;
    public int m_curWayPoint = 0;
    public bool GetRanDom()
    {
        if (CompareTag("Boss"))
        {
            if (Random.value < 0.5)
            {
                return true;
            }
            else return false;
        }
        else return true;
    }
    public virtual Vector3 GetRandomPos()
    {
        if (m_curWayPoint > m_wayCtr.m_waypoints.Length - 1)
        {
            m_curWayPoint = 0;
        }
        return m_wayCtr.m_waypoints[m_curWayPoint].transform.position;

    }
    public virtual bool GetCurrentAnim(string animname)
    {
        if (m_animator.GetCurrentAnimatorStateInfo(0).IsName(animname))
        {
            return true;
        }
        else return false;
    }
    protected virtual void OnTriggerEnter(Collider other) {
        
        if( other.CompareTag("feature"))
            ChangeState(FSMPatrolState.Instance);

    }
    public virtual bool IsCloseToTarget( Vector3 targetPos, float range )
    {
        float dist = Vector3.SqrMagnitude(transform.position - targetPos);

        if (dist < range * range)
        {
           return true;
        }
        return false;

    }
    public virtual void Move(Vector3 targetPos)
    {
        m_navAgent.SetDestination(targetPos);
        Play("Walk");
        m_navAgent.stoppingDistance = stopDist;
    }
   
    public virtual void Rotate( Vector3 targetPos )
    {
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, _rotSpeed * Time.deltaTime);

    }
    public virtual void Play(string name)
    {
        m_animator.Play(name);
        
    }
    public virtual void Settrigger(string name)
    {
        m_animator.SetTrigger(name);
    }
    public virtual void Stop()
    {
        m_navAgent.ResetPath();
    }
    public virtual void DrawRay( float range, Color col )
    {
        var dir = (_target.transform.position - transform.position).normalized;
        dir.y = 0f;
        Debug.DrawRay( transform.position + Vector3.up * 0.6f, dir * range, col);
    }

    public virtual void SetDamage(int damage, SkillData skillData = null) { }
   

    #region Unity Animation Event Methods

    void AnimEvent_SkeletonAtk()
    {
        PlayerController player = _target.GetComponent<PlayerController>();
        player.SetDamage(m_SkelltonStat.Attack);
    }
    void AnimEvent_BossAtk()
    {
        Managers.Sound.Play("Whoosh/Whoosh 14_5", Define.Sound.Effect);
        PlayerController player = _target.GetComponent<PlayerController>();
        player.SetDamage(m_BossStat.Attack);
    }
    void AnimEvent_Die()
    {
        Managers.Resource.Destroy(gameObject);
    }
    #endregion
}