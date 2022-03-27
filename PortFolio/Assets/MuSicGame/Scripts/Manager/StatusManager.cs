using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StatusManager : MonoBehaviour
{
    [SerializeField] float blickSpeed = 0.1f;
    [SerializeField] int blinkCount = 10;

    bool isDead = false;

    //HP 22.03.26 by����
    int maxHp = 3;
    int currentHP = 3;

    //Shield 22.03.26 by����
    int maxShield = 3;
    int currentShield = 0;

    [SerializeField] GameObject[] hpObject = null;
    [SerializeField] GameObject[] shield = null;

    Result theResult;
    NoteManager theNote;

    private void Start()
    {
        theResult = FindObjectOfType<Result>();
        theNote = FindObjectOfType<NoteManager>();
    }

    //�������� ����� �� ȣ���� ��� /22.03.27 by����
    public void DecreaseHP(int p_num)
    {
        currentHP -= p_num;

        //currentHP�� 0���� ���ų� ������ 22.03.27 by����
        if (currentHP <= 0)
        {
           
            isDead = true;
            theResult.ShowResult();
            theNote.RemoveNote();

        }
        SettingHPObject();
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

    //�׾��� �� �� �׾��� �� Ȯ���ϴ� ��� 22.03.27 by����
    public bool Isdead()
    {
        return isDead;
    }
}
