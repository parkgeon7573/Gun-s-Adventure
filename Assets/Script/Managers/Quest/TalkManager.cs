using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
  
    Dictionary<int, string[]> talkData;
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        talkData.Add(1000, new string[] { "안녕?", "이곳에 처음왔구나" });


        talkData.Add(10 + 1000, new string[] { "도와줘", "이 마을은 위험에 쳐해있어" });
        talkData.Add(11 + 1000, new string[] { "몬스터들이", "우리를 괴롭히고있어" });
        talkData.Add(20 + 1000, new string[] { "마을에 있는", "몬스터5마리를 해치워줘" });


        talkData.Add(30 + 1000, new string[] { "해치워 줬구나", "고마워" });
        talkData.Add(31 + 1000, new string[] { "하지만 아직 문제가 있어", "보스몬스터가 남아 있는한 몬스터들은 계속 생길거야" });

        talkData.Add(40 + 1000, new string[] { "던전 으로 가는 포탈을 열어줄게 보스몬스터를 잡아줘", "이 검을 가지고 가 2 번을 누르면 사용할수 있어" });

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
