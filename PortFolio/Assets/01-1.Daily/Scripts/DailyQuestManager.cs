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
        //int []에는 해당 퀘스트에 연관된 NPC ID를 입력 기능 22.05.17 승주
        questList.Add(10, new DailyQuestData("대화 하기",
                                                            new int[] { 1000,2000 }));
    }

    //NPC Id를 받고 퀘스트 번로를 반환하는 함수 기능 22.05.17 승주
    public int GetQusetTalkIndex(int id)
    {
        return questId + qusetActionIndex;
    }

    public void CheckQuest()
    {
        qusetActionIndex++;
    }
}
