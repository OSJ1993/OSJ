using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGameCombo : MonoBehaviour
{

   

    Animator playerAnim;

    //combo �Է� ���� ���� üũ ��� 22.04.26 by����
    bool comboPossible;

    //combo ����ܰ� ��� 22.04.26 by����
    public int comboStep;

    //Smash Ű ���� ��� 22.04.26 by����
    bool inputSmash;



    void Start()
    {

        playerAnim = GetComponent<Animator>();




    }

    //combo�� ������ ��� 22.04.26 by����
    public void ComboPossible()
    {

        comboPossible = true;


    }

    //�Է� Ű�� combo�ܰ迡 ���� ���� ���� ��� ��� 22.04.26 by����
    public void NextAtk()
    {
        if (!inputSmash)
        {
            if (comboStep == 2) playerAnim.Play("NomalAtk2");
            if (comboStep == 3) playerAnim.Play("NomalAtk3");
        }
        if (inputSmash)
        {
            if (comboStep == 1) playerAnim.Play("SmashAtk1");
            if (comboStep == 2) playerAnim.Play("SmashAtk2");
            if (comboStep == 3) playerAnim.Play("SmashAtk3");
        }

    }

    //combo �ܰ踦 �ٽ� ó������ �ʱ�ȭ ��Ű�� ��� 22.04.26 by����
    public void ResetCombo()
    {
        comboPossible = false;
        inputSmash = false;
        comboStep = 0;
    }

    //NormalAtk ��� 22.04.26 by����
    void NormalAttack()
    {
        //���� �Է½� ù��° ���� ��� ��� ��� 22.04.26 by����
        if (comboStep == 0)
        {
            playerAnim.Play("NomalAtk1");

            comboStep = 1;
            return;
        }

        // �� ���ĺ��ʹ� comboStep�� 1�� ������Ű�� ��� 22.04.26 by����
        if (comboStep != 0)
        {
            if (comboPossible)
            {
                //������ ���� ���� ��� 22.04.26 by����
                comboPossible = false;
                comboStep += 1;
                
            }
        }
    }

    void SmashAttack()
    {
        if (comboPossible)
        {
            comboPossible = false;
            inputSmash = true;
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) NormalAttack();


        if (Input.GetMouseButtonDown(1)) SmashAttack();
    }

    
}
