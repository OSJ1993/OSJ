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

    public GameObject itemCoim;
    public GameObject itemPower;
    public GameObject itemBoom;

    public GameObject player;

    public ScrollGameManager scrollGameManager;
    public ScrollGameObjectManager scrollObjectManager;

    SpriteRenderer spriteRenderer;

    Animator anim;

    //pattern �帧�� �ʿ��� ���� ���� 22.04.15 by����
    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (enemyName == "B")
            anim = GetComponent<Animator>();

    }

    //�Ҹ�Ǵ� ������ Ȱ��ȭ �� ��, �ٽ� �ʱ�ȭ �����ִ� ��� 22.04.13 by����
    //OnEnable() ������Ʈ�� Ȱ��ȭ �� �� ȣ��Ǵ� �����ֱ� �Լ� ��� 22.04.13 by����
    void OnEnable()
    {
        switch (enemyName)
        {
            case "B":
                health = 4000;
                Invoke("Stop", 2.5f);
                break;

            case "L":
                health = 40;
                break;

            case "M":
                health = 10;
                break;

            case "S":
                health = 3;
                break;
        }
    }

    void Stop()
    {
        //stop �Լ��� �� �� ������ �ʵ��� �ϴ� ��� 22.04.15by����
        if (!gameObject.activeSelf)
            return;

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        Invoke("Think", 2);
    }

    //pattern�� ������ ���� ���� ��� 22.04.15 by����
    void Think()
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;

        curPatternCount = 0;
        

        switch (patternIndex)
        {
            case 0:
                FireFoward();
                break;

            case 1:
                FireShot();
                break;

            case 2:
                FireArc();
                break;

            case 3:
                FireAround();
                break;
        }
    }

    void FireFoward()
    {

        //Instantiate() �Ű����� ������Ʈ�� �����ϴ� �Լ� 22.04.07 by����
        //bullet�� ��ġ�� ���� ���ִ� ��� 22.04.07 by����
        GameObject bulletR = scrollObjectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.3f;
        GameObject bulletRR = scrollObjectManager.MakeObj("BulletBossA");
        bulletRR.transform.position = transform.position + Vector3.right * 0.6f;
        GameObject bulletL = scrollObjectManager.MakeObj("BulletBossA");
        bulletL.transform.position = transform.position + Vector3.left * 0.3f;
        GameObject bulletLL = scrollObjectManager.MakeObj("BulletBossA");
        bulletLL.transform.position = transform.position + Vector3.left * 0.6f;


        //Rigidbody2D�� ������ Addforce()�� �Ѿ� �߻縦 �����ִ� ��� 22.04.07 by����
        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();


        //���Ͱ� ���� ��(1)�� ��ȯ�� ���� 22.04.08 by����
        //Enemy Bullet Speed ���� ��� 22.04.08 by����
        rigidR.AddForce(Vector2.down * 9, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 9, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 9, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 9, ForceMode2D.Impulse);


        curPatternCount++;

        //curPatternCount�� maxPatternCount[patternIndex]���� �۴ٸ� �ٽ� "FireFoward"�� �ٽ� ���� ��Ű�� ��� 22.04.16 by����
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireFoward", 2);

        //Pattern�� �� ä�����ٸ� �ٽ� "Think"�� ���� ��Ű�� ��� 22.04.16 by����
        else
            Invoke("Think", 3);


    }

    void FireShot()
    {
        for (int index = 0; index < 5; index++)
        {

            //Instantiate() �Ű����� ������Ʈ�� �����ϴ� �Լ� 22.04.07 by����
            //bullet�� ��ġ�� ���� ���ִ� ��� 22.04.07 by����
            GameObject bullet = scrollObjectManager.MakeObj("BulletEnemyB");
            bullet.transform.position = transform.position;


            //Rigidbody2D�� ������ Addforce()�� �Ѿ� �߻縦 �����ִ� ��� 22.04.07 by����
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            //enemy���� �� �� player ������ �Ѱ������ν� enemy�� Prefab���� ���� �� enemy�� playr���� bullet�� ��� ���� ��� 22.04.07 by����
            //��ǥ�� ���� =��ǥ�� ��ġ-�ڽ��� ��ġ 22.04.07 by����
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
            dirVec += ranVec;

            rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);


        }
        curPatternCount++;

        //curPatternCount�� maxPatternCount[patternIndex]���� �۴ٸ� �ٽ� "FireShot"�� �ٽ� ���� ��Ű�� ��� 22.04.16 by����
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireShot", 0.15f);

        //Pattern�� �� ä�����ٸ� �ٽ� "Think"�� ���� ��Ű�� ��� 22.04.16 by����
        else
            Invoke("Think", 3);
    }

    void FireArc()
    {


        //#Fire Arc Continue Fire
        GameObject bullet = scrollObjectManager.MakeObj("BulletEnemyA");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;


        //Rigidbody2D�� ������ Addforce()�� �Ѿ� �߻縦 �����ִ� ��� 22.04.07 by����
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();


        Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 10 * curPatternCount / maxPatternCount[patternIndex]), -1);

        rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);




        curPatternCount++;

        //curPatternCount�� maxPatternCount[patternIndex]���� �۴ٸ� �ٽ� "FireArc"�� �ٽ� ���� ��Ű�� ��� 22.04.16 by����
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.15f);

        //Pattern�� �� ä�����ٸ� �ٽ� "Think"�� ���� ��Ű�� ��� 22.04.16 by����
        else
            Invoke("Think", 3);
    }

    void FireAround()
    {
        int roundNumA = 50;
        int roundNumB = 40;
        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;

        for (int index = 0; index < roundNum; index++)
        {
            GameObject bullet = scrollObjectManager.MakeObj("BulletBossB");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;


            //Rigidbody2D�� ������ Addforce()�� �Ѿ� �߻縦 �����ִ� ��� 22.04.07 by����
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();


            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum)
                                                   ,(Mathf.Sin(Mathf.PI * 2 * index / roundNum)));

            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * index / roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);


        }




        curPatternCount++;

        //curPatternCount�� maxPatternCount[patternIndex]���� �۴ٸ� �ٽ� "FireAround"�� �ٽ� ���� ��Ű�� ��� 22.04.16 by����
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 2);

        //Pattern�� �� ä�����ٸ� �ٽ� "Think"�� ���� ��Ű�� ��� 22.04.16 by����
        else
            Invoke("Think", 3);
    }



    void Update()
    {
        if (enemyName == "B")
            return;

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
            GameObject bullet = scrollObjectManager.MakeObj("BulletEnemyA");
            bullet.transform.position = transform.position;


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
            GameObject bulletR = scrollObjectManager.MakeObj("BulletEnemyB");
            bulletR.transform.position = transform.position + Vector3.right * 0.3f;

            GameObject bulletL = scrollObjectManager.MakeObj("BulletEnemyB");
            bulletL.transform.position = transform.position + Vector3.left * 0.3f;


            //Rigidbody2D�� ������ Addforce()�� �Ѿ� �߻縦 �����ִ� ��� 22.04.07 by����
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

            //enemy���� �� �� player ������ �Ѱ������ν� enemy�� Prefab���� ���� �� enemy�� playr���� bullet�� ��� ���� ��� 22.04.07 by����
            //��ǥ�� ���� =��ǥ�� ��ġ-�ڽ��� ��ġ 22.04.07 by����
            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

            //���Ͱ� ���� ��(1)�� ��ȯ�� ���� 22.04.08 by����
            //Enemy Bullet Speed ���� ��� 22.04.08 by����
            rigidR.AddForce(dirVecR.normalized * 6, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 6, ForceMode2D.Impulse);


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
        if (health <= 0)
            return;

        health -= dmg;

        if (enemyName == "B")
        {
            anim.SetTrigger("OnHit");
        }
        else
        {

            //�ǰݽ� enemy�� spriteRenderer�� �Ͼ������ �ٲ�� �ϴ� ��� 22.04.07 by����
            //��ҿ��� sprite�� 0 �ǰݽ� sprite�� 1 �����ִ� ��� 22.04.07 by����
            spriteRenderer.sprite = sprites[1];

            //�ٲ� sprite�� �ð� ���� �ְ� �ٽ� �ǵ����� ��� (Invoke) 22.04.07 by����
            Invoke("ReturnSprite", 0.1f);

        }


        //���࿡ health�� 0���� �� ��� �ı� �Ǵ� ��� 22.04.07 by����
        if (health <= 0)
        {


            //enemy�� Destory�� �Ǹ� player���� score�� �����ִ� ��� 22.04.11 by����
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;


            //health�� zero�� �� item�� Drop �� �� �ְ�(Radom) �ϴ� ��� 22.04.12 by����
            //Boss�� ������ ����Ʈ�� Ȯ�� 0���� ����� ��� 22.04.15 by����
            int ran = enemyName == "B" ? 0 : Random.Range(0, 10);

            //item Drop Ȱ�� ��� 22.04.12 by����
            if (ran < 3)
            {
                //Not Item 30%
                Debug.Log("Not Item");

            }
            else if (ran < 6)
            {
                //Coin  30%
                GameObject itemCoin = scrollObjectManager.MakeObj("ItemCoin");
                itemCoin.transform.position = transform.position;
                Rigidbody2D rigid = itemCoin.GetComponent<Rigidbody2D>();
                rigid.velocity = Vector2.down * 1.5f;


            }
            else if (ran < 8)
            {
                //Power 20%
                GameObject itemPower = scrollObjectManager.MakeObj("ItemPower");
                itemPower.transform.position = transform.position;
                Rigidbody2D rigid = itemPower.GetComponent<Rigidbody2D>();
                rigid.velocity = Vector2.down * 1.5f;
            }
            else if (ran < 10)
            {
                //Boom 20%
                GameObject itemBoom = scrollObjectManager.MakeObj("ItemBoom");
                itemBoom.transform.position = transform.position;
                Rigidbody2D rigid = itemBoom.GetComponent<Rigidbody2D>();
                rigid.velocity = Vector2.down * 1.5f;
            }


            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;

            scrollGameManager.CallExplosion(transform.position, enemyName);

            //Boss Kill�ϸ� Stage Clear ��� 22.04.21 by����
            if (enemyName == "B")
            {
                scrollGameManager.StageEnd();
            }

        }
    }




    //�ǰݽ� enemy�� spritRenderer�� �ٲ���� �Ͼ���� �ٽ� ���� ���·� ���ƿ��� ���ִ� ��� 22.04.07 by����
    void ReturnSprite()
    {
        //��ҿ��� sprite�� 0 �ǰݽ� sprite�� 1 �����ִ� ��� 22.04.07 by����
        spriteRenderer.sprite = sprites[0];
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        //bullet�� ���������� �ٱ����� ���� �Ŀ��� ���� ��Ű�� ��� 22.04.07 by����
        if (collision.gameObject.tag == "BorderBullet" && enemyName != "B")
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }

        //player�� bullet�� �ε����� ���� ��Ű�� ��� 22.04.07 by����
        else if (collision.gameObject.tag == "PlayerBullet")
        {

            Bullet bullet = collision.gameObject.GetComponent<Bullet>();

            OnHit(bullet.dmg);

            //bullet�� enemy���� �ǰݽ� bullet�� ������Ű�� ��� 22.04.07 by����
            collision.gameObject.SetActive(false);


        }


    }

    public static implicit operator Enemy(CardGameEntity v)
    {
        throw new System.NotImplementedException();
    }
}
