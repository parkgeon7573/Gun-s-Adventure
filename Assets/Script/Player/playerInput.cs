using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    public string moveHorizontalAxisName = "Horizontal";
    public string moveVerticalAxisName = "Vertical";

    public string GetWeaponHand = "Hand";
    public string GetWeaponSword = "Sword";
    public string attackButtonName = "Fire1";
    public string jumpButtonName = "Jump";

    public Vector2 MoveInput { get; private set; }
    public bool Attack { get; private set; }
    public bool Jump { get; private set; }
    public bool Hand { get; private set; }
    public bool Sword { get; private set; }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            Managers.UI.ShowPopupUI<UI_Inven>();

        if (Input.GetKeyDown(KeyCode.Escape)) Managers.UI.ClosePopupUI();

        MoveInput = new Vector2(Input.GetAxisRaw(moveHorizontalAxisName), Input.GetAxisRaw(moveVerticalAxisName));
        
        if (MoveInput.sqrMagnitude > 1) MoveInput = MoveInput.normalized;

        Hand = Input.GetButtonDown(GetWeaponHand);
        Sword = Input.GetButtonDown(GetWeaponSword);
        Jump = Input.GetButtonDown(jumpButtonName);
        if (!EventSystem.current.IsPointerOverGameObject()) 
            Attack = Input.GetButtonDown(attackButtonName);
    }
}
