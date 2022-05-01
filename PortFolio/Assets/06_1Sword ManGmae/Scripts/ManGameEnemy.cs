using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManGameEnemy : MonoBehaviour
{


    public Transform target;

    //target과의 거리 기능 22.05.01 승주
    float targetDis;


    public float hp;
    public float curHp;

    public float atk;
    public float curAtk;

    bool enableMove;

    private void OnEnable()
    {
        EnableMove();
        StartCoroutine("EnemyMovement");
    }

    //stage가 넘어갈 수록 Enemy가 강해지는 기능 22.04.29 승주
    public void StatSet(int waveStep)
    {
        curHp = hp * waveStep * 0.5f;
        curHp = atk * waveStep * 0.5f;

    }

    IEnumerator EnemyMovement()
    {
        while (true)
        {
            //Enemy는 계속 target를 바라보게 하는 기능 22.05.01 승주
            transform.LookAt(target);

            //target과의 거리를 계속 계산 하는 기능 22.05.01 승주
            //magnitude  -> Vectror(좌표)를 크리고 변환 시키는 기능 22.05.01 승주

            targetDis = (target.position - transform.position).magnitude;

            //target과의 거리가 3보다 크다면(이상)
            if (targetDis > 3)
            {
                //enableMove가 true일 떄만 실행 시키는 기능 22.05.01승주
                if (enableMove)
                {
                    //속도 4로 앞으로 움직이는 기능 22.05.01 승주
                    //방향 기준은 자기 자신이다.
                    transform.Translate(Vector3.forward * 4 * Time.deltaTime,
                        Space.Self
                        );

                }


            }

            yield return null;
        }
    }

    //Enemy 공격 기능 22.05.01 승주
    void EnemyAttack()
    {

        //target과의 거리가 3보다 작으면(미만)
        if (targetDis <=3)
        {
            
            GetComponent<Animator>().SetTrigger("EnemyAtk");

        }

    }

    //공격하는 동안 못 움직이게 하는 기능 22.05.01 승주 
    void EnableMove()
    {
        enableMove = true;
    }
    void DisableMove()
    {
        enableMove = false;
    }


    //player에게 맞을 때 기능 22.04.29 승주
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SwordManPlayerAtk")
        {
            if (gameObject.name != "Boss")
                GetComponent<Animator>().Play("Enemy_Hit");

            //HitEffect 기능 22.04.29 승주

            //hitPosition는 공격한 Object의 위치와 맞은 Object의 위치를 + 하고 0.5f *에서 반으로 나눈 player와 enemy의 위치가 되는 기능 22.04.29 승주
            Vector3 hitPosition = (other.transform.position + transform.position) * 0.5f;

            //Damage는 공격한 부모Object의 player의 공격력을 가져와서 Damage로 만든 기능 22.04.29 승주
            //Damge를 Random하게 주기 위한 기능 22.04.29 승주
            int dmg = (int)(other.GetComponentInParent<ManGamePlayer>().atk * Random.Range(0.8f, 2f));

            ManGameHitManager.instance.DmgTextOn(hitPosition, dmg);
            ManGameHitManager.instance.ScoreUp(100);

            //Damage만큼 HP가 깎이게 하는 기능 22.04.29 승주
            curHp -= dmg;

            //curHp가 zreo가 되서 enemy가 사라지게 하는 기능 22.04.30 승주
            if (curHp <= 0)
            {
                //죽었을 떄 실 DeathEffectOn 실행 시키는 기능 22.05.01 승주
                ManGameHitManager.instance.DeathEffectOn(transform.position);

                gameObject.SetActive(false);

                if (gameObject.name == "Boss")
                {
                    ManSpawnManager.instance.WaveStart();
                }
            }
        }
    }
}
