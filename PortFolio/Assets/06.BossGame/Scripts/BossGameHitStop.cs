using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGameHitStop : MonoBehaviour
{
    //scrpts �ߺ����� ���� ��� 22.04.26 ����
    bool stop;

    //Ÿ�ݽ� ���� ���� �ð� ��� 22.04.26 ����
    public float stopTime;

    //camera�� ���� ���� ��� ������ ��ǥ ��� 22.04.26 ����
    public Transform shakeCam;
    public Vector3 shake;


    public void StopTime()
    {
        //stop�� false�� ���� �۵� ��Ű�� ��� 22.04.26 ����
        if (!stop)
        {
            stop = true;

            //camera�� ��� ��ǥ�� �����̰� timeScale�� 0���� �ٲٴ� ��� 22.04.26 ����
            shakeCam.localPosition = shake;
            Time.timeScale = 0;

            StartCoroutine("ReturnTimeScale");
        }
    }

    IEnumerator ReturnTimeScale()
    {
        //stopTime���� �Ŀ� 
        yield return new WaitForSecondsRealtime(stopTime);

        //timeScale�� �ٽ� 1�� �ǰ� 
        Time.timeScale = 1;

        //camera ��ǥ�� �ٽ� ���ƿ��� �ϴ� ��� 22.04.26 ����
        shakeCam.localPosition = Vector3.zero;
        stop = false; 
    }
}
