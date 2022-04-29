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

    //stage가 넘어갈 수록 Enemy가 강해지는 기능 22.04.29 승주
    public void StatSet(int waveStep)
    {
        curHp = hp * waveStep * 0.5f;
        curHp = atk * waveStep * 0.5f;

    }

    //player에게 맞을 때 기능 22.04.29 승주
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag== "SwordManPlayerAtk")
        {

        }
    }
}
