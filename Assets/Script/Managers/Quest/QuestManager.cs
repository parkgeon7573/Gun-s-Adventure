using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    [SerializeField]
    Portal m_portal;
    [SerializeField]
    PlayerMovement player;
    PlayerController m_player;
    Inventory playerInven;
    [SerializeField]
    NPCController questController;
    [SerializeField]
    Item item;
    Dictionary<int, QuestData> questList;
    // Start is called before the first frame update
    void Awake()
    {
        m_player = player.GetComponentInChildren<PlayerController>();
        if(questId > 30) m_portal.gameObject.SetActive(true);
        else
            m_portal.gameObject.SetActive(false);
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }
    private void Start()
    {
        playerInven = m_player.GetComponentInChildren<Inventory>();
    }
    private void Update()
    {
        if (questId >= 50)
            player.HasWeapon(1);
    }
        // Update is called once per frame
        void GenerateData()
    {
        questList.Add(10, new QuestData("주민과 대화하기", new int[] { 1000, 1000 }));
        questList.Add(20, new QuestData("몬스터 해치우기", new int[] { 1000 }));
        questList.Add(30, new QuestData("의뢰 해결보고하기", new int[] { 1000, 1000 }));
        questList.Add(40, new QuestData("보스몬스터 해치우기", new int[] { 1000, 1000 }));
        questList.Add(50, new QuestData("게임 클리어 하기", new int[] { 1000, 1000 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }
    public string CheckQuest(int id)
    {
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;


        ControlObject();
        if (questActionIndex == questList[questId].npcId.Length)
        {
            if (questController.isSuccess)
                NextQuest();
            else
                questActionIndex--;
        }
            

        return questList[questId].qeustName;
    }
    public string CheckQuest()
    {
        return questList[questId].qeustName;
    }
    public void NextQuest()
    {        
        questId += 10;
        if (questId > 50)
            questId = 50;
        questActionIndex = 0;
    }
    public void NextQuest(int value)
    {
        questId += 10;
        questActionIndex = 0;
    }
    void ControlObject()
    {
        switch (questId)
        {
            case 10:
                questController.isSuccess = true;
                break;
            case 20:
                if (questActionIndex == 1)
                    if (questController.monsterDie == 0)
                        questController.isSuccess = false;
                break;
            case 30:
                if(questActionIndex == 1)
                {
                    m_player.Heal(100);
                }
                break;
            case 40:
                if (questActionIndex == 1)
                {
                    //player.HasWeapon(1);
                    playerInven.AddItem(item);
                    m_portal.gameObject.SetActive(true);
                    questController.isSuccess = false;
                }                    
                break;
            case 50:
                break;
        }
    }
}
