using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] goGameUI = null;

    //Title화면 보여주기
    [SerializeField] GameObject goTitleUI = null;

    //싱글턴
    public static GameManager instance;

    public bool isStartGame = false;

    ComboManager thecombo;
    ScoreManager theScore;
    TimingManager thetiming;
    StatusManager theStatus;
    PlayerController thePlayer;
    StageManager theStage;
    NoteManager theNote;

    // SerializeField 쓴 이유 CenterFlame 비활성화 되어있기 때문 22.04.01 by승주
    [SerializeField] CenterFlame theMusic=null;

    void Start()
    {
        instance = this;

        theNote = FindObjectOfType<NoteManager>();
        theStage = FindObjectOfType<StageManager>();
        thecombo = FindObjectOfType<ComboManager>();
        theScore = FindObjectOfType<ScoreManager>();
        thetiming = FindObjectOfType<TimingManager>();
        theStatus = FindObjectOfType<StatusManager>();
        thePlayer = FindObjectOfType<PlayerController>();
    }

    
    public void GameStart(int p_songNum,int p_bpm)
    {
        for (int i = 0; i < goGameUI.Length; i++)
        {
            goGameUI[i].SetActive(true);
           
        }
        theMusic.bgmName = "BGM" + p_songNum;
        theNote.bpm = p_bpm;
        theStage.RemoveStage();
        theStage.SettingStage(p_songNum);
        thecombo.ResetCombo();
        theScore.Initialized();
        thetiming.Initialized();
        theStatus.Initialized();
        thePlayer.Initialized();

        AudioManager.instance.StopBGM();

        isStartGame = true;
    }

    public void MainMenu()
    {
        for (int i = 0; i < goGameUI.Length; i++)
        {
            goGameUI[i].SetActive(false);

        }

        goTitleUI.SetActive(true);
    }
}
