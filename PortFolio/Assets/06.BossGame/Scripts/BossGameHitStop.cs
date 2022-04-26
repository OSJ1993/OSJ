using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGameHitStop : MonoBehaviour
{
    //scrpts 중복실행 방지 기능 22.04.26 승주
    bool stop;

    //타격시 멈춰 있을 시간 기능 22.04.26 승주
    public float stopTime;

    //camera를 흔들기 위한 축과 움직일 좌표 기능 22.04.26 승주
    public Transform shakeCam;
    public Vector3 shake;


    public void StopTime()
    {
        //stop이 false일 때만 작동 시키는 기능 22.04.26 승주
        if (!stop)
        {
            stop = true;

            //camera를 흔들 좌표로 움직이고 timeScale을 0으로 바꾸는 기능 22.04.26 승주
            shakeCam.localPosition = shake;
            Time.timeScale = 0;

            StartCoroutine("ReturnTimeScale");
        }
    }

    IEnumerator ReturnTimeScale()
    {
        //stopTime실행 후에 
        yield return new WaitForSecondsRealtime(stopTime);

        //timeScale은 다시 1이 되고 
        Time.timeScale = 1;

        //camera 좌표도 다시 돌아오게 하는 기능 22.04.26 승주
        shakeCam.localPosition = Vector3.zero;
        stop = false; 
    }
}
