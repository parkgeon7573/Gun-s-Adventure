using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool sDwon1;
    bool sDwon2;
    bool isSwap;
    Weapon equipWeapon;
    int equipWeaponIndex = -1;
    void GetInput()
    {
        sDwon1 = Input.GetKeyDown(KeyCode.Alpha1);
        sDwon2 = Input.GetKeyDown(KeyCode.Alpha2);
    }

    
   
    CharacterController m_characterController;
    playerInput m_playerInput;
    Animator m_animator;

    Camera m_camera;

    [SerializeField]
    GameObject[] _weapons;
    [SerializeField]
    bool[] hasWeapons;
    [SerializeField]
    bool toggleCameraRotation;
    [SerializeField]
    float Speed = 10.0f;
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
        Attack();

        GetInput();
        Swap();

        Jump();
        MoveAnimation(m_playerInput.moveInput);
    }
        



    public void Attack()
    {
        if (equipWeapon == null)
            return;
        
        if(m_playerInput.attack && !isSwap)
        {
            equipWeapon.Use(equipWeapon.weapontype);
            TriggerAnim(equipWeapon.weapontype == Weapon.WeaponType.Sowrd ? "doSwing" : "doHand");
        }
    }
    #region Movement
    public void MoveUpdate(Vector2 moveInput)
    {

        var targetSpeed = Speed * moveInput.magnitude;
        var moveDirection = Vector3.Normalize(transform.forward * moveInput.y + transform.right * moveInput.x);
        if (isSwap == true) moveDirection = Vector3.zero;

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
    private void MoveAnimation(Vector2 moveInput)
    {
        m_animator.SetFloat("Vertical Move", moveInput.y, 0.3f, Time.deltaTime);
        m_animator.SetFloat("Horizontal Move", moveInput.x, 0.2f, Time.deltaTime);
    }
    private void TriggerAnim(string triggername)
    {
        m_animator.SetTrigger($"{triggername}");
    }
    #endregion


    #region SwapWeapon
    void Swap()
    {
        if (sDwon1 && (!hasWeapons[0] || equipWeaponIndex == 0))
            return;
        if (sDwon2 && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;


        int weaponIndex = -1;
        if (sDwon1) weaponIndex = 0;
        if (sDwon2) weaponIndex = 1;

        if (sDwon1 || sDwon2)
        {
            if (equipWeapon != null)
                StartCoroutine("SetFalse", equipWeapon.gameObject);

            equipWeaponIndex = weaponIndex;
            equipWeapon = _weapons[weaponIndex].GetComponent<Weapon>();
            StartCoroutine("SetTrue", equipWeapon.gameObject);

            TriggerAnim("doSwap");
            isSwap = true;
            Invoke("SwapOut", 0.4f);
        }
    }

    void SwapOut()
    {
        isSwap = false;
    }

    IEnumerator SetFalse(GameObject go)
    {
        yield return new WaitForSeconds(0.4f);
        go.SetActive(false);
    }
    IEnumerator SetTrue(GameObject go)
    {
        yield return new WaitForSeconds(0.4f);
        go.SetActive(true);
    }
    #endregion
    private void GetWeapon(GameObject weapon)
    {
        if (weapon.tag == "Weapon")
        {
            Item item = weapon.GetComponent<Item>();
            int weaponIndex = item.value;
            hasWeapons[weaponIndex] = true;
        }
    }
}
