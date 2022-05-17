using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyQuestManager : MonoBehaviour
{
    public int questId;
    public int qusetActionIndex;

    Dictionary<int, DailyQuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, DailyQuestData>();
        GenerateData();
    }


    void GenerateData()
    {
        //int []���� �ش� ����Ʈ�� ������ NPC ID�� �Է� ��� 22.05.17 ����
        questList.Add(10, new DailyQuestData("��ȭ �ϱ�",
                                                            new int[] { 1000,2000 }));
    }

    //NPC Id�� �ް� ����Ʈ ���θ� ��ȯ�ϴ� �Լ� ��� 22.05.17 ����
    public int GetQusetTalkIndex(int id)
    {
        return questId + qusetActionIndex;
    }

    public void CheckQuest()
    {
        qusetActionIndex++;
    }
}
