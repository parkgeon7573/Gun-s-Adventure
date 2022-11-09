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
    float smoothness = 10.0f;
    float animWalk;
    [SerializeField]
    bool toggleCameraRotation;
    

    //카메라 토글
    #region Camera
    void LookAround()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            toggleCameraRotation = true;
        }
        else
        {
            toggleCameraRotation = false;
        }
    }

    void spinCamera()
    {
        LookAround();
        if (toggleCameraRotation != true)
        {
            Vector3 playerRotate = Vector3.Scale(m_camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
        }
    }
    #endregion
    //플레이어 움직임
    #region normalmove
    void MovePlayer()
    {
        //float vertiacl = Input.GetAxis("Vertical");
        //float offset = 0.5f + Input.GetAxis("Sprint") * 0.5f;
        //float horizontal = Input.GetAxis("Horizontal");

        //float moveParameter = Mathf.Abs(vertiacl * offset);
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
        m_animator.SetTrigger("Jump");
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
        spinCamera();
        MovePlayer();
    }
    


}
