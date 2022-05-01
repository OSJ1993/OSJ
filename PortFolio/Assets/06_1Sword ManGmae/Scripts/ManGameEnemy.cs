using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManGameEnemy : MonoBehaviour
{


    public Transform target;

    //target���� �Ÿ� ��� 22.05.01 ����
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

    //stage�� �Ѿ ���� Enemy�� �������� ��� 22.04.29 ����
    public void StatSet(int waveStep)
    {
        curHp = hp * waveStep * 0.5f;
        curHp = atk * waveStep * 0.5f;

    }

    IEnumerator EnemyMovement()
    {
        while (true)
        {
            //Enemy�� ��� target�� �ٶ󺸰� �ϴ� ��� 22.05.01 ����
            transform.LookAt(target);

            //target���� �Ÿ��� ��� ��� �ϴ� ��� 22.05.01 ����
            //magnitude  -> Vectror(��ǥ)�� ũ���� ��ȯ ��Ű�� ��� 22.05.01 ����

            targetDis = (target.position - transform.position).magnitude;

            //target���� �Ÿ��� 3���� ũ�ٸ�(�̻�)
            if (targetDis > 3)
            {
                //enableMove�� true�� ���� ���� ��Ű�� ��� 22.05.01����
                if (enableMove)
                {
                    //�ӵ� 4�� ������ �����̴� ��� 22.05.01 ����
                    //���� ������ �ڱ� �ڽ��̴�.
                    transform.Translate(Vector3.forward * 4 * Time.deltaTime,
                        Space.Self
                        );

                }


            }

            yield return null;
        }
    }

    //Enemy ���� ��� 22.05.01 ����
    void EnemyAttack()
    {

        //target���� �Ÿ��� 3���� ������(�̸�)
        if (targetDis <=3)
        {
            
            GetComponent<Animator>().SetTrigger("EnemyAtk");

        }

    }

    //�����ϴ� ���� �� �����̰� �ϴ� ��� 22.05.01 ���� 
    void EnableMove()
    {
        enableMove = true;
    }
    void DisableMove()
    {
        enableMove = false;
    }


    //player���� ���� �� ��� 22.04.29 ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SwordManPlayerAtk")
        {
            if (gameObject.name != "Boss")
                GetComponent<Animator>().Play("Enemy_Hit");

            //HitEffect ��� 22.04.29 ����

            //hitPosition�� ������ Object�� ��ġ�� ���� Object�� ��ġ�� + �ϰ� 0.5f *���� ������ ���� player�� enemy�� ��ġ�� �Ǵ� ��� 22.04.29 ����
            Vector3 hitPosition = (other.transform.position + transform.position) * 0.5f;

            //Damage�� ������ �θ�Object�� player�� ���ݷ��� �����ͼ� Damage�� ���� ��� 22.04.29 ����
            //Damge�� Random�ϰ� �ֱ� ���� ��� 22.04.29 ����
            int dmg = (int)(other.GetComponentInParent<ManGamePlayer>().atk * Random.Range(0.8f, 2f));

            ManGameHitManager.instance.DmgTextOn(hitPosition, dmg);
            ManGameHitManager.instance.ScoreUp(100);

            //Damage��ŭ HP�� ���̰� �ϴ� ��� 22.04.29 ����
            curHp -= dmg;

            //curHp�� zreo�� �Ǽ� enemy�� ������� �ϴ� ��� 22.04.30 ����
            if (curHp <= 0)
            {
                //�׾��� �� �� DeathEffectOn ���� ��Ű�� ��� 22.05.01 ����
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
