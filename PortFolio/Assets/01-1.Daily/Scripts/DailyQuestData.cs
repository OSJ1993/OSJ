using System.Collections;
using System.Collections.Generic;


public class DailyQuestData 
{
    //Quset ���� ��� 22.05.17 ����
    public string questName;
    public int[] npcId;

    //����ü ������ ���� �Ű����� ������ �ۼ� ��� 22.05.17 ����
    public DailyQuestData(string name, int[] npc)
    {
        questName = name;
        npcId = npc;
    }
}
