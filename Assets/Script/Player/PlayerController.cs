using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum PlayerState
    {
        Normal,
        Sowrd,
        Die
    }
    PlayerState m_state;
    PlayerStat m_playerstat;
    CharacterController m_characterController;
    playerInput m_playerInput;
    Animator m_animator;

    Camera m_camera;

    [SerializeField]
    bool toggleCameraRotation;
    [SerializeField]
    float m_jumpVelocity = 10.0f;
    float smoothness = 10.0f;
    [Range(0.01f, 1f)]
    [SerializeField]
    float airControlPercent; //점프하는동안 플레이어가 원래속도의 몇퍼센트만큼 통제할수있는지

    // 데코레이터패턴
    float currentVelocityY;

    public float currentSpeed => new Vector2(m_characterController.velocity.x, m_characterController.velocity.z).magnitude;



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
    // Start is called before the first frame update
    void Start()
    {
        m_playerstat = gameObject.GetComponent<PlayerStat>();
        m_playerInput = GetComponent<playerInput>();
        m_animator = GetComponent<Animator>();
        m_characterController = GetComponent<CharacterController>();
        m_camera = Camera.main;
        
    }
    private void FixedUpdate()
    {
        spinCamera();
        MoveUpdate(m_playerInput.moveInput);
    }

    // Update is called once per frame
    void Update()
    {
       
        switch (m_state)
        {
            case PlayerState.Normal:
                NormalAttack();
                break;
            case PlayerState.Die:
                break;
            case PlayerState.Sowrd:

                break;
        }

        Jump();
        MoveAnimation(m_playerInput.moveInput);
    }
        
    public void MoveUpdate(Vector2 moveInput) 
    {
        var targetSpeed = m_playerstat.Speed * moveInput.magnitude;
        var moveDirection = Vector3.Normalize(transform.forward * moveInput.y + transform.right * moveInput.x);

        currentVelocityY += Time.deltaTime * Physics.gravity.y * 5;

        var velocity = moveDirection * targetSpeed + Vector3.up * currentVelocityY;

        m_characterController.Move(velocity * Time.deltaTime);
        if (m_characterController.isGrounded) currentVelocityY = 0f;   
    }
    public void Jump() 
    {
        if (m_playerInput.jump)
        {
            if (!m_characterController.isGrounded) return;
            currentVelocityY = m_jumpVelocity;

            TriggerAnim("Jump");
        }
    }

    public void NormalAttack()
    {
        if (m_playerInput.attack)
        {
            TriggerAnim("Attack1");
            GameObject go = Managers.Resource.Instantiate("Effect/Sword1", transform);
        }
    }

    private void MoveAnimation(Vector2 moveInput)
    {
        m_animator.SetFloat("Vertical Move", moveInput.y, 0.3f, Time.deltaTime);
        m_animator.SetFloat("Horizontal Move", moveInput.x, 0.2f, Time.deltaTime);
    }
    private void TriggerAnim(string triggername)
    {
        m_animator.SetTrigger($"{triggername}");
    }
}
