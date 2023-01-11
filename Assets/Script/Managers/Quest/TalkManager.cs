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
        talkData.Add(1000, new string[] { "�ȳ�?", "�̰��� ó���Ա���" });


        talkData.Add(10 + 1000, new string[] { "������", "�� ������ ���迡 �����־�" });
        talkData.Add(11 + 1000, new string[] { "���͵���", "�츮�� ���������־�" });
        talkData.Add(20 + 1000, new string[] { "������ �ִ�", "����5������ ��ġ����" });


        talkData.Add(30 + 1000, new string[] { "��ġ�� �ᱸ��", "����" });
        talkData.Add(31 + 1000, new string[] { "������ ���� ������ �־�", "�������Ͱ� ���� �ִ��� ���͵��� ��� ����ž�" });

        talkData.Add(40 + 1000, new string[] { "���� ���� ���� ��Ż�� �����ٰ� �������͸� �����", "�� ���� ������ �� 2 ���� ������ ����Ҽ� �־�" });

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
