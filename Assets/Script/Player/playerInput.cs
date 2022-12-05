using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class playerInput : MonoBehaviour
{
    public string moveHorizontalAxisName = "Horizontal";
    public string moveVerticalAxisName = "Vertical";


    public string attackButtonName = "Fire1";
    public string jumpButtonName = "Jump";
    
    public Vector2 moveInput { get; private set; }
    public bool attack { get; private set; }
    public bool jump { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            Managers.UI.ShowPopupUI<UI_Inven>();

        if (Input.GetKeyDown(KeyCode.Escape)) Managers.UI.ClosePopupUI();

        moveInput = new Vector2(Input.GetAxisRaw(moveHorizontalAxisName), Input.GetAxisRaw(moveVerticalAxisName));
        
        if (moveInput.sqrMagnitude > 1) moveInput = moveInput.normalized;

        jump = Input.GetButtonDown(jumpButtonName);
        if (EventSystem.current.IsPointerOverGameObject()) return;
        else attack = Input.GetButtonDown(attackButtonName);
    }
}
