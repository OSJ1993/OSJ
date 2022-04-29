using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ManGameHitManager : MonoBehaviour
{
    public static ManGameHitManager instance;

    public TextMeshProUGUI[] dmgText;
    public Camera cam;

    Vector3 upPosition = new Vector3(0, 2, 0);

    private void Awake()
    {
        //싱글톤 패턴 기능 22.04.29 승주
        if (instance == null) instance = this;
    }

    //damageText들을 띄우는 기능 22.04.29 승주
    public void DmgTextOn(Vector3 hitPosition,int dmg)
    {
        for (int i = 0; i < dmgText.Length; i++)
        {
            //i번째 damageText가 켜져있지 않다면 이미 켜져 있는 text는 pass하고 그 다음 안 켜져있는 damgeText를 켜기 위한 기능 22.04.29 승주
            if (!dmgText[i].gameObject.activeSelf)
            {
                //안 켜져 있는 Text의 위치는 hitPosition 이다 기능 22.04.29 승주
                //Camera.WorldToScreenPotin(A) => 월드 좌표르 A를 화면 좌표로 변경
                dmgText[i].transform.position = cam.WorldToScreenPoint(hitPosition+ upPosition);

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
}
