using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Song
{
    //곡 이름 /22.04.01 by승주
    public string name;

    //곡 작곡가 /22.04.01 by승주
    public string composer;

    //곡 마다 bpm이 다르기 때문에 bpm따라 note 생성시간이 달라지게 하는 기능 /22.04.01 by승주
    public int bpm;

    //곡을 바꿀 때 마다 그 곡을 표현하는 디스크 이미지 변경하는 기능 /22.04.01 by승주
    public Sprite sprite;
}

public class StageMenu : MonoBehaviour
{

    public static StageMenu instance;

    [SerializeField] Song[] songList = null;
    [SerializeField] Text txtSongName = null;
    [SerializeField] Text txtSongComposer = null;
    [SerializeField] Image imgDisk = null;


    [SerializeField] GameObject TitleMenu = null;
    


    //현재 어떤 곡을 선택 했는 지 담고 있을 기능 /22.04.01 by승주
    int currentSong = 0;

    //시작하면 currentSong의 0번째 곡을 화면에 띄어주는 기능 /22.04.01 by승주
    void Start()
    {
        if (instance == null) instance = this;

        SettingSong();
    }

    

    //Next Button 누르면 정보가 다음 곡으로 바뀌게 만들어주는 기능 /22.04.01 by승주
    public void BtnNext()
    {
        AudioManager.instance.PlaySFX("Touch");

        //최대 곡 넘지 않게 하는 기능 /22.04.01 by승주
        if (++currentSong > songList.Length - 1)
            currentSong = 0;

        SettingSong();

        Debug.Log("다음으로 넘어가기");
    }

    //Prior Button 누르면 정보가 이전 곡으로 바뀌게 만들어주는 기능 /22.04.01 by승주
    public void BtnPrior()
    {
        AudioManager.instance.PlaySFX("Touch");

        //최대 곡 넘지 않게 하는 기능 /22.04.01 by승주
        if (--currentSong < 0)
            currentSong = songList.Length - 1;
        SettingSong();

        Debug.Log("이전으로 넘어가기");
    }

    //현재 곡 /22.04.01 by승주
    void SettingSong()
    {
        // 곡 List들 중에서 현재 고른 곡[currentSong]에 있는 name으로 변경 /22.04.01 by승주
        txtSongName.text = songList[currentSong].name;
        txtSongComposer.text = songList[currentSong].composer;
        imgDisk.sprite = songList[currentSong].sprite;

        AudioManager.instance.PlayBGM("BGM" + currentSong);

    }

    public void BtnBack()
    {
        TitleMenu.SetActive(true);
        this.gameObject.SetActive(false);

        AudioManager.instance.StopBGM();
    }
  
    public void BtnPlay()
    {
        int t_bpm = songList[currentSong].bpm;

        MusicGameManager.instance.GameStart(currentSong,t_bpm);
        this.gameObject.SetActive(false);

        
        

        
    }

    public void BtnReStart()
    {
        int t_bpm = songList[currentSong].bpm;

        MusicGameManager.instance.GameStart(currentSong, t_bpm);
        this.gameObject.SetActive(false);

        
    
    
    }

    
}
