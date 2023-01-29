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
        talkData.Add(1000, new string[] { "안녕?",
                                                    "이 곳에 처음 왔구나?",
                                                    "만나서 반가워:0",
                                                    "게임 시작 하려면 이 녀셕이 역활 해줄거임"});

        talkData.Add(2000, new string[] { "안녕? 못해 ",
                                                    "이 곳에 처ㅇㅇㅇ?",
                                                    "만나서 반가워",
                                                    "게임 시작 하려면 이 녀셕이 역활 해줄거임"});


        talkData.Add(10 + 100, new string[] { "게임 스토리",
                                                 "여러가지 게임경험을 하면서",
                                                 "어떤 분야가 있나 확인 후",
                                                 "자존감을 회복 시켜서",
                                                 "취업 위해 힘쓰는 게임입니다."});

        talkData.Add(10 + 200, new string[] { "이 길이 맞는걸까??",
                                                         "아냐 할 수 있을꺼야",
                                                         "용기를 가지자"});

        talkData.Add(10 + 300, new string[] { "취업 할 수 있을 꺼야....",
                                                         "자존감이 없어....",
                                                         "그러니 아무도 만나고",
                                                         "싶지 않아...."});

        talkData.Add(10 + 400, new string[] { "어제와 똑같이 살면서 ",
                                                    "다른 미래를",
                                                    "기대하는 것은 ",
                                                    "정신병 초기증세이다",
                                                    "-아인슈타인"});
        talkData.Add(10 + 500, new string[] { "침대 그만.... ",
                                                         "이럴 수록 더 열심히",
                                                         "해야돼 ",
                                                         "도망가지 말자!"});

        //Quest Talk 기능 22.05.17 승주
        talkData.Add(10 + 1000, new string[] { "나는 할 수 있어 ",
                                                          "아직 꽃 피지 못한 봉오리일 뿐" ,
                                                          "취업하자!"});





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
