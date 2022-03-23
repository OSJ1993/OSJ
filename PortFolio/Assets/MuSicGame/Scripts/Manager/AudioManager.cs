using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//인스팩트 창에서 수정 가능하게 [System.Serializable] /22.03.23 by승주
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] Sound[] sfx = null;
    [SerializeField] Sound[] bgm = null;

    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] AudioSource[] sfxPlayer = null;

    void Start()
    {
        instance = this;
    }


    //배경음 재생 /22.03.23 by승주
    public void PlayBGM(string p_bgmName)
    {
        //Sound class에 있는 string name 과 PlayBGM(string p_bgmName)의 값이 일치하는 지 확인 하기 위해 for문 사용 /22.03.23 by승주

        //int i=0부터 bgm 갯수 만큼 비교
        for (int i = 0; i < bgm.Length; i++)
        {
            //조건문을 비교를 해 주는데 드로우파라미터?의 이름이 해당 bgm안에 있는 배열 index와 일치 하는 지 확인하는 함수 /22.03.23 by승주
            if (p_bgmName == bgm[i].name)
            {
                //일치 하는 index(이름)가 있다면 bgmPlayer에 clip을 i번쨰 index에 clip으로 대체 /22.03.23 by승주
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();
            }
        }

    }
    //배경음 정지 /22.03.23 by승주
    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    //효과음 재생 /22.03.23 by승주
    public void PlaySFX(string p_sfxName)
    {
        //Sound class에 있는 string name 과 PlayBGM(string p_bgmName)의 값이 일치하는 지 확인 하기 위해 for문 사용 /22.03.23 by승주

        //int i=0부터 bgm 갯수 만큼 비교
        for (int i = 0; i < sfx.Length; i++)
        {
            //조건문을 비교를 해 주는데 드로우파라미터?의 이름이 해당 bgm안에 있는 배열 index와 일치 하는 지 확인하는 함수 /22.03.23 by승주
            if (p_sfxName == sfx[i].name)
            {
                //일치하는 index(이름)을 찾으면 바로 재생하는 것이 아니라 재생중이지 않는 Player을 찾아줘야 한다 /22.03.23 by승주
                //그래서 AudioSource Player 갯수만큼 반복 /22.03.23 by승주
                for (int x = 0; x < sfxPlayer.Length; x++)
                {
                    //x번째 index에 재생중이지 않는 지!
                    if (!sfxPlayer[x].isPlaying)
                    {
                        //x번째 clip을 sfx[i] clip으로 대체.
                        sfxPlayer[x].clip = sfx[i].clip;
                        sfxPlayer[x].Play();
                        return;
                    }
                }
                Debug.Log("모든 오디오 플레이어가 재생중입니다.");
                return;


            }
        }
        Debug.Log(p_sfxName + "이름의 효과음이 없습니다.");
    }
}
