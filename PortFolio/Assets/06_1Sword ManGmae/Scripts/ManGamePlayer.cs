using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManGamePlayer : MonoBehaviour
{
    public Animator playerAni;
    public Rigidbody playerRigid;
    public float moveSpeed;
    public float dashSpeed;
    public float hp;
    public float curHp;
    public float atk;

    //combo �Է��� ���� �ϰ� �ϴ� ��� 22.04.29 ����
    bool comboInput;

    //combo �ܰ� ��� 22.04.29 ����
    int comboStep;

    //�׼� ���ϴ� ���� �������� ���ϰ� �ϴ� ��� 22.04.29 ����
    public bool enableMove;

    private void Start()
    {
        EnableMove();
    }


    //���� ��� 22.04.29 ����
    public void Attack()
    {
        //comboStep�� 0�� �� ������ ������ 
        if (comboStep == 0)
        {
            //ù��° ���� �ִϸ��̼� ��� ��� 22.04.29 ����
            playerAni.Play("Player_AtkA");
            comboStep = 1;
            return;
        }

        //comboStep�� 0�� �ƴ� �� ������ ������
        if (comboStep != 0)
        {
            //combo ������ ������ ����
            if (comboInput)
            {
                //combo�Է��� �߰��� �ȵǰ� �ϴ� ��� 22.04.29 ����
                comboInput = false;
                comboStep += 1;
            }
        }
    }

    //combo�Է��� true�� ����� �ִ� ��� 22.04.29 ����
    void ComboInput()
    {
        comboInput = true;
    }
    
    //combo �ִϸ��̼��� ��� ��ų ��� 22.04.29 ����
    void ComboPlay()
    {
        if (comboStep == 2) playerAni.Play("Player_AtkB");
        if (comboStep == 3) playerAni.Play("Player_AtkC");
                          
    }

    //������ �߰��� ������ �ʾ��� �� reset ��ų ��� 22.04.29 ����
    void ComboReset()
    {
        comboInput = false;
        comboStep = 0;
    }

    void EnableMove()
    {
        enableMove = true;
    }
    void DisableMove()
    {
        enableMove = false;
    }
}
