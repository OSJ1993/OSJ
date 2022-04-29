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
        if (other.gameObject.tag == "SwordManPlayerAtk")
        {
            //HitEffect ��� 22.04.29 ����

            //hitPosition�� ������ Object�� ��ġ�� ���� Object�� ��ġ�� + �ϰ� 0.5f *���� ������ ���� player�� enemy�� ��ġ�� �Ǵ� ��� 22.04.29 ����
            Vector3 hitPosition = (other.transform.position + transform.position) * 0.5f;

            //Damage�� ������ �θ�Object�� player�� ���ݷ��� �����ͼ� Damage�� ���� ��� 22.04.29 ����
            //Damge�� Random�ϰ� �ֱ� ���� ��� 22.04.29 ����
            int dmg = (int)(other.GetComponentInParent<ManGamePlayer>().atk * Random.Range(0.8f, 2f));

            ManGameHitManager.instance.DmgTextOn(hitPosition, dmg);

            //Damage��ŭ HP�� ���̰� �ϴ� ��� 22.04.29 ����
            curHp -= dmg;
        }
    }
}
