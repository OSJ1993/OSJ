using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFlame : MonoBehaviour
{
    
    bool musicStart = false;

    public string bgmName = "";

    public void ResetMusic()
    {
        musicStart = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //musicStart�� false�� ���� ����ǰ� �ϱ�.
        if (!musicStart)
        {

            //�������� �ڱ⿡�� ���� �ݶ��̴��� �±װ� Note��� ����� ���.
            if (collision.CompareTag("Note"))
            {
                AudioManager.instance.PlayBGM(bgmName);
                musicStart = true;
            }

        }

    }
}