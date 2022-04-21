using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg;

    //������ ȸ���ϴ� �Ѿ� ��� 22.04.15 by����
    public bool isRotate;

    void Update()
    {
        if (isRotate) transform.Rotate(Vector3.forward * 0);
    }

    //bullet�� BorderBullet�� ������ bullet���� 22.04.07 by����
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet")
        {
            gameObject.SetActive(false);
        }
    }
}
