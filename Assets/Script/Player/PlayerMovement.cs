using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour, IUpdateableObject
{    
    private readonly static int Sword1 = Animator.StringToHash("Sword1");
    private readonly static int Sword2 = Animator.StringToHash("Sword2");
    private readonly static int Sword3 = Animator.StringToHash("Sword3");
    private readonly static int Hand1 = Animator.StringToHash("Hand1");
    private readonly static int Hand2 = Animator.StringToHash("Hand2");
    private readonly static int Hand3 = Animator.StringToHash("Hand3");

    Animator m_animator;
    PlayerStat m_stat;
    Weapon equipWeapon;
    CharacterController m_characterController;
    PlayerInput m_playerInput;
    PlayerController m_playerContorller;
    AttackAreaUnitFind[] m_attackArea;
    [SerializeField]
    CameraMove m_camera;
    [SerializeField]
    Image aim;
    [SerializeField]
    QuestManager questManager;
    TalkManager talkManager;
    [SerializeField]
    Portal portal;
    [SerializeField]
    GameObject[] m_SwordEffect;
    [SerializeField]
    GameObject[] _weapons;
    [SerializeField]
    GameObject m_attackAreaObj;
    [SerializeField]
    Transform bulletPos;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    AudioClip step;
    AudioSource soundSource;
    public float CurrentSpeed => new Vector2(m_characterController.velocity.x, m_characterController.velocity.z).magnitude;
        
    public bool[] hasWeapons;
    public bool isDefense;
    bool isSwap;
    [SerializeField]
    float stepSondSpeed = 3f;
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
                GetCurrentAnim("Hand1") || GetCurrentAnim("Hand2") || GetCurrentAnim("Hand3") || GetCurrentAnim("Mace1"))
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
        MoveAnimation(m_playerInput.MoveInput);
        Attack();
        Swap();
        Jump();
        DefenseAttack();
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
        talkManager = GameObject.Find("TalkManager").GetComponent<TalkManager>();
        StartPosition();
        soundSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        MoveUpdate(m_playerInput.MoveInput);
    }

    void StartPosition()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (questManager == null)
                m_characterController.enabled = true;
            else if (talkManager.BossDie == true)
            {                
                transform.position = portal.transform.position;
                m_camera.transform.position = transform.position;
                HasWeapon(1);
                HasWeapon(2);
            }
                
        }
        else if(SceneManager.GetActiveScene().name == "Boss")
        {
            transform.position = portal.transform.position;
        }
    }
    void ResetKeyBuffer()
    {
        m_keyBuffer.Clear();
    }
    void Attack()
    {
        if (m_playerInput.Attack)
        {
            if (GetCurrentAnim("Mace1")) return;
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
    bool currentjump()
    {
        if (currentVelocityY < -10 || currentVelocityY > 10)
        {
            return false;
        }
        else return true;
    }
    public void MoveUpdate(Vector2 moveInput)
    {
        if (m_stat.Speed > 1 && !soundSource.isPlaying && currentjump() && m_characterController.isGrounded)
        {
            soundSource.pitch = stepSondSpeed;
            soundSource.clip = step;
            soundSource.Play();
        }
        else if (m_stat.Speed <= 1 || !currentjump())
        {
            soundSource.Stop();
        }
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
    public void DefenseAttack()
    {
        if(equipWeaponIndex == 1 || equipWeaponIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isDefense = true;
                m_animator.SetBool("Defense", true);
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                isDefense = false;
                m_animator.SetBool("Defense", false);
            }
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
        m_skillTable.Add(Sword1, new SkillData() { attackArea = 0, knockbackDist = 0.3f, Effet = 0, Damage = 15 });
        m_skillTable.Add(Sword2, new SkillData() { attackArea = 1, knockbackDist = 0.5f, Effet = 1, Damage = 20 });
        m_skillTable.Add(Sword3, new SkillData() { attackArea = 2, knockbackDist = 1f, Effet = 1, Damage = 35 });
        m_skillTable.Add(Hand1, new SkillData() { attackArea = 3, knockbackDist = 0.1f, Damage = 10 });
        m_skillTable.Add(Hand2, new SkillData() { attackArea = 3, knockbackDist = 0.1f, Damage = 15 });
        m_skillTable.Add(Hand3, new SkillData() { attackArea = 4, knockbackDist = 0.1f, Damage = 25 });
    }

    #region SwapWeapon
    void Swap()
    {
        aim.color = new Color(1, 1, 1, equipWeaponIndex == 2  ? 1 : 0);

        if (Input.GetKeyDown(KeyCode.Alpha1))
            Debug.Log("22");
        if (m_playerInput.Hand && (!hasWeapons[0] || equipWeaponIndex == 0))
            return;
        if (m_playerInput.Sword && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;
        if (m_playerInput.Mace && (!hasWeapons[2] || equipWeaponIndex == 2))
            return;

        int weaponIndex = 0;
        if (m_playerInput.Hand) weaponIndex = 0;
        if (m_playerInput.Sword) weaponIndex = 1;
        if (m_playerInput.Mace) weaponIndex = 2;

        if (m_playerInput.Hand || m_playerInput.Sword || m_playerInput.Mace)
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
    public void GetWeapon(GameObject weapon)
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
    public void DntHasWeapon(int weaponidx)
    {
        hasWeapons[weaponidx] = false;
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
    void AnimEvent_MaceAttack()
    {
        GameObject gameObject1 = Instantiate(bullet, bulletPos.position, transform.rotation);
        Managers.Sound.Play("Effect/Ice Spell 22", Define.Sound.Effect);
    }
    void AnimEvent_HandAttack()
    {
        SkillData skillData;
        if (!m_skillTable.TryGetValue(m_HandComboList[m_comboIndex], out skillData))
            return;
        var unitList = m_attackArea[skillData.attackArea].m_unitList;
        for (int i = 0; i < unitList.Count; i++)
        {
            var monster = unitList[i].GetComponent<MonsterController>();
            var monsterstat = unitList[i].GetComponent<SkeletonStat>();
            if (monster != null && monster.isActiveAndEnabled && monsterstat.Hp > 0)
            {
                Managers.Sound.Play("Effect/Stab 22_1", Define.Sound.Effect);
                monster.SetDamage(m_stat.Attack, skillData);
            }
        }
    }
    void AnimEvent_Swing()
    {
        Managers.Sound.Play("Effect/Swing", Define.Sound.Effect);
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
            var boss = unitList[i].GetComponent<BossController>();
            var monster = unitList[i].GetComponent<MonsterController>();
            if (monster != null && monster.isActiveAndEnabled || boss != null)
            {
                Managers.Sound.Play("Effect/Stab 22_1", Define.Sound.Effect);
                monster.SetDamage(m_stat.Attack, skillData);
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