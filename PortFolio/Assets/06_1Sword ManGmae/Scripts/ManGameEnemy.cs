using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManGameEnemy : MonoBehaviour
{
    Animator enemyAni;

    public float hp;
    public float curHp;

    public float atk;
    public float curAtk;

    //stage�� �Ѿ ���� Enemy�� �������� ��� 22.04.29 ����
    public void StatSet(int waveStep)
    {
        curHp = hp * waveStep * 0.5f;
        curHp = atk * waveStep * 0.5f;

    }

    //player���� ���� �� ��� 22.04.29 ����
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag== "SwordManPlayerAtk")
        {

        }
    }
}
