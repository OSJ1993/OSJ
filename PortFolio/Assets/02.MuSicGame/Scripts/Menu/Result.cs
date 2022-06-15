using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Result : MonoBehaviour
{
    [SerializeField] GameObject goUI = null;

    [SerializeField] Text[] txtCount = null;
    [SerializeField] Text txtCoin = null;
    [SerializeField] Text txtScore = null;
    [SerializeField] Text txtMaxCombo = null;

    GoalPlate thegoalPlate;

    public GameObject boSang;
    public GameObject btnGroup;

    public GameObject inGameButton;

    ScoreManager theScore;
    ComboManager theCombo;
    TimingManager theTiming;

    //
    void Start()
    {
        theScore = FindObjectOfType<ScoreManager>();
        theCombo = FindObjectOfType<ComboManager>();
        theTiming = FindObjectOfType<TimingManager>();

        thegoalPlate = GetComponent<GoalPlate>();
    }

    public void ShowResult()
    {
        FindObjectOfType<CenterFlame>().ResetMusic();

        //���â�� ������ ����� ���� 22.03.29 by����
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

        //�ݺ����� ���� �� �ؽ�Ʈ ������ŭ �ݺ� �����Ű�� ��� /22.03.24 by����
        for (int i = 0; i < txtCount.Length; i++)
        {
            //Ÿ���� �� �±� ������ ��Ʈ�� Ÿ������ ��ȯ���Ѽ� ��� /22.03.24 by����
            txtCount[i].text = string.Format("{0:#,##0}", t_judgement[i]);
        }

        txtScore.text = string.Format("{0:#,##0}", t_currentScore);
        txtMaxCombo.text = string.Format("{0:#,##0}", t_maxCombo);

        inGameButton.SetActive(false);
    }

    public void BtnMainMenu()
    {
        goUI.SetActive(false);

        //MusicGameManager.instance.MainMenu();

        theCombo.ResetCombo();

        SceneManager.LoadScene(2);
    }

    public void BtnReStart()
    {
        goUI.SetActive(false);
        theCombo.ResetCombo();
        boSang.SetActive(false);
        inGameButton.SetActive(true);
    }

    //���� ��� ���� ��� 22.06.07 ����
    public void BtnClear()
    {
        btnGroup.SetActive(false);

        goUI.SetActive(true);
        if (gameObject == true)
        {
            boSang.SetActive(true);
            
        }
        else if(gameObject == false)
        {
            boSang.SetActive(false);
        }
       
        
    }

    public void OnClickLoadGame()
    {
        SceneManager.LoadScene("01-1.Daily");
        ClearManager.stageClear[0] = true;
        

        BtnClear();
    }


   



}
