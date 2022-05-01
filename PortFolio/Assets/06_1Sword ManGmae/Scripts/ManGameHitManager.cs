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
        //싱글톤 패턴 기능 22.04.29 승주
        if (instance == null) instance = this;
    }

    //score가 올라가는 기능 22.04.30 승주
    public void ScoreUp(int point)
    {

        score += point;

        //점수가 score만큼 올라 갔으니 점수도 바꿔주는 기능 22.04.30 승주
        scoreText.text = string.Format("Score: {0}", score);
    }

    public void DeathEffectOn(Vector3 enemyPosition)
    {
        for (int i = 0; i < deathEffect.Length; i++)
        {
            //현재 켜져있지 않는 deathEffect를 켜주는 기능 22.04.30 승주
            if (!deathEffect[i].activeSelf)
            {
                deathEffect[i].transform.position = enemyPosition
                    + upPosition;
                deathEffect[i].SetActive(true);
                return;
            }
        }
    }


    //damageText들을 띄우는 기능 22.04.29 승주
    public void DmgTextOn(Vector3 hitPosition, int dmg)
    {
        StartCoroutine("HitStop");

        for (int i = 0; i < dmgText.Length; i++)
        {
            //i번째 damageText가 켜져있지 않다면 이미 켜져 있는 text는 pass하고 그 다음 안 켜져있는 damgeText를 켜기 위한 기능 22.04.29 승주
            if (!dmgText[i].gameObject.activeSelf)
            {
                //안 켜져 있는 Text의 위치는 hitPosition 이다 기능 22.04.29 승주
                //Camera.WorldToScreenPotin(A) => 월드 좌표르 A를 화면 좌표로 변경
                dmgText[i].transform.position = cam.WorldToScreenPoint(hitPosition + upPosition);

                //안 켜져있던 damageText의 Text의 내용은 얼마인지는 모르지만 Damage다 라는 기능 22.04.29 승주
                dmgText[i].text = dmg.ToString();
                dmgText[i].gameObject.SetActive(true);

                //다이나믹한 효과를 위해서 Damage에 따라서 fontSize 변화 시키는 기능 22.04.29 승주
                dmgText[i].fontSize = dmg * 0.5f;

                //text하나가 켜지면 다른 text가 켜지지 않게 하는 기능 22.04.29
                return;


            }
        }
    }

    //Hit시 잠시 멈추는 기능 22.04.30 승주
    IEnumerator HitStop()
    {
        //중복실행 방지 기능 22.04.30 승주
        if (!hitStop)
        {
            StartCoroutine("CamEffect");

            hitStop = true;

            //게임이  흐르는 속도를 멈췄다가
            Time.timeScale = 0;

            //0.5후에
            yield return new WaitForSecondsRealtime(0.3f);

            //게임이 흐르는 속도를 다시 실행 시키는 기능 22.04.30 승주
            Time.timeScale = 1;

            hitStop = false;

        }

    }

    //camara 약간 흔들리는 액션 기능 22.04.30 승주
    IEnumerator CamEffect()
    {

        // camara 두번 흔들리게 하는 기능 22.04.30 승주
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
