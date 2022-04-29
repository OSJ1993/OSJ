using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGameHitStop : MonoBehaviour
{
    //scrpts 중복실행방지기능22.04.26 승주
    bool stop;

    //타격시멈춰있을시간기능22.04.26 승주
    public float stopTime;

    //camera를흔들기위한축과움직일좌표기능22.04.26 승주
    public Transform shakeCam;
    public Vector3 shake;


    public float slowTime;
    public Animator playerAni;



    public void StopTime()
    {
        //stop이false일때만작동시키는기능22.04.26 승주
        if (!stop)
        {
            stop = true;

            //camera를흔들좌표로움직이고timeScale을0으로바꾸는기능22.04.26 승주
            shakeCam.localPosition = shake;
            Time.timeScale = 0;

            StartCoroutine("ReturnTimeScale");



        }

    }


    IEnumerator ReturnTimeScale()
    {
        //stopTime실행후에
        yield return new WaitForSecondsRealtime(stopTime);

        //parrying시 슬로우 모션 기능 22.04.28 승주
        if (!playerAni.GetCurrentAnimatorStateInfo(0).IsName("Idle") &&
            !playerAni.GetCurrentAnimatorStateInfo(0).IsName("Knight_Running") &&
            !playerAni.GetCurrentAnimatorStateInfo(0).IsName("NomalAtk1") &&
            !playerAni.GetCurrentAnimatorStateInfo(0).IsName("NomalAtk2") &&
            !playerAni.GetCurrentAnimatorStateInfo(0).IsName("NomalAtk3") &&
            !playerAni.GetCurrentAnimatorStateInfo(0).IsName("SmashAtk1") &&
            !playerAni.GetCurrentAnimatorStateInfo(0).IsName("SmashAtk2") &&
            !playerAni.GetCurrentAnimatorStateInfo(0).IsName("SmashAtk3") &&
            !playerAni.GetCurrentAnimatorStateInfo(0).IsName("Rigidity") &&
            !playerAni.GetCurrentAnimatorStateInfo(0).IsName("Defense"))


        {
            Time.timeScale = 0.01f;


            yield return new WaitForSecondsRealtime(slowTime);

        }


        //timeScale은다시1이되고
        Time.timeScale = 1;

        //camera 좌표도다시돌아오게하는기능22.04.26 승주
        shakeCam.localPosition = Vector3.zero;
        stop = false;

    }

    void OnTriggerEnter(Collider other)
    {
        StopTime();
    }

}






