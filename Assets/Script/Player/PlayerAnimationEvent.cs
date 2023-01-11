using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{

    PlayerStat m_stat;
    [SerializeField]
    GameObject m_attackAreaObj;
    AttackAreaUnitFind[] m_attackArea;
    [SerializeField]
    ParticleSystem[] m_SwordEffect;
    [SerializeField]
    Weapon[] m_weapon;
    Animator m_animator;

    
    private void Start()
    {
        m_stat = GetComponent<PlayerStat>();
        m_animator = GetComponent<Animator>();
        m_attackArea = m_attackAreaObj.GetComponentsInChildren<AttackAreaUnitFind>();
    }

     void AnimEvent_Sword1_Start()
    {
        var unitList = m_attackArea[0].m_unitList;
        OnHitEvent(unitList);
        Managers.Resource.Instantiate("Effect/Sword1", this.transform);
    }
    void AnimEvent_Sword2_Start()
    {
        var unitList = m_attackArea[1].m_unitList;
        OnHitEvent(unitList);
        Managers.Resource.Instantiate("Effect/Sword2", this.transform);
    }
    void AnimEvent_Sword3_Start()
    {
        var unitList = m_attackArea[2].m_unitList;
        OnHitEvent(unitList);
        Managers.Resource.Instantiate("Effect/Sword3", this.transform);
    }
    void OnHitEvent(List<GameObject> gameObjects, int weapon = 0, int skiil = 0)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            var mon = gameObjects[i].GetComponent<MonsterController>();
            Stat targetStat = gameObjects[i].GetComponent<Stat>();
            PlayerStat myStat = gameObject.GetComponent<PlayerStat>();
            if (mon != null)
            {                
                int damage = Mathf.Max(0, myStat.Attack + weapon + skiil - targetStat.Defense);
               // mon.SetDamage();
                Debug.Log(damage);
                targetStat.Hp -= damage;
            }            
        }
    }


}
