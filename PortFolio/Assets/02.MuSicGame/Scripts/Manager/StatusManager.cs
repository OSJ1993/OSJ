using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StatusManager : MonoBehaviour
{
    // ü���� ������ �����Ÿ� /22.03.27 by����
    [SerializeField] float blickSpeed = 0.1f;
    [SerializeField] int blinkCount = 10;
    int currentBlinkCount = 0;
    bool isBlink = false;

    bool isDead = false;

    //HP 22.03.26 by����
    int maxHp = 3;
    int currentHP = 3;

    //Shield 22.03.26 by����
    int maxShield = 3;
    int currentShield = 0;

    [SerializeField] GameObject[] hpObject = null;
    [SerializeField] GameObject[] shieldObject = null;





    // �� Combo �̻��� �� Shield�� �ϳ� ȸ�� �� �� ���ϴ� ��� 22.03.28 by����
    [SerializeField] int shieldlncreaseCombo = 5;
    int currentShieldCombo = 0;



    //Gauge ���� 22.03.28 by����
    [SerializeField] Image shieldGauge = null;

    Result theResult;
    NoteManager theNote;

    //�������� ������ ť���� MeshRenderer ���ٰ� ����� �ݺ��ϴ� ��� 22.03.28 by����
    [SerializeField] MeshRenderer playrMesh = null;

    private void Start()
    {
        theResult = FindObjectOfType<Result>();
        theNote = FindObjectOfType<NoteManager>();
    }

    // HP, Shield, shieldGauge �ʱ�ȭ ��Ű�� ��� 22.03.29 by����
    // isDead �ʱⰪ���� �ǵ���
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

    //miss �߸� Shield Combo �ʱ�ȭ 22.03.28 by����
    public void ResetShieldCombo()
    {
        currentShieldCombo = 0;
        shieldGauge.fillAmount = (float)currentShieldCombo / shieldlncreaseCombo;
    }

    //Shield ���� 22.03.28 by����
    public void IncreaseShield()
    {
        currentShield++;

        //�ִ� ������ ���� �ʰ� �ϴ� ��� 22.03.28 by����
        if (currentShield >= maxShield)
            currentShield = maxShield;

        //Shield������ �� ȣ�� 22.03.28 
        SettingShieldObject();
    }

    //Shield ���� 22.03.28 by����
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


    //�������� ����� �� ȣ���� ��� /22.03.27 by����
    public void DecreaseHP(int p_num)
    {
        if (!isBlink)
        {
            //Shield�� �ִٸ� ü�� ��� Shield�� ��� �ϴ� ���. 22.03.28 by����
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

            //currentHP�� 0���� ���ų� ������ 22.03.27 by����
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

    //ü���� ������ ��Ʈ�� �ϳ��� ������� ���  22.03.27 by����
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

    //�׾��� �� �� �׾��� �� Ȯ���ϴ� ��� 22.03.27 by����
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
