using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Song
{
    //�� �̸� /22.04.01 by����
    public string name;

    //�� �۰ /22.04.01 by����
    public string composer;

    //�� ���� bpm�� �ٸ��� ������ bpm���� note �����ð��� �޶����� �ϴ� ��� /22.04.01 by����
    public int bpm;

    //���� �ٲ� �� ���� �� ���� ǥ���ϴ� ��ũ �̹��� �����ϴ� ��� /22.04.01 by����
    public Sprite sprite;
}

public class StageMenu : MonoBehaviour
{
    
    [SerializeField] Song[] songList = null;
    [SerializeField] Text txtSongName = null;
    [SerializeField] Text txtSongComposer = null;
    [SerializeField] Image imgDisk = null;


    [SerializeField] GameObject TitleMenu = null;


    //���� � ���� ���� �ߴ� �� ��� ���� ��� /22.04.01 by����
    int currentSong = 0;

    //�����ϸ� currentSong�� 0��° ���� ȭ�鿡 ����ִ� ��� /22.04.01 by����
    void Start()
    {
        SettingSong();
    }

    //Next Button ������ ������ ���� ������ �ٲ�� ������ִ� ��� /22.04.01 by����
    public void BtnNext()
    {
        AudioManager.instance.PlaySFX("Touch");

        //�ִ� �� ���� �ʰ� �ϴ� ��� /22.04.01 by����
        if (++currentSong > songList.Length - 1)
            currentSong = 0;

        SettingSong();


    }

    //Prior Button ������ ������ ���� ������ �ٲ�� ������ִ� ��� /22.04.01 by����
    public void BtnPrior()
    {
        AudioManager.instance.PlaySFX("Touch");

        //�ִ� �� ���� �ʰ� �ϴ� ��� /22.04.01 by����
        if (--currentSong < 0)
            currentSong = songList.Length - 1;
        SettingSong();
    }

    //���� �� /22.04.01 by����
    void SettingSong()
    {
        // �� List�� �߿��� ���� �� ��[currentSong]�� �ִ� name���� ���� /22.04.01 by����
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

        GameManager.instance.GameStart(currentSong,t_bpm);
        this.gameObject.SetActive(false);

        
    }
}
