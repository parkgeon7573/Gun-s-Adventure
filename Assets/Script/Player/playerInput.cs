using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour, IUpdateableObject
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


    private void OnEnable()
    {
        UpdateManager.Instance.RegisterUpdateablObject(this);
    }

    private void OnDisable()
    {
        if (UpdateManager.Instance != null)
            UpdateManager.Instance.DeregisterUpdateableObject(this);
    }

    public void OnUpdate()
    {

        MoveInput = new Vector2(Input.GetAxisRaw(moveHorizontalAxisName), Input.GetAxisRaw(moveVerticalAxisName));

        if (MoveInput.sqrMagnitude > 1) MoveInput = MoveInput.normalized;

        Hand = Input.GetButtonDown(GetWeaponHand);
        Sword = Input.GetButtonDown(GetWeaponSword);
        Jump = Input.GetButtonDown(jumpButtonName);
        if (!EventSystem.current.IsPointerOverGameObject())
            Attack = Input.GetButtonDown(attackButtonName);
    }
}
