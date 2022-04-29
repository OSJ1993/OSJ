using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossGameColPlayerAtk : MonoBehaviour
{
    //player의 comboStep을 확인 하기 위한 기능 22.04.26 승주
    public BossGameCombo combo;

    //Collider 공격 타입 기능 22.04.26 승주
    public string type_Atk;

    //combo단계 기능 22.04.26 승주
    int comboStep;

    public string dmg;
    public TextMeshProUGUI dmgtext;

    public BossGameHitStop hitStop;

    //Collider가 활성화 될 때 comboStep을 가져오는 기능 22.04.26 승주
    private void OnEnable()
    {
        comboStep = combo.comboStep;
    }

    //collider가 다른 trigger collider와 충돌 했을 때 작동하는 기능 22.04.26 승주
    private void OnTriggerEnter(Collider other)
    {
        //충돌한 collider가 BossGame_HitBox_Enemy라면 
        if (other.tag == "BossGame_HitBox_Enemy")
        {
            //damage의 collider 공격 type과 comboStep을 넣는 기능 22.04.26 승주
            dmg = string.Format("{0}+{1}", type_Atk, comboStep);
            dmgtext.text = dmg;
            dmgtext.gameObject.SetActive(true);

            hitStop.StopTime();

           
        }
    }
}
