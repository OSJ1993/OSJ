using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGameHitStop : MonoBehaviour
{
    //scrpts �ߺ�����������22.04.26 ����
    bool stop;

    //Ÿ�ݽø��������ð����22.04.26 ����
    public float stopTime;

    //camera���������������������ǥ���22.04.26 ����
    public Transform shakeCam;
    public Vector3 shake;


    public float slowTime;
    public Animator playerAni;



    public void StopTime()
    {
        //stop��false�϶����۵���Ű�±��22.04.26 ����
        if (!stop)
        {
            stop = true;

            //camera�������ǥ�ο����̰�timeScale��0���ιٲٴ±��22.04.26 ����
            shakeCam.localPosition = shake;
            Time.timeScale = 0;

            StartCoroutine("ReturnTimeScale");



        }

    }


    IEnumerator ReturnTimeScale()
    {
        //stopTime�����Ŀ�
        yield return new WaitForSecondsRealtime(stopTime);

        //parrying�� ���ο� ��� ��� 22.04.28 ����
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


        //timeScale���ٽ�1�̵ǰ�
        Time.timeScale = 1;

        //camera ��ǥ���ٽõ��ƿ����ϴ±��22.04.26 ����
        shakeCam.localPosition = Vector3.zero;
        stop = false;

    }

    void OnTriggerEnter(Collider other)
    {
        StopTime();
    }

}






