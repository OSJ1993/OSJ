using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingManager : MonoBehaviour
{
    //생성된 노트를 담는 List만들기. 판정범위에 있는 지 모든 노트를 비교해야함.
    public List<GameObject> boxNoteList = new List<GameObject>();

    //판정범위에 중심을 알려주는 Center변수 선언.
    [SerializeField] Transform Center = null;

    //다양한 판정범위를 보여줄 RectTransform[]배열도 선언.
    [SerializeField] RectTransform[] timingRect = null;

    //실제 판정 판독에 쓸 Vector2[] 선언. 여기에 RectTransform 합?을 정해줄것입니다.
    Vector2[] timingBoxs = null;


    EffectManager theEffect;
    ScoreManager theScoreManager;
    ComboManager theComboManager;


    void Start()
    {
        theEffect = FindObjectOfType<EffectManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();
        theComboManager = FindObjectOfType<ComboManager>();

        //타이밍 박스 설정.
        //timingBoxs 별 크기는 timingRect 갯?으로 넣어주기.
        timingBoxs = new Vector2[timingRect.Length];

        //timingBoxs 한정범위.
        for (int i = 0; i < timingRect.Length; i++)
        {
            //각각의 판정 범위 => 최소값 = 중심 - (이미지의 너비 / 2)
            //                    최대값 = 중심 + (이미지의 너비 / 2)
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                              Center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }


    //판정함수.
    public bool CheckTiming()
    {
        //리스트에 있는 노트들을 확인해서 판정 박스에 있는 노트를 찾아야함.
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            //각 노트를 x값을 따로 받아서 이 값으로 판정범위안에 들어왔는지 판단.
            //판정범위 최소값 <= 노트의 x값 <= 판정범위 최대값.
            float t_notePosX = boxNoteList[i].transform.localPosition.x;

            //각 노트마다 판정범위 안에 있는 지 확인해야하고 그 판정범위도 배열이기 때문에 반복문으로 실행.
            for (int x = 0; x < timingBoxs.Length; x++)
            {
                //조건문 노트에x값이 판정범위 안에 들어와 있는 지 각 x최소값 최대값y 비교
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {
                    //노트 제거
                    //0번째는 퍼팩트
                    //인덱스 0부터 확인하므로 판정순서도 Perfect -> Cool -> Good -> Bad

                    boxNoteList[i].GetComponent<Note>().HideNote();


                    //해당 노트 인덱스를 이용해서 노트를 빼주는 코드.
                   
                    boxNoteList.RemoveAt(i);

                    //이펙트 연출
                    //Bad타이밍에는 Effect가 나오지 않게 해주기.
                    //인덱스 0:퍼팩트 1:쿨 2 굿 3베드 이니 -1미만 0:퍼팩트 1:쿨 2 굿 일 때 이펙트재생.
                    if (x < timingBoxs.Length - 1)
                        theEffect.NoteHitEffect();

                    //파라미트 값을 x에게 넘겨주기.
                    theEffect.JudgementEffect(x);

                    //점수 증가
                    theScoreManager.IncreasaseScore(x);


                    return true;
                }
            }
        }

        //콤보 초기화
        theComboManager.ResetCombo();

        theEffect.JudgementEffect(timingBoxs.Length);
        return false;
    }
}
