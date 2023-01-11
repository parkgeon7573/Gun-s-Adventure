using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private readonly static int Sword1 = Animator.StringToHash("Sword1");
    private readonly static int Sword2 = Animator.StringToHash("Sword2");
    private readonly static int Sword3 = Animator.StringToHash("Sword3");
    private readonly static int Hand1 = Animator.StringToHash("Hand1");
    private readonly static int Hand2 = Animator.StringToHash("Hand2");
    private readonly static int Hand3 = Animator.StringToHash("Hand3");
    PlayerAnimationEvent m_animEvt;
    Animator m_animator;
    PlayerStat m_stat;
    Weapon equipWeapon;
    CharacterController m_characterController;
    PlayerInput m_playerInput;
    PlayerController m_playerContorller;
    AttackAreaUnitFind[] m_attackArea;
    [SerializeField]
    GameObject[] m_SwordEffect;
    [SerializeField]
    GameObject[] _weapons;
    [SerializeField]
    GameObject m_attackAreaObj;

    public float CurrentSpeed => new Vector2(m_characterController.velocity.x, m_characterController.velocity.z).magnitude;

    [SerializeField]
    public bool[] hasWeapons;
    bool isSwap;
    [SerializeField]
    float Speed = 10.0f;
    float currentVelocityY;

    int equipWeaponIndex = 0;
    int m_comboIndex = 0;

    Queue<KeyCode> m_keyBuffer = new Queue<KeyCode>();
    Dictionary<int, SkillData> m_skillTable = new Dictionary<int, SkillData>();
    
    public bool IsAttack
    {
        get
        {
            if (GetCurrentAnim("Sword1") || GetCurrentAnim("Sword2") || GetCurrentAnim("Sword3")||
                GetCurrentAnim("Hand1") || GetCurrentAnim("Hand2") || GetCurrentAnim("Hand3"))
            {
                return true;
            }
            return false;
        }
    }
    List<int> m_SwordComboList = new List<int>() { Sword1, Sword2, Sword3 };
    private List<int> m_HandComboList = new List<int>() { Hand1, Hand2, Hand3 };

    bool GetCurrentAnim(string animname)
    {
        if (m_animator.GetCurrentAnimatorStateInfo(0).IsName(animname))
        {
            return true;
        }
        else return false;
    }
    void Start()
    {
        InitSkilTavle();
        m_attackArea = m_attackAreaObj.GetComponentsInChildren<AttackAreaUnitFind>();
        equipWeapon = _weapons[0].GetComponent<Weapon>();
        m_stat = GetComponent<PlayerStat>();
        m_animator = GetComponent<Animator>();
        m_playerInput = GetComponent<PlayerInput>();
        m_playerContorller = GetComponent<PlayerController>();
        m_characterController = GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        MoveUpdate(m_playerInput.MoveInput);
    }

    void ResetKeyBuffer()
    {
        m_keyBuffer.Clear();
    }
    void Attack()
    {
        if (m_playerInput.Attack)
        {
            if (equipWeapon == null)
                return;
            if (!isSwap && !IsAttack)
            {
                m_animator.SetTrigger(equipWeapon.name + 1);
            }
            else
            {
                m_keyBuffer.Enqueue(KeyCode.Mouse0);
                if (IsInvoking("ResetKeyBuffer"))
                {
                    CancelInvoke("ResetKeyBuffer");
                }
                Invoke(nameof(ResetKeyBuffer), 0.9f);
            }
        }
    }

    #region Movement
    public void MoveUpdate(Vector2 moveInput)
    {
        if (!IsAttack && !m_playerContorller.isTalk)
        {
            m_stat.Speed = Speed * moveInput.magnitude;
            var moveDirection = Vector3.Normalize(transform.forward * moveInput.y + transform.right * moveInput.x);
            if (isSwap == true) moveDirection = Vector3.zero;

            currentVelocityY += Time.deltaTime * Physics.gravity.y * 5;

            var velocity = moveDirection * m_stat.Speed + Vector3.up * currentVelocityY;

            m_characterController.Move(velocity * Time.deltaTime);
            if (m_characterController.isGrounded) currentVelocityY = 0f;
        }
    }
    public void Jump()
    {
        if (m_playerInput.Jump && !m_playerContorller.isTalk)
        {
            if (!m_characterController.isGrounded) return;
            currentVelocityY = m_stat.JumpVelocity;

            m_animator.SetTrigger("Jump");
        }
    }
    public void MoveAnimation(Vector2 moveInput)
    {
        m_animator.SetFloat("Vertical Move", moveInput.y, 0.3f, Time.deltaTime);
        m_animator.SetFloat("Horizontal Move", moveInput.x, 0.2f, Time.deltaTime);
    }
    #endregion
    void InitSkilTavle()
    {
        m_skillTable.Add(Sword1, new SkillData() { attackArea = 0, knockbackDist = 0.3f, Effet = 0, Damage = 20 });
        m_skillTable.Add(Sword2, new SkillData() { attackArea = 1, knockbackDist = 0.5f, Effet = 1, Damage = 30 });
        m_skillTable.Add(Sword3, new SkillData() { attackArea = 2, knockbackDist = 1f, Effet = 1, Damage = 40 });
        m_skillTable.Add(Hand1, new SkillData() { attackArea = 3, knockbackDist = 0.1f, Damage = 10 });
        m_skillTable.Add(Hand2, new SkillData() { attackArea = 3, knockbackDist = 0.1f, Damage = 15 });
        m_skillTable.Add(Hand3, new SkillData() { attackArea = 4, knockbackDist = 0.1f, Damage = 25 });
    }
    void Update()
    {
        MoveAnimation(m_playerInput.MoveInput);
        Attack();
        Swap();
        Jump();
    }

    #region SwapWeapon
    void Swap()
    {
        if (m_playerInput.Hand && (!hasWeapons[0] || equipWeaponIndex == 0))
            return;
        if (m_playerInput.Sword && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;


        int weaponIndex = 0;
        if (m_playerInput.Hand) weaponIndex = 0;
        if (m_playerInput.Sword) weaponIndex = 1;

        if (m_playerInput.Hand || m_playerInput.Sword)
        {
            if (equipWeapon != null)
                StartCoroutine(SetFalse(equipWeapon.gameObject));

            equipWeaponIndex = weaponIndex;
            equipWeapon = _weapons[weaponIndex].GetComponent<Weapon>();
            StartCoroutine(SetTrue(equipWeapon.gameObject));

            m_animator.SetTrigger("doSwap");
            isSwap = true;
            Invoke(nameof(SwapOut), 0.4f);
        }
    }

    void SwapOut()
    {
        isSwap = false;
    }
    void GetWeapon(GameObject weapon)
    {
        if (weapon.CompareTag("Weapon"))
        {
            Item item = weapon.GetComponent<Item>();
            int weaponIndex = item.value;
            hasWeapons[weaponIndex] = true;
        }
    }
    public void HasWeapon(int weaponidx)
    {
        hasWeapons[weaponidx] = true;
    }

    #endregion
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
    IEnumerator DestroyObj(GameObject gameObject)
    {
        yield return new WaitForSeconds(0.5f);
        GameObject.Destroy(gameObject);
    }

    #region Unity Animation Event Methods
    void AnimEvent_HandAttack()
    {
        SkillData skillData;
        if (!m_skillTable.TryGetValue(m_HandComboList[m_comboIndex], out skillData))
            return;
        var unitList = m_attackArea[skillData.attackArea].m_unitList;
        for (int i = 0; i < unitList.Count; i++)
        {
            var monster = unitList[i].GetComponent<MonsterController>();
            if (monster != null)
            {
                monster.SetDamage(skillData, m_stat.Attack);
            }
        }
    }
    void AnimEvent_SwordAttack()
    {
        SkillData skillData;
        if (!m_skillTable.TryGetValue(m_SwordComboList[m_comboIndex], out skillData))
            return;
        var unitList = m_attackArea[skillData.attackArea].m_unitList;
        GameObject go = Managers.Resource.Instantiate(m_SwordEffect[m_comboIndex], this.transform);
        go.transform.rotation = go.transform.rotation;
        StartCoroutine(DestroyObj(go));
        for (int i = 0; i < unitList.Count; i++)
        {
            var monster = unitList[i].GetComponent<MonsterController>();
            if (monster != null)
            {
                monster.SetDamage(skillData, m_stat.Attack);
            }
        }
    }

    void AnimEvent_SwordCombo()
    {
        bool isCombo = false;
        if (m_keyBuffer.Count > 0)
        {
            if (m_keyBuffer.Count > 3)
            {
                ResetKeyBuffer();
                isCombo = false;
            }
            else
            {
                //var key = m_keyBuffer.Dequeue();
                isCombo = true;
            }
        }
        if (isCombo)
        {
            m_comboIndex++;
            if (m_comboIndex >= m_SwordComboList.Count)
            {
                m_comboIndex = 0;
            }
            m_animator.SetTrigger(m_SwordComboList[m_comboIndex]);
        }
        else
        {
            m_comboIndex = 0;
        }
    }
    void AnimEvent_HandCombo()
    {
        bool isCombo = false;
        if (m_keyBuffer.Count > 0)
        {
            if (m_keyBuffer.Count > 5)
            {
                ResetKeyBuffer();
                isCombo = false;
            }
            else
            {
                var key = m_keyBuffer.Dequeue();
                isCombo = true;
            }

        }
        if (isCombo)
        {
            m_comboIndex++;
            if (m_comboIndex >= m_HandComboList.Count)
            {
                m_comboIndex = 0;
            }
            m_animator.SetTrigger(m_HandComboList[m_comboIndex]);
        }
        else
        {
            m_comboIndex = 0;
        }
    }

    #endregion
}