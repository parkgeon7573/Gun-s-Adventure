using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerStat m_stat;
    PlayerMovement m_move;
    Camera m_camera;
    GameObject nearObject;
    [SerializeField]
    bool toggleCameraRotation;

    [HideInInspector]
    public bool isTalk = false;
    public GameObject gamePanel;
    public Text healthText;

    public Image weapon1Img;
    public Image weapon2Img;
    float smoothness = 10.0f;
    public void SetDamage(int Dmg)
    {
        float Damage = Dmg - m_stat.Defense;
        m_stat.Hp -= Damage;
    }
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
        m_move = GetComponent<PlayerMovement>();
        m_stat = GetComponent<PlayerStat>();


        m_camera = Camera.main;
        Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
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
    }
}

    // Update is called once per frame
 
   