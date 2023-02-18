using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    //[HideInInspector]
    public bool BossDie = false;
    Dictionary<int, string[]> talkData;
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void GenerateData()
    {
        talkData.Add(1000, new string[] { "안녕?", "이곳에 처음왔구나" });


        talkData.Add(10 + 1000, new string[] { "도와줘", "이 마을은 위험에 쳐해있어" });
        talkData.Add(11 + 1000, new string[] { "몬스터들이", "우리를 괴롭히고있어" });
        talkData.Add(20 + 1000, new string[] { "마을에 있는", "몬스터5마리를 해치워줘" });


        talkData.Add(30 + 1000, new string[] { "해치워 줬구나", "고마워, 체력을 회복시켜 줄게" });
        talkData.Add(31 + 1000, new string[] { "하지만 아직 문제가 있어", "보스몬스터가 남아 있는한 몬스터들은 계속 생길거야" });

        talkData.Add(40 + 1000, new string[] { "던전 입구에 포탈을 열어줄게 보스몬스터를 잡아줘", "이 검을 가지고 가 인벤토리에 넣어줄게" });

        talkData.Add(50 + 1000, new string[] { "보스를 잡아주었구나", "덕분에 우리를 괴롭히는 몬스터가 없어졌어" });

    }
    public void BossDead()
    {
        BossDie = true;
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
                return GetTalk(id - id % 100, talkIndex);
            else
                return GetTalk(id - id % 10, talkIndex);
        }
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
