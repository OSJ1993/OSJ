using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StatusManager : MonoBehaviour
{
    // 체력이 닳으면 깜빡거림 /22.03.27 by승주
    [SerializeField] float blickSpeed = 0.1f;
    [SerializeField] int blinkCount = 10;
    int currentBlinkCount = 0;
    bool isBlink = false;

    bool isDead = false;

    //HP 22.03.26 by승주
    int maxHp = 3;
    int currentHP = 3;

    //Shield 22.03.26 by승주
    int maxShield = 3;
    int currentShield = 0;

    [SerializeField] GameObject[] hpObject = null;
    [SerializeField] GameObject[] shieldObject = null;





    // 몇 Combo 이상일 때 Shield가 하나 회복 될 지 정하는 기능 22.03.28 by승주
    [SerializeField] int shieldlncreaseCombo = 5;
    int currentShieldCombo = 0;



    //Gauge 조절 22.03.28 by승주
    [SerializeField] Image shieldGauge = null;

    Result theResult;
    NoteManager theNote;

    //데미지를 받으면 큐브의 MeshRenderer 껐다가 켜줬다 반복하는 기능 22.03.28 by승주
    [SerializeField] MeshRenderer playrMesh = null;

    private void Start()
    {
        theResult = FindObjectOfType<Result>();
        theNote = FindObjectOfType<NoteManager>();
    }

    // HP, Shield, shieldGauge 초기화 시키는 기능 22.03.29 by승주
    // isDead 초기값으로 되돌림
    public void Initialized()
    {
        currentHP = maxHp;
        currentShield = 0;
        currentShieldCombo = 0;
        shieldGauge.fillAmount = 0;
        isDead = false;
        SettingHPObject();
        SettingShieldObject();

    }

    public void CheckShield()
    {
        currentShieldCombo++;

        if (currentShieldCombo >= shieldlncreaseCombo)
        {
            currentShieldCombo = 0;
            IncreaseShield();
        }

        shieldGauge.fillAmount = (float)currentShieldCombo / shieldlncreaseCombo;
        ;
    }

    //miss 뜨면 Shield Combo 초기화 22.03.28 by승주
    public void ResetShieldCombo()
    {
        currentShieldCombo = 0;
        shieldGauge.fillAmount = (float)currentShieldCombo / shieldlncreaseCombo;
    }

    //Shield 증가 22.03.28 by승주
    public void IncreaseShield()
    {
        currentShield++;

        //최대 갯수를 넘지 않게 하는 기능 22.03.28 by승주
        if (currentShield >= maxShield)
            currentShield = maxShield;

        //Shield증가할 때 호출 22.03.28 
        SettingShieldObject();
    }

    //Shield 감소 22.03.28 by승주
    public void DecreasShield(int p_num)
    {
        currentShield -= p_num;

        SettingShieldObject();

        if (currentShield <= 0)
            currentShield = 0;
    }

    public void IncreaseHP(int p_num)
    {
        currentHP += p_num;
        if (currentHP >= maxHp)
            currentHP = maxHp;

        SettingHPObject();
    }


    //데미지가 닳았을 때 호출할 기능 /22.03.27 by승주
    public void DecreaseHP(int p_num)
    {
        if (!isBlink)
        {
            //Shield가 있다면 체력 대신 Shield가 닳게 하는 기능. 22.03.28 by승주
            if (currentShield > 0)
                DecreasShield(p_num);
            else
            {
                currentHP -= p_num;

                if (currentHP <= 0)
                {
                    theResult.ShowResult();
                    theNote.RemoveNote();
                }
                else
                {
                    StartCoroutine(BlinkCo());
                }

                SettingHPObject();
            }

           /* currentHP -= p_num;

            //currentHP가 0보다 같거나 작으면 22.03.27 by승주
            if (currentHP <= 0)
            {

                isDead = true;
                theResult.ShowResult();
                theNote.RemoveNote();

            }
            else
            {
                StartCoroutine(BlinkCo());
            }

            SettingHPObject();*/


        }
    }

    //체력이 닳으면 하트가 하나씩 닳아지는 기능  22.03.27 by승주
    void SettingHPObject()
    {
        for (int i = 0; i < hpObject.Length; i++)
        {
            if (i < currentHP)
                hpObject[i].gameObject.SetActive(true);
            else
                hpObject[i].gameObject.SetActive(false);
        }
    }

    void SettingShieldObject()
    {
        for (int i = 0; i < shieldObject.Length; i++)
        {
            if (i < currentShield)
                shieldObject[i].gameObject.SetActive(true);
            else
                shieldObject[i].gameObject.SetActive(false);
        }
    }

    //죽었는 지 안 죽었는 지 확인하는 기능 22.03.27 by승주
    public bool Isdead()
    {
        return isDead;
    }

    IEnumerator BlinkCo()
    {
        isBlink = true;

        while (currentBlinkCount <= blinkCount)
        {
            playrMesh.enabled = !playrMesh.enabled;
            yield return new WaitForSeconds(blickSpeed);
            currentBlinkCount++;
        }

        playrMesh.enabled = true;
        currentBlinkCount = 0;
        isBlink = false;
    }
}
