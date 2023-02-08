using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimController : MonoBehaviour
{
    public enum Motion
    {
        Idle,
        Walk,
        Attack,
        Hit,
        Max
    }
    Motion m_motion;
    Animator m_animator;
    public Motion GetCurrentMotion()
    {
        return m_motion;
    }

    public void Play(string animName, bool isBlend)
    {
        if (isBlend)
        {
            m_animator.SetTrigger(animName);
        }
        else
        {
            m_animator.Play(animName, 0, 0f);
        }
    }
    StringBuilder m_sb = new StringBuilder();
    public void Play(Motion motion, bool isBlend = true)
    {
        m_motion = motion;
        m_sb.Append(motion);
        Play(m_sb.ToString(), isBlend);
        m_sb.Clear();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }
}
