using System.Collections;
using System.Collections.Generic;


public class DailyQuestData 
{
    //Quset 정보 기능 22.05.17 승주
    public string questName;
    public int[] npcId;

    //구조체 생성을 위해 매개변수 생성자 작성 기능 22.05.17 승주
    public DailyQuestData(string name, int[] npc)
    {
        questName = name;
        npcId = npc;
    }
}
