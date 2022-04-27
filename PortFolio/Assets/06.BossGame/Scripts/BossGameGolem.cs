using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGameGolem : MonoBehaviour
{
    Animator golemAni;

    //golem�� ���� target ��� 22.04.26 ����
    public Transform target;

    public float golemSpeed;

    //golem�� action ����ġ ��� 22.04.26 ����
    bool enableAct;

    //golem�� ���� �ܰ� ��� 22.04.26 ����
    int atkStep;

    void Start()
    {
        golemAni = GetComponent<Animator>();
        enableAct = true;
    }

    //golem�� target�� �ٶ󺸰� �ϴ� ��� 22.04.26 ����
    void RotateGolem()
    {
        Vector3 dir = target.position - transform.position;

        //target������ Slerp�� �̿��Ͽ� �ε巴�� ȸ�� ��Ű�� ��� 22.04.26 ����
        transform.localRotation = Quaternion.Slerp(transform.localRotation,
                                         Quaternion.LookRotation(dir), 5 * Time.deltaTime);
    }

    //golem�� target���� �̵� ��Ű�� ��� 22.04.26 ����
    void MoveGolem()
    {
        //target���� �Ÿ��� 10�̻��� ���� target�� ���ؼ� �̵� �ϴ� ��� 22.04.26 ����
        if ((target.position - transform.position).magnitude >= 10)
        {
            golemAni.SetBool("Walk", true);
            transform.Translate(Vector3.forward * golemSpeed * Time.deltaTime, Space.Self);

        }

        //target���� �Ÿ��� 10������ ���� ���� �ϴ� ��� 22.04.26 ����
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

    //golem�� ���� ��� 22.04.26 ����
    void GolemAtk()
    {
        //target�� �Ÿ��� 10���϶�� ���� �ܰ迡 ���� ���� �����ϴ� ��� 22.04.26 ���� 
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

    //boss�� �ൿ�� ������ ����ġ ��� 22.04.26 ����
    void FreezeGolem()
    {
        enableAct = false;
    }
    void UnFreezeGolem()
    {
        enableAct = true;
    }
}
