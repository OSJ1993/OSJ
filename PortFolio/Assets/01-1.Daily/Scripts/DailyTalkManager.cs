using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class DailyTalkManager : MonoBehaviour
{

    Dictionary<int, string[]> talkData;


    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerataData();


    }


    void GenerataData()
    {
        //Talk Data ��� 22.05.17 ����
        //NPC :1000
        //Box: 100
        //��ȭ �ϳ����� ���� ������ ��� �����Ƿ� string[] �迭ȭ ��Ų ��� 22.05.17 ����
        talkData.Add(1000, new string[] { "�ȳ�?",
                                                    "�� ���� ó�� �Ա���?",
                                                    "������ �ݰ���:0",
                                                    "���� ���� �Ϸ��� �� ����� ��Ȱ ���ٰ���"});

        talkData.Add(2000, new string[] { "�ȳ�? ���� ",
                                                    "�� ���� ó������?",
                                                    "������ �ݰ���",
                                                    "���� ���� �Ϸ��� �� ����� ��Ȱ ���ٰ���"});


        talkData.Add(10 + 100, new string[] { "���� ���丮",
                                                 "�������� ���Ӱ����� �ϸ鼭",
                                                 "� �о߰� �ֳ� Ȯ�� ��",
                                                 "�������� ȸ�� ���Ѽ�",
                                                 "��� ���� ������ �����Դϴ�."});

        talkData.Add(10 + 200, new string[] { "�� ���� �´°ɱ�??",
                                                         "�Ƴ� �� �� ��������",
                                                         "��⸦ ������"});

        talkData.Add(10 + 300, new string[] { "��� �� �� ���� ����....",
                                                         "�������� ����....",
                                                         "�׷��� �ƹ��� ������",
                                                         "���� �ʾ�...."});

        talkData.Add(10 + 400, new string[] { "������ �Ȱ��� ��鼭 ",
                                                    "�ٸ� �̷���",
                                                    "����ϴ� ���� ",
                                                    "���ź� �ʱ������̴�",
                                                    "-���ν�Ÿ��"});
        talkData.Add(10 + 500, new string[] { "ħ�� �׸�.... ",
                                                         "�̷� ���� �� ������",
                                                         "�ؾߵ� ",
                                                         "�������� ����!"});

        //Quest Talk ��� 22.05.17 ����
        talkData.Add(10 + 1000, new string[] { "���� �� �� �־� ",
                                                          "���� �� ���� ���� �������� ��" ,
                                                          "�������!"});





    }

    //��ȭ �ҷ����� ��� 22.05.17 ����
    public string GetTalk(int id, int talkIndex)
    {

        //talkIndex�� ��ȭ�� ���� ������ ���Ͽ� ���� Ȯ�� �ϴ� ��� 22.05.17 ����
        if (talkIndex == talkData[id].Length)
        {
            return null;


        }
        else
            return talkData[id][talkIndex];




    }
}
