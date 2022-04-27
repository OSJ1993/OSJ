using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGameGolem : MonoBehaviour
{
    Animator golemAni;

    //golem이 따라갈 target 기능 22.04.26 승주
    public Transform target;

    public float golemSpeed;

    //golem의 action 스위치 기능 22.04.26 승주
    bool enableAct;

    //golem의 공격 단게 기능 22.04.26 승주
    int atkStep;

    void Start()
    {
        golemAni = GetComponent<Animator>();
        enableAct = true;
    }

    //golem이 target을 바라보게 하는 기능 22.04.26 승주
    void RotateGolem()
    {
        Vector3 dir = target.position - transform.position;

        //target방향을 Slerp를 이용하여 부드럽게 회전 시키는 기능 22.04.26 승주
        transform.localRotation = Quaternion.Slerp(transform.localRotation,
                                         Quaternion.LookRotation(dir), 5 * Time.deltaTime);
    }

    //golem을 target에게 이동 시키는 기능 22.04.26 승주
    void MoveGolem()
    {
        //target과의 거리가 10이상일 때는 target을 향해서 이동 하는 기능 22.04.26 승주
        if ((target.position - transform.position).magnitude >= 10)
        {
            golemAni.SetBool("Walk", true);
            transform.Translate(Vector3.forward * golemSpeed * Time.deltaTime, Space.Self);

        }

        //target과의 거리가 10이하일 때는 정지 하는 기능 22.04.26 승주
        if ((target.position - transform.position).magnitude < 10)
        {
            golemAni.SetBool("Walk", false);

        }
    }

    void Update()
    {
        if (enableAct)
        {
            RotateGolem();
            MoveGolem();
        }
    }

    //golem의 공격 기능 22.04.26 승주
    void GolemAtk()
    {
        //target과 거리가 10이하라면 공격 단계에 따라 패턴 공격하는 기능 22.04.26 승주 
        if ((target.position - transform.position).magnitude < 10)
        {

            
            switch (Random.Range(0, 3))
            {
                case 0:
                    atkStep += 1;
                    golemAni.Play("Golem_Atk1");
                    break;

                case 1:
                    atkStep += 1;
                    golemAni.Play("Golem_Atk2");
                    break;

                case 2:
                    atkStep += 0;
                    golemAni.Play("Golem_Atk3");
                    break;
            }


        }
    }

    //boss의 행동을 제어할 스위치 기능 22.04.26 승주
    void FreezeGolem()
    {
        enableAct = false;
    }
    void UnFreezeGolem()
    {
        enableAct = true;
    }
}
