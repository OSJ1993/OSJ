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

        //Enemy speed�� �����ϴ� ��� 22.04.07 by����
        rigid.velocity = Vector2.down * speed;

    }


    //player�� �߻��� bullet�� ������ enemy�� �������� �ް� �ϴ� ��� 22.04.07 by����
    void OnHit(int dmg)
    {
        health -= dmg;

        //�ǰݽ� enemy�� spriteRenderer�� �Ͼ������ �ٲ�� �ϴ� ��� 22.04.07 by����
        //��ҿ��� sprite�� 0 �ǰݽ� sprite�� 1 �����ִ� ��� 22.04.07 by����
        spriteRenderer.sprite = sprites[1];

        //�ٲ� sprite�� �ð� ���� �ְ� �ٽ� �ǵ����� ��� (Invoke) 22.04.07 by����
        Invoke("ReturnSprite", 0.1f);

        //���࿡ health�� 0���� ���ų� �۰� ���� ��� �ı� �Ǵ� ��� 22.04.07 by����
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    //�ǰݽ� enemy�� spritRenderer�� �ٲ���� �Ͼ���� �ٽ� ���� ���·� ���ƿ��� ���ִ� ��� 22.04.07 by����
    void ReturnSprite()
    {
        //��ҿ��� sprite�� 0 �ǰݽ� sprite�� 1 �����ִ� ��� 22.04.07 by����
        spriteRenderer.sprite = sprites[0];
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //bullet�� ���������� �ٱ����� ���� �Ŀ��� ���� ��Ű�� ��� 22.04.07 by����
        if (collision.gameObject.tag == "BorderBullet")
            Destroy(gameObject);

        //player�� bullet�� �ε����� ���� ��Ű�� ��� 22.04.07 by����
        else if (collision.gameObject.tag == "PlayerBullet")
        {

            Bullet bullet = collision.gameObject.GetComponent<Bullet>();

            OnHit(bullet.dmg);

            //bullet�� enemy���� �ǰݽ� bullet�� ������Ű�� ��� 22.04.07 by����
            Destroy(collision.gameObject);

        }

    }
}
