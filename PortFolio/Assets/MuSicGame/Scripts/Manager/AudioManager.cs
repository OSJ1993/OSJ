using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ν���Ʈ â���� ���� �����ϰ� [System.Serializable] /22.03.23 by����
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


    //����� ��� /22.03.23 by����
    public void PlayBGM(string p_bgmName)
    {
        //Sound class�� �ִ� string name �� PlayBGM(string p_bgmName)�� ���� ��ġ�ϴ� �� Ȯ�� �ϱ� ���� for�� ��� /22.03.23 by����

        //int i=0���� bgm ���� ��ŭ ��
        for (int i = 0; i < bgm.Length; i++)
        {
            //���ǹ��� �񱳸� �� �ִµ� ��ο��Ķ����?�� �̸��� �ش� bgm�ȿ� �ִ� �迭 index�� ��ġ �ϴ� �� Ȯ���ϴ� �Լ� /22.03.23 by����
            if (p_bgmName == bgm[i].name)
            {
                //��ġ �ϴ� index(�̸�)�� �ִٸ� bgmPlayer�� clip�� i���� index�� clip���� ��ü /22.03.23 by����
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();
            }
        }

    }
    //����� ���� /22.03.23 by����
    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    //ȿ���� ��� /22.03.23 by����
    public void PlaySFX(string p_sfxName)
    {
        //Sound class�� �ִ� string name �� PlayBGM(string p_bgmName)�� ���� ��ġ�ϴ� �� Ȯ�� �ϱ� ���� for�� ��� /22.03.23 by����

        //int i=0���� bgm ���� ��ŭ ��
        for (int i = 0; i < sfx.Length; i++)
        {
            //���ǹ��� �񱳸� �� �ִµ� ��ο��Ķ����?�� �̸��� �ش� bgm�ȿ� �ִ� �迭 index�� ��ġ �ϴ� �� Ȯ���ϴ� �Լ� /22.03.23 by����
            if (p_sfxName == sfx[i].name)
            {
                //��ġ�ϴ� index(�̸�)�� ã���� �ٷ� ����ϴ� ���� �ƴ϶� ��������� �ʴ� Player�� ã����� �Ѵ� /22.03.23 by����
                //�׷��� AudioSource Player ������ŭ �ݺ� /22.03.23 by����
                for (int x = 0; x < sfxPlayer.Length; x++)
                {
                    //x��° index�� ��������� �ʴ� ��!
                    if (!sfxPlayer[x].isPlaying)
                    {
                        //x��° clip�� sfx[i] clip���� ��ü.
                        sfxPlayer[x].clip = sfx[i].clip;
                        sfxPlayer[x].Play();
                        return;
                    }
                }
                Debug.Log("��� ����� �÷��̾ ������Դϴ�.");
                return;


            }
        }
        Debug.Log(p_sfxName + "�̸��� ȿ������ �����ϴ�.");
    }
}
