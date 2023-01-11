using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    PlayerController m_player;
    UI_Portal m_ui;
    RaycastHit hit;
    float m_detectDist = 3f;
    private void Start()
    {
        m_ui = Managers.UI.MakeWorldSpaceUI<UI_Portal>(transform);
        m_ui.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (IsCloseToTarget())
        {
            m_ui.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                Managers.Scene.LoadScene(Define.Scene.Boss);
            }
        }
        else m_ui.gameObject.SetActive(false);
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
}
