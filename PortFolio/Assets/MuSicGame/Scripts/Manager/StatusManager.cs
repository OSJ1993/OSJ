using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StatusManager : MonoBehaviour
{
    [SerializeField] float blickSpeed = 0.1f;
    [SerializeField] int blinkCount = 10;

    bool isDead = false;

    //HP 22.03.26 by승주
    int maxHp = 3;
    int currentHP = 3;

    //Shield 22.03.26 by승주
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

    //데미지가 닳았을 때 호출할 기능 /22.03.27 by승주
    public void DecreaseHP(int p_num)
    {
        currentHP -= p_num;

        //currentHP가 0보다 같거나 작으면 22.03.27 by승주
        if (currentHP <= 0)
        {
           
            isDead = true;
            theResult.ShowResult();
            theNote.RemoveNote();

        }
        SettingHPObject();
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

    //죽었는 지 안 죽었는 지 확인하는 기능 22.03.27 by승주
    public bool Isdead()
    {
        return isDead;
    }
}
