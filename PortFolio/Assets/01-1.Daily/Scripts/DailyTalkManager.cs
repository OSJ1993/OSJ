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
        //Talk Data 기능 22.05.17 승주
        //NPC :1000
        //Box: 100
        //대화 하나에는 여러 문장이 들어 있으므로 string[] 배열화 시킨 기능 22.05.17 승주
        talkData.Add(1000, new string[] { "안녕? :0",
                                                    "이 곳에 처음 왔구나?:1",
                                                    "만나서 반가워:0",
                                                    "게임 시작 하려면 이 녀셕이 역활 해줄거임"});
        talkData.Add(2000, new string[] { "안녕? 못해 :0",
                                                    "이 곳에 처ㅇㅇㅇ?:1",
                                                    "만나서 반가워:0",
                                                    "게임 시작 하려면 이 녀셕이 역활 해줄거임"});


        talkData.Add(10+100, new string[] { "노력해야지?",
                                                 "어서 컴퓨터를 켜",
                                                 "이놈은 내 자료 성적 등을 보면서 스스로 이야기 할 수 있게 해줄 역활"});

        talkData.Add(10+200, new string[] { "해도 돼?",
                                                 "그냥 쉬어",
                                                 "그러니 한강을 가렴"});

        //Quest Talk 기능 22.05.17 승주
        talkData.Add(10 + 1000, new string[] { "퀘스트 대화 끝나고 게임 실행을 시켜야 :0 ?",
                                                          "하는 데 그 기능은 어떻게 찾냐 또.... :1" ,
                                                          "찾을 수 있겠지...?:0"});
       




    }

    //대화 불러오는 기능 22.05.17 승주
    public string GetTalk(int id, int talkIndex)
    {
        
        //talkIndex와 대화의 문자 갯수를 비교하여 끝을 확인 하는 기능 22.05.17 승주
        if (talkIndex == talkData[id].Length)
        {
            return null;
           

        }
        else
            return talkData[id][talkIndex];

        

       
    }
}
