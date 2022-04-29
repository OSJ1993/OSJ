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

    //combo 입력이 가능 하게 하는 기능 22.04.29 승주
    bool comboInput;

    //combo 단계 기능 22.04.29 승주
    int comboStep;

    //액션 취하는 동안 움직이지 못하게 하는 기능 22.04.29 승주
    public bool enableMove;

    private void Start()
    {
        EnableMove();
    }


    //공격 기능 22.04.29 승주
    public void Attack()
    {
        //comboStep이 0일 때 공격을 누르면 
        if (comboStep == 0)
        {
            //첫번째 공격 애니메이션 재생 기능 22.04.29 승주
            playerAni.Play("Player_AtkA");
            comboStep = 1;
            return;
        }

        //comboStep이 0이 아닐 때 공격이 눌리면
        if (comboStep != 0)
        {
            //combo 가능한 상태일 때만
            if (comboInput)
            {
                //combo입력이 추가로 안되게 하는 기능 22.04.29 승주
                comboInput = false;
                comboStep += 1;
            }
        }
    }

    //combo입력을 true로 만들어 주는 기능 22.04.29 승주
    void ComboInput()
    {
        comboInput = true;
    }
    
    //combo 애니메이션을 재생 시킬 기능 22.04.29 승주
    void ComboPlay()
    {
        if (comboStep == 2) playerAni.Play("Player_AtkB");
        if (comboStep == 3) playerAni.Play("Player_AtkC");
                          
    }

    //공격을 추가로 누르지 않았을 때 reset 시킬 기능 22.04.29 승주
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
