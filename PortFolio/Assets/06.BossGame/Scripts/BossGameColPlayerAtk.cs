using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossGameColPlayerAtk : MonoBehaviour
{
    //player�� comboStep�� Ȯ�� �ϱ� ���� ��� 22.04.26 ����
    public BossGameCombo combo;

    //Collider ���� Ÿ�� ��� 22.04.26 ����
    public string type_Atk;

    //combo�ܰ� ��� 22.04.26 ����
    int comboStep;

    public string dmg;
    public TextMeshProUGUI dmgtext;

    public BossGameHitStop hitStop;

    //Collider�� Ȱ��ȭ �� �� comboStep�� �������� ��� 22.04.26 ����
    private void OnEnable()
    {
        comboStep = combo.comboStep;
    }

    //collider�� �ٸ� trigger collider�� �浹 ���� �� �۵��ϴ� ��� 22.04.26 ����
    private void OnTriggerEnter(Collider other)
    {
        //�浹�� collider�� BossGame_HitBox_Enemy��� 
        if (other.tag == "BossGame_HitBox_Enemy")
        {
            //damage�� collider ���� type�� comboStep�� �ִ� ��� 22.04.26 ����
            dmg = string.Format("{0}+{1}", type_Atk, comboStep);
            dmgtext.text = dmg;
            dmgtext.gameObject.SetActive(true);

            hitStop.StopTime();

           
        }
    }
}
