using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Animator m_animator;
    //[SerializeField]
   // float normalSpeed = 3.0f;
    //float m_speed;
    [SerializeField]
    float jumpForce = 4.0f;
    float gravity = -6.8f;
    Vector3 moveDirection;

    CharacterController m_characterCtr;

    public void MoveTo(Vector3 direction, float speed)
    {
        
        //moveDirection = direction;
        moveDirection = new Vector3(direction.x, moveDirection.y, direction.z);
        //m_speed = moveDirection.magnitude * normalSpeed;
        m_characterCtr.Move(moveDirection * speed * Time.deltaTime);
            
        
    }


    public void JumpTo()
    {
        if(m_characterCtr.isGrounded == true)
        {
            moveDirection.y = jumpForce;
        }
    }

    void Isground()
    {
        if (m_characterCtr.isGrounded == false)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_characterCtr = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Isground();
        
    }
}
