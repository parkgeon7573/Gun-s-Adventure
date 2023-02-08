using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour,  IUpdateableObject
{
    TalkManager talkManager;
    [SerializeField]
    QuestManager questManager;
    [SerializeField]
    PlayerController m_player;
    [SerializeField]
    GameObject talkPanel;

    UI_Dialog m_ui;
    public Text talkText;

    RaycastHit hit;

    public bool isAction = false;
    public bool isSuccess = true;

    [HideInInspector]
    public int monsterDie = 0;

    [HideInInspector]
    public int npcId = 1000;
    float m_detectDist = 5f;
    int talkIndex = 0;
    // Start is called before the first frame update
    private void OnEnable()
    {
        UpdateManager.Instance.RegisterUpdateablObject(this);
    }

    private void OnDisable()
    {
        if (UpdateManager.Instance != null)
            UpdateManager.Instance.DeregisterUpdateableObject(this);
    }
    void Start()
    {
        m_ui = Managers.UI.MakeWorldSpaceUI<UI_Dialog>(transform);
        m_ui.gameObject.SetActive(false);
        talkManager = GameObject.Find("TalkManager").GetComponent<TalkManager>();
        Debug.Log(questManager.CheckQuest());
    }

    void Talk()
    {
        int questTalkIdex = questManager.GetQuestTalkIndex(npcId);
        string talkData = talkManager.GetTalk(npcId + questTalkIdex, talkIndex);
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(npcId));
            return;
        }
        talkText.text = talkData;

        isAction = true;
        talkIndex++;
    }
    public void Action()
    {
        Talk();
        talkPanel.SetActive(isAction);
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

    public void OnUpdate()
    {
        if (IsCloseToTarget())
        {
            m_ui.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.R))
            {
                Action();
            }
        }
        else m_ui.gameObject.SetActive(false);
        if (monsterDie == 5)
        {
            questManager.questId = 30;
            questManager.questActionIndex = 0;
            monsterDie = 0;
        }
        if (talkManager.BossDie == true)
        {
            questManager.questId = 50;
            questManager.questActionIndex = 0;
        }
    }
}
