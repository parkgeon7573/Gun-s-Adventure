using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    float m_speed = 3f;
    [SerializeField]
    float m_runspeed = 9f;
    float finalSeed;
    PlayerMove m_playermove;
    Animator m_animator;
    Camera m_camera;
    CharacterController m_CharacterCtr;

    Vector3 m_dir;
       
    
    


    //플레이어 움직임
    #region normalmove
    void MovePlayer()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        
        Vector3 right = transform.TransformDirection(Vector3.right);

        Vector3 moveDirection = forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal");


        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJump();

        }
        if(moveDirection != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                PlayerRun();
            }
            else
            {
                PlayerWalk();
            }
        }                
        else if(moveDirection == Vector3.zero)
        {
            PlayerIdle();
        }
        m_playermove.MoveTo(moveDirection, finalSeed);
    }

    void PlayerIdle()
    {
        finalSeed = 0;
        m_animator.SetFloat("Move", 0f, 5.0f, 0.5f);
    }
    void PlayerJump() 
    {
        m_playermove.JumpTo();
        
    }
    void PlayerWalk()
    {
        finalSeed = m_speed;
        m_animator.SetFloat("Move", 0.5f, 5.0f, 0.5f);
    }
    void PlayerRun()
    {
        finalSeed = m_runspeed;
        m_animator.SetFloat("Move", 1.0f, 5.0f, 0.5f);
    }

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        m_playermove = GetComponent<PlayerMove>();
        m_animator = GetComponent<Animator>();
        m_camera = Camera.main;
        m_CharacterCtr = GetComponent<CharacterController>();
    }
    

    // Update is called once per frame
    void Update()
    {        
        
        MovePlayer();
    }
    


}
