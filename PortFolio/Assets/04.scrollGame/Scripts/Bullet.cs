using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg;

    //bullet¿Ã BorderBulletø° ¥Í¿∏∏È bullet¡¶∞≈ 22.04.07 byΩ¬¡÷
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet")
        {
            gameObject.SetActive(false);
        }
    }
}
