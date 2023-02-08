using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class Portal : MonoBehaviour
{
    [SerializeField]
    GameObject m_loadingObj;
    [SerializeField]
    Scrollbar m_loadingSlider;
    [SerializeField]
    Text m_loadText;
    [SerializeField]
    PlayerController m_player;
    [SerializeField]
    BaseScene m_scene;
    UI_Portal m_ui;
    RaycastHit hit;
    float m_detectDist = 3f;
    AsyncOperation m_LoadingInfo;
    private void Start()
    {
        m_ui = Managers.UI.MakeWorldSpaceUI<UI_Portal>(transform);
        m_ui.gameObject.SetActive(false);
    }

    void Loading()
    {
        if (m_LoadingInfo != null)
        {
            if (m_LoadingInfo.isDone)
            {

            }
            else
            {
                m_loadingSlider.size = m_LoadingInfo.progress;
                m_loadText.text = Mathf.CeilToInt(m_LoadingInfo.progress * 100.0f).ToString() + '%';
                if (m_loadingSlider.size >= 0.9f)
                    m_loadingSlider.size = Mathf.Clamp(1f, 0.9f, 1f);
            }
        }
    }
  
    public bool IsCloseToTarget()
    {
        var dir = m_player.transform.position - transform.position;
        if (Physics.Raycast(transform.position + Vector3.up * 0.6f, dir.normalized, out hit, m_detectDist, 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Background")))
        {
            if (hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    public void Update()
    {
        Loading();
        if (IsCloseToTarget())
        {
            m_ui.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                Scene scene = SceneManager.GetActiveScene();
                m_loadingObj.SetActive(true);
                if (scene.name.Equals("Game"))
                    m_LoadingInfo = m_scene.GoNextScene("Boss");
                else
                    m_LoadingInfo = m_scene.GoNextScene("Game");
            }
        }
        else m_ui.gameObject.SetActive(false);
    }
}
