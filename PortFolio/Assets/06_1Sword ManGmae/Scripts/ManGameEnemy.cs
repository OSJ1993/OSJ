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
        if (other.gameObject.tag == "SwordManPlayerAtk")
        {
            //HitEffect 기능 22.04.29 승주

            //hitPosition는 공격한 Object의 위치와 맞은 Object의 위치를 + 하고 0.5f *에서 반으로 나눈 player와 enemy의 위치가 되는 기능 22.04.29 승주
            Vector3 hitPosition = (other.transform.position + transform.position) * 0.5f;

            //Damage는 공격한 부모Object의 player의 공격력을 가져와서 Damage로 만든 기능 22.04.29 승주
            //Damge를 Random하게 주기 위한 기능 22.04.29 승주
            int dmg = (int)(other.GetComponentInParent<ManGamePlayer>().atk * Random.Range(0.8f, 2f));

            ManGameHitManager.instance.DmgTextOn(hitPosition, dmg);

            //Damage만큼 HP가 깎이게 하는 기능 22.04.29 승주
            curHp -= dmg;
        }
    }
}
