using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingManager : MonoBehaviour
{
    //판정들을 기록할 배열 변수 선언 22.03.24 by승주
    int[] judgementRecord = new int[5];

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
    StageManager theStageManager;
    PlayerController thePlayer;

    void Start()
    {
        theEffect = FindObjectOfType<EffectManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();
        theComboManager = FindObjectOfType<ComboManager>();
        theStageManager = FindObjectOfType<StageManager>();
        thePlayer = FindObjectOfType<PlayerController>();

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





                    if (CheckCanNextPlate())
                    {
                        //점수 증가
                        theScoreManager.IncreasaseScore(x);

                        //발판 등장 /22.03.24 by승주
                        theStageManager.ShowNextPlate();

                        //파라미트 값을 x에게 넘겨주기. 판정연출
                        theEffect.JudgementEffect(x);

                        //판정기록 /22.03.24 by승주
                        judgementRecord[x]++;

                    }
                    else
                    {
                        theEffect.JudgementEffect(5);
                    }
                    return true;
                }
            }
        }

        //콤보 초기화
        theComboManager.ResetCombo();

        theEffect.JudgementEffect(timingBoxs.Length);

        //판정 기록 /22.03.24 by승주
        MissRecord();

        return false;
    }

    //다음 발판이 가능한 지 확인하는 함수 22.03.24 by승주
    bool CheckCanNextPlate()
    {
        //Physics.Raycast(): 가상의 광선을 쏴서 맞은 대상의 정보를 가져오는 함수 22.03.24 by승주
        //(광선 위치, 방향, 충돌 정보, 길이) 22.03.24 by승주
        // 플레이어에 destPos 위치에서 레이저를 아래방향으로 쏜다 그리고 부딪힌 결과를 내보내 준다 레이저에 부딪힌 정보는 t_hitInfo 담기게 된다.  22.03.24 by승주
        if (Physics.Raycast(thePlayer.destPos, Vector3.down, out RaycastHit t_hitInfo, 1.1f))
        {
            //부딪힌 녀석이 BasicPlate 확인하기 위한 조건문 /22.03.24 by승주
            if (t_hitInfo.transform.CompareTag("BasicPlate"))
            {
                //BasicPlate라면 가져와서 저장 /22.03.24 by승주
                BasicPlate t_plate = t_hitInfo.transform.GetComponent<BasicPlate>();

                // flag 이 값이 true 인지 확인  /22.03.24 by승주
                if (t_plate.flag)
                {
                    //이미 밟은 발판은 또 다시 밟을 경우 새로운 발판나오는것을 막아준다.
                    t_plate.flag = false;
                    return true;
                }

            }
        }

        //모든 조건 걸리지 않고 빠져 나올 경우 return false 다음 발판을 나올 수 없다는 걸 알려준다./22.03.24 by승주
        return false;
    }

    public int[] GetJudgementRecord()
    {
        return judgementRecord;
    }

    public void MissRecord()
    {
        //판정 기록 /22.03.24 by승주
        judgementRecord[4]++;
    }
}
