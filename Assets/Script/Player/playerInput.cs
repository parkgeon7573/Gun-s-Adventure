using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInput : MonoBehaviour
{
    public string moveHorizontalAxisName = "Horizontal";
    public string moveVerticalAxisName = "Vertical";

    public string attackButtonName = "Fire1";
    public string jumpButtonName = "Jump";
    public string runButtonName = "Sprint";
    
    public Vector2 moveInput { get; private set; }
    public bool fire { get; private set; }
    public bool jump { get; private set; }
    public float run { get; private set; }
    public float runspeed { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(GameManager.Instance != null && GameManager.Instance.isGameover)
        {
            moveInput = Vector2.zero;
            fire = false;
            jump = false;
            return;
        }*/


        moveInput = new Vector2(Input.GetAxisRaw(moveHorizontalAxisName), Input.GetAxisRaw(moveVerticalAxisName));
        if (moveInput.sqrMagnitude > 1) moveInput = moveInput.normalized;

        runspeed = (0.5f + Input.GetAxis(runButtonName)) / 1;

        run = 0.5f + Input.GetAxis(runButtonName) * 0.5f;
        jump = Input.GetButtonDown(jumpButtonName);
        fire = Input.GetButtonDown(attackButtonName);
    }
}
