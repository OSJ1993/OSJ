using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] GameObject goUI = null;

    [SerializeField] Text[] txtCount = null;
    [SerializeField] Text txtCoin = null;
    [SerializeField] Text txtScore = null;
    [SerializeField] Text txtMaxCombo = null;

    ScoreManager theScore;
    ComboManager theCombo;
    TimingManager theTiming;

    //
    void Start()
    {
        theScore = FindObjectOfType<ScoreManager>();
        theCombo = FindObjectOfType<ComboManager>();
        theTiming = FindObjectOfType<TimingManager>();
    }

    public void ShowResult()
    {
        FindObjectOfType<CenterFlame>().ResetMusic();

        //결과창이 나오면 오디오 종료 22.03.29 by승주
        AudioManager.instance.StopBGM();

        goUI.SetActive(true);

        for (int i = 0; i < txtCount.Length; i++)
        {
            txtCount[i].text = "0";
        }

        txtCoin.text = "0";
        txtScore.text = "0";
        txtMaxCombo.text = "0";

        int[] t_judgement = theTiming.GetJudgementRecord();
        int t_currentScore = theScore.GetCurrentScore();
        int t_maxCombo = theCombo.GetMaxCombo();

        //반복문을 통해 각 텍스트 갯수만큼 반복 실행시키는 기능 /22.03.24 by승주
        for (int i = 0; i < txtCount.Length; i++)
        {
            //타입이 안 맞기 때문에 스트링 타입으로 변환시켜서 사용 /22.03.24 by승주
            txtCount[i].text = string.Format("{0:#,##0}", t_judgement[i]);
        }

        txtScore.text = string.Format("{0:#,##0}", t_currentScore);
        txtMaxCombo.text = string.Format("{0:#,##0}", t_maxCombo);
    }

    public void BtnMainMenu()
    {
        goUI.SetActive(false);

        MusicGameManager.instance.MainMenu();

        theCombo.ResetCombo();
    }

    public void BtnReStart()
    {
        goUI.SetActive(false);
        theCombo.ResetCombo();
    }

}
