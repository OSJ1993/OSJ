using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg;

    //스스로 회전하는 총알 기능 22.04.15 by승주
    public bool isRotate;

    void Update()
    {
        if (isRotate) transform.Rotate(Vector3.forward * 0);
    }

    //bullet이 BorderBullet에 닿으면 bullet제거 22.04.07 by승주
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet")
        {
            gameObject.SetActive(false);
        }
    }
}
