using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ManGameHitManager : MonoBehaviour
{
    public static ManGameHitManager instance;

    public TextMeshProUGUI[] dmgText;
    public GameObject[] deathEffect;
    public Camera cam;

    public int score;
    public TextMeshProUGUI scoreText;

    Vector3 upPosition = new Vector3(0, 2, 0);

    bool hitStop;

    private void Awake()
    {
        //�̱��� ���� ��� 22.04.29 ����
        if (instance == null) instance = this;
    }

    //score�� �ö󰡴� ��� 22.04.30 ����
    public void ScoreUp(int point)
    {

        score += point;

        //������ score��ŭ �ö� ������ ������ �ٲ��ִ� ��� 22.04.30 ����
        scoreText.text = string.Format("Score: {0}", score);
    }

    public void DeathEffectOn(Vector3 enemyPosition)
    {
        for (int i = 0; i < deathEffect.Length; i++)
        {
            //���� �������� �ʴ� deathEffect�� ���ִ� ��� 22.04.30 ����
            if (!deathEffect[i].activeSelf)
            {
                deathEffect[i].transform.position = enemyPosition
                    + upPosition;
                deathEffect[i].SetActive(true);
                return;
            }
        }
    }


    //damageText���� ���� ��� 22.04.29 ����
    public void DmgTextOn(Vector3 hitPosition, int dmg)
    {
        StartCoroutine("HitStop");

        for (int i = 0; i < dmgText.Length; i++)
        {
            //i��° damageText�� �������� �ʴٸ� �̹� ���� �ִ� text�� pass�ϰ� �� ���� �� �����ִ� damgeText�� �ѱ� ���� ��� 22.04.29 ����
            if (!dmgText[i].gameObject.activeSelf)
            {
                //�� ���� �ִ� Text�� ��ġ�� hitPosition �̴� ��� 22.04.29 ����
                //Camera.WorldToScreenPotin(A) => ���� ��ǥ�� A�� ȭ�� ��ǥ�� ����
                dmgText[i].transform.position = cam.WorldToScreenPoint(hitPosition + upPosition);

                //�� �����ִ� damageText�� Text�� ������ �������� ������ Damage�� ��� ��� 22.04.29 ����
                dmgText[i].text = dmg.ToString();
                dmgText[i].gameObject.SetActive(true);

                //���̳����� ȿ���� ���ؼ� Damage�� ���� fontSize ��ȭ ��Ű�� ��� 22.04.29 ����
                dmgText[i].fontSize = dmg * 0.5f;

                //text�ϳ��� ������ �ٸ� text�� ������ �ʰ� �ϴ� ��� 22.04.29
                return;


            }
        }
    }

    //Hit�� ��� ���ߴ� ��� 22.04.30 ����
    IEnumerator HitStop()
    {
        //�ߺ����� ���� ��� 22.04.30 ����
        if (!hitStop)
        {
            StartCoroutine("CamEffect");

            hitStop = true;

            //������  �帣�� �ӵ��� ����ٰ�
            Time.timeScale = 0;

            //0.5�Ŀ�
            yield return new WaitForSecondsRealtime(0.3f);

            //������ �帣�� �ӵ��� �ٽ� ���� ��Ű�� ��� 22.04.30 ����
            Time.timeScale = 1;

            hitStop = false;

        }

    }

    //camara �ణ ��鸮�� �׼� ��� 22.04.30 ����
    IEnumerator CamEffect()
    {

        // camara �ι� ��鸮�� �ϴ� ��� 22.04.30 ����
        cam.transform.localPosition = new Vector3(
        cam.transform.localPosition.x + Random.Range(-0.2f, 0.2f),
        cam.transform.localPosition.y + Random.Range(-0.2f, 0.2f),
        cam.transform.localPosition.z + Random.Range(-0.2f, 0.2f)
        );
        yield return new WaitForSecondsRealtime(0.05f);

        cam.transform.localPosition = new Vector3(
        cam.transform.localPosition.x + Random.Range(-0.2f, 0.2f),
        cam.transform.localPosition.y + Random.Range(-0.2f, 0.2f),
        cam.transform.localPosition.z + Random.Range(-0.2f, 0.2f)
        );
        yield return new WaitForSecondsRealtime(0.05f);

        cam.transform.localPosition = Vector3.zero;

    }
}
