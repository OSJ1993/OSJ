using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFlame : MonoBehaviour
{
    AudioSource myAudio;
    bool musicStart = false;

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //musicStart가 false일 때만 실행되게 하기.
        if (!musicStart)
        {

        //조건으로 자기에게 닿은 콜라이더에 태그가 Note라면 오디오 재생.
        if (collision.CompareTag("Note"))
        {
            myAudio.Play();
            musicStart = true;
        }

        }

    }
}
