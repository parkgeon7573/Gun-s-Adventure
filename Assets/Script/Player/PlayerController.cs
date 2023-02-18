using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour, IUpdateableObject
{
    public Item[] items;
    Inventory inven;
    PlayerStat m_stat;
    PlayerMovement m_move;
    Camera m_camera;
    Animator m_animator;
    [SerializeField]
    bool toggleCameraRotation;

    [HideInInspector]
    public bool isTalk = false;
    public GameObject gamePanel;
    public Text healthText;

    public Image weapon1Img;
    public Image weapon2Img;
    public Image weapon3Img;
    float smoothness = 10.0f;

    public void OnClinked1()
    {
        m_move.DntHasWeapon(1);
        inven.AddItem(items[0]);
    }
    public void OnClinked2()
    {
        m_move.DntHasWeapon(2);
        inven.AddItem(items[1]);
    }
    public void SetDamage(int Dmg)
    {
        if (!m_move.isDefense)
        {
            float Damage = Dmg - m_stat.Defense;
            m_stat.Hp -= Damage;
        }
        else if (m_move.isDefense)
        {
            Managers.Sound.Play("Medieval Combat Sounds/Shield Metal 7_4", Define.Sound.Effect);
        }
    }
    public void DamageAnim()
    {
        m_animator.SetTrigger("GetHit");
    }
    public void Heal(int hp)
    {
        m_stat.Hp += hp;
        if(m_stat.Hp > m_stat.MaxHp)
        {
            m_stat.Hp = m_stat.MaxHp;
        }
    }
    //ī�޶� ���
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

    void SpinCamera()
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
        inven = GetComponentInChildren<Inventory>();
        m_animator = GetComponent<Animator>();
        m_move = GetComponent<PlayerMovement>();
        m_stat = GetComponent<PlayerStat>();
        m_camera = Camera.main;

    }
    void FixedUpdate()
    {        
        SpinCamera();
    }

    private void LateUpdate()
    {
        healthText.text = m_stat.Hp + "/" + m_stat.MaxHp;
        weapon1Img.color = new Color(1, 1, 1, m_move.hasWeapons[0] ? 1 : 0);
        weapon2Img.color = new Color(1, 1, 1, m_move.hasWeapons[1] ? 1 : 0);
        weapon3Img.color = new Color(1, 1, 1, m_move.hasWeapons[2] ? 1 : 0);
    }
    private void GameOver()
    {
        if(m_stat.Hp <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
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
        GameOver();
    }
}

    // Update is called once per frame
 
   