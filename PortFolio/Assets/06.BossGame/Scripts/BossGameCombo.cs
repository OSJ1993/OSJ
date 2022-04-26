using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGameCombo : MonoBehaviour
{

   

    Animator playerAnim;

    //combo 입력 가능 여부 체크 기능 22.04.26 by승주
    bool comboPossible;

    //combo 진행단계 기능 22.04.26 by승주
    public int comboStep;

    //Smash 키 여부 기능 22.04.26 by승주
    bool inputSmash;



    void Start()
    {

        playerAnim = GetComponent<Animator>();




    }

    //combo의 시작점 기능 22.04.26 by승주
    public void ComboPossible()
    {

        comboPossible = true;


    }

    //입력 키와 combo단계에 따라서 다음 동작 재생 기능 22.04.26 by승주
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

    //combo 단계를 다시 처음으로 초기화 시키는 기능 22.04.26 by승주
    public void ResetCombo()
    {
        comboPossible = false;
        inputSmash = false;
        comboStep = 0;
    }

    //NormalAtk 기능 22.04.26 by승주
    void NormalAttack()
    {
        //최초 입력시 첫번째 공격 모션 재생 기능 22.04.26 by승주
        if (comboStep == 0)
        {
            playerAnim.Play("NomalAtk1");

            comboStep = 1;
            return;
        }

        // 그 이후부터는 comboStep을 1씩 증가시키는 기능 22.04.26 by승주
        if (comboStep != 0)
        {
            if (comboPossible)
            {
                //무차별 공격 방지 기능 22.04.26 by승주
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
