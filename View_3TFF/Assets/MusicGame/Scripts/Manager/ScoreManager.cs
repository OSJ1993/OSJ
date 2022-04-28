using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //Text 변수 선언.
    [SerializeField] Text txtScore = null;

    //맞출 때 마다 점수 올라가게 할 디폴트변수.
    [SerializeField] int increasaseScore = 10;

    //현재 점수를 담을 변수.
    int currentScore = 0;

    //판정에 따라 가중치가 다를 수 있게 해주는 변수 선언.
    [SerializeField] float[] weight = null;

    //콤보 보너스.
    [SerializeField] int comboBonusScore = 10;


    Animator myAnim;
    string animScoreUp = "ScoreUp";

    //점수가 올랐다는 것은 올바른 판정으로 맞췄다는 것 그래서 콤보도 올려주기.
    ComboManager theCombo;


    void Start()
    {
        theCombo = FindObjectOfType<ComboManager>();
        myAnim = GetComponent<Animator>();
        currentScore = 0;
        txtScore.text = "0";
    }

    //어떤 노트 판정을 받아왔는 확인.
    public void IncreasaseScore(int p_JudgementState)
    {
        //콤보 증가.
        theCombo.IncrcascCombo();

        //콤보 보너스 점수 계산.(현재 콤보/10) *10
        int t_currentCombo = theCombo.GetCurrentCombo();
        int t_bonusComboScore = (t_currentCombo / 10) * comboBonusScore;

        //가중치 계산.
        //임시 변수에 증가할 변수를 넣어주고 여기에 판정에 따른 가중치를 넣어주기.
        int t_increasaseScore = increasaseScore + t_bonusComboScore;
        t_increasaseScore = (int)(t_increasaseScore * weight[p_JudgementState]);

        //점수 반영.
        //이 점수를 현재 점수에 더하기.
        currentScore += t_increasaseScore;
        //문자열 형식 : 재화, 단위, 소수점, 날짜 표현 형식으로 변환시켜줌
        txtScore.text = string.Format("{0:#,##0}", currentScore);

        //애니 재생.
        myAnim.SetTrigger(animScoreUp);
    }
}
