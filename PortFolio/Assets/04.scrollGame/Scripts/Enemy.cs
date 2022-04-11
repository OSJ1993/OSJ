using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int enemyScore;
    public float speed;
    public int health;
    public Sprite[] sprites;

    //bullet�� �߻� �Ǵ� �ӵ� �����̸� ���� ��� max(�ִ� ���� ������), cur(���� �ѹ� �߻��� �� �����Ǵ� ������) 22.04.07 by����
    public float maxShotDealy;
    public float curShotDelay;

    //bullet Prefab�� ������ �� �ִ� ��� 22.04.07 by����
    public GameObject bulletObjA;
    public GameObject bulletObjB;

    public GameObject player;

    SpriteRenderer spriteRenderer;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();


    }

    void Update()
    {

        Fire();
        Reload();
    }

    //Bullet �߻� ��� 22.04.07 by����
    void Fire()
    {
        
        
        //curShotDelay(����) Shot �����̰� maxShotDelay�� ���� �ʾҴٸ� ������ �ȵ� �� �� �� �ְ� ���ִ� ��� 22.04.07 by����
        if (curShotDelay < maxShotDealy)
            return;

        if (enemyName == "S")
        {
            //Instantiate() �Ű����� ������Ʈ�� �����ϴ� �Լ� 22.04.07 by����
            //bullet�� ��ġ�� ���� ���ִ� ��� 22.04.07 by����
            GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);

            //Rigidbody2D�� ������ Addforce()�� �Ѿ� �߻縦 �����ִ� ��� 22.04.07 by����
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            //enemy���� �� �� player ������ �Ѱ������ν� enemy�� Prefab���� ���� �� enemy�� playr���� bullet�� ��� ���� ��� 22.04.07 by����
            //��ǥ�� ���� =��ǥ�� ��ġ-�ڽ��� ��ġ 22.04.07 by����
            Vector3 dirVec = player.transform.position - transform.position;

            rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);

            
        }

        else if (enemyName == "L")
        {
            //Instantiate() �Ű����� ������Ʈ�� �����ϴ� �Լ� 22.04.07 by����
            //bullet�� ��ġ�� ���� ���ִ� ��� 22.04.07 by����
            GameObject bulletR = Instantiate(bulletObjB, transform.position + Vector3.right * 0.3f, transform.rotation);
            GameObject bulletL = Instantiate(bulletObjB, transform.position + Vector3.left * 0.3f, transform.rotation);

            //Rigidbody2D�� ������ Addforce()�� �Ѿ� �߻縦 �����ִ� ��� 22.04.07 by����
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

            //enemy���� �� �� player ������ �Ѱ������ν� enemy�� Prefab���� ���� �� enemy�� playr���� bullet�� ��� ���� ��� 22.04.07 by����
            //��ǥ�� ���� =��ǥ�� ��ġ-�ڽ��� ��ġ 22.04.07 by����
            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

            //���Ͱ� ���� ��(1)�� ��ȯ�� ���� 22.04.08 by����
            rigidR.AddForce(dirVecR.normalized * 10, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 10, ForceMode2D.Impulse);


        }

        //bullet�� �� �߻� ���� ��� �ٽ� �������� ���� ������ ������ 0���� �ʱ�ȭ ��Ű�� ��� 22.04.07 by����
        curShotDelay = 0;

    }





    //Bullet �߻� �� ���� �ϴ� ��� 22.04.07 by����
    void Reload()
    {

        curShotDelay += Time.deltaTime;
    }






    //player�� �߻��� bullet�� ������ enemy�� �������� �ް� �ϴ� ��� 22.04.07 by����
    public void OnHit(int dmg)
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

            //enemy�� Destory�� �Ǹ� player���� score�� �����ִ� ��� 22.04.11 by����
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;
            
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
