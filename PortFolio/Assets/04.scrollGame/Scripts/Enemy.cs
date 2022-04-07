using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int health;
    public Sprite[] sprites;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        //Enemy speed를 설정하는 기능 22.04.07 by승주
        rigid.velocity = Vector2.down * speed;

    }


    //player가 발사한 bullet을 맞으면 enemy가 데미지를 받게 하는 기능 22.04.07 by승주
    void OnHit(int dmg)
    {
        health -= dmg;

        //피격시 enemy의 spriteRenderer가 하얀색으로 바뀌게 하는 기능 22.04.07 by승주
        //평소에는 sprite는 0 피격시 sprite는 1 시켜주는 기능 22.04.07 by승주
        spriteRenderer.sprite = sprites[1];

        //바꾼 sprite를 시간 차를 주고 다시 되돌리는 기능 (Invoke) 22.04.07 by승주
        Invoke("ReturnSprite", 0.1f);

        //만약에 health가 0보다 같거나 작게 됬을 경우 파괴 되는 기능 22.04.07 by승주
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    //피격시 enemy의 spritRenderer가 바뀌었던 하얀색이 다시 원래 상태로 돌아오게 해주는 기능 22.04.07 by승주
    void ReturnSprite()
    {
        //평소에는 sprite는 0 피격시 sprite는 1 시켜주는 기능 22.04.07 by승주
        spriteRenderer.sprite = sprites[0];
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //bullet과 마찬가지로 바깥으로 나간 후에는 삭제 시키는 기능 22.04.07 by승주
        if (collision.gameObject.tag == "BorderBullet")
            Destroy(gameObject);

        //player의 bullet과 부딪히면 삭제 시키는 기능 22.04.07 by승주
        else if (collision.gameObject.tag == "PlayerBullet")
        {

            Bullet bullet = collision.gameObject.GetComponent<Bullet>();

            OnHit(bullet.dmg);

            //bullet이 enemy에게 피격시 bullet도 삭제시키는 기능 22.04.07 by승주
            Destroy(collision.gameObject);

        }

    }
}
