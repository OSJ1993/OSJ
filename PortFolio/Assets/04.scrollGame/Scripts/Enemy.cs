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

    //bullet이 발사 되는 속도 딜레이를 위한 기능 max(최대 실제 딜레이), cur(현재 한발 발사한 후 충전되는 딜레이) 22.04.07 by승주
    public float maxShotDealy;
    public float curShotDelay;

    //bullet Prefab을 저장할 수 있는 기능 22.04.07 by승주
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

    //pattern 흐름에 필요한 변수 생성 22.04.15 by승주
    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (enemyName == "B")
            anim = GetComponent<Animator>();

    }

    //소모되는 변수는 활성화 될 때, 다시 초기화 시켜주는 기능 22.04.13 by승주
    //OnEnable() 컴포넌트가 활성화 될 때 호출되는 생명주기 함수 기능 22.04.13 by승주
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
        //stop 함수가 두 번 사용되지 않도록 하는 기능 22.04.15by승주
        if (!gameObject.activeSelf)
            return;

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        Invoke("Think", 2);
    }

    //pattern을 돌리기 위한 생각 기능 22.04.15 by승주
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

        //Instantiate() 매개변수 오브젝트를 생성하는 함수 22.04.07 by승주
        //bullet의 위치를 지정 해주는 기능 22.04.07 by승주
        GameObject bulletR = scrollObjectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.3f;
        GameObject bulletRR = scrollObjectManager.MakeObj("BulletBossA");
        bulletRR.transform.position = transform.position + Vector3.right * 0.6f;
        GameObject bulletL = scrollObjectManager.MakeObj("BulletBossA");
        bulletL.transform.position = transform.position + Vector3.left * 0.3f;
        GameObject bulletLL = scrollObjectManager.MakeObj("BulletBossA");
        bulletLL.transform.position = transform.position + Vector3.left * 0.6f;


        //Rigidbody2D를 가져와 Addforce()로 총알 발사를 시켜주는 기능 22.04.07 by승주
        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();


        //벡터가 단위 값(1)로 변환된 변수 22.04.08 by승주
        //Enemy Bullet Speed 설정 기능 22.04.08 by승주
        rigidR.AddForce(Vector2.down * 9, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 9, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 9, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 9, ForceMode2D.Impulse);


        curPatternCount++;

        //curPatternCount이 maxPatternCount[patternIndex]보다 작다면 다시 "FireFoward"를 다시 실행 시키는 기능 22.04.16 by승주
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireFoward", 2);

        //Pattern이 다 채워졌다면 다시 "Think"를 실행 시키는 기능 22.04.16 by승주
        else
            Invoke("Think", 3);


    }

    void FireShot()
    {
        for (int index = 0; index < 5; index++)
        {

            //Instantiate() 매개변수 오브젝트를 생성하는 함수 22.04.07 by승주
            //bullet의 위치를 지정 해주는 기능 22.04.07 by승주
            GameObject bullet = scrollObjectManager.MakeObj("BulletEnemyB");
            bullet.transform.position = transform.position;


            //Rigidbody2D를 가져와 Addforce()로 총알 발사를 시켜주는 기능 22.04.07 by승주
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            //enemy생성 직 후 player 변수를 넘겨줌으로써 enemy가 Prefab에서 나온 후 enemy가 playr에게 bullet을 쏘기 위한 기능 22.04.07 by승주
            //목표물 방향 =목표물 위치-자신의 위치 22.04.07 by승주
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
            dirVec += ranVec;

            rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);


        }
        curPatternCount++;

        //curPatternCount이 maxPatternCount[patternIndex]보다 작다면 다시 "FireShot"를 다시 실행 시키는 기능 22.04.16 by승주
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireShot", 0.15f);

        //Pattern이 다 채워졌다면 다시 "Think"를 실행 시키는 기능 22.04.16 by승주
        else
            Invoke("Think", 3);
    }

    void FireArc()
    {


        //#Fire Arc Continue Fire
        GameObject bullet = scrollObjectManager.MakeObj("BulletEnemyA");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;


        //Rigidbody2D를 가져와 Addforce()로 총알 발사를 시켜주는 기능 22.04.07 by승주
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();


        Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 10 * curPatternCount / maxPatternCount[patternIndex]), -1);

        rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);




        curPatternCount++;

        //curPatternCount이 maxPatternCount[patternIndex]보다 작다면 다시 "FireArc"를 다시 실행 시키는 기능 22.04.16 by승주
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.15f);

        //Pattern이 다 채워졌다면 다시 "Think"를 실행 시키는 기능 22.04.16 by승주
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


            //Rigidbody2D를 가져와 Addforce()로 총알 발사를 시켜주는 기능 22.04.07 by승주
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();


            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum)
                                                   ,(Mathf.Sin(Mathf.PI * 2 * index / roundNum)));

            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * index / roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);


        }




        curPatternCount++;

        //curPatternCount이 maxPatternCount[patternIndex]보다 작다면 다시 "FireAround"를 다시 실행 시키는 기능 22.04.16 by승주
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 2);

        //Pattern이 다 채워졌다면 다시 "Think"를 실행 시키는 기능 22.04.16 by승주
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

    //Bullet 발사 기능 22.04.07 by승주
    void Fire()
    {


        //curShotDelay(현재) Shot 딜레이가 maxShotDelay를 넘지 않았다면 장전이 안된 걸 알 수 있게 해주는 기능 22.04.07 by승주
        if (curShotDelay < maxShotDealy)
            return;

        if (enemyName == "S")
        {
            //Instantiate() 매개변수 오브젝트를 생성하는 함수 22.04.07 by승주
            //bullet의 위치를 지정 해주는 기능 22.04.07 by승주
            GameObject bullet = scrollObjectManager.MakeObj("BulletEnemyA");
            bullet.transform.position = transform.position;


            //Rigidbody2D를 가져와 Addforce()로 총알 발사를 시켜주는 기능 22.04.07 by승주
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            //enemy생성 직 후 player 변수를 넘겨줌으로써 enemy가 Prefab에서 나온 후 enemy가 playr에게 bullet을 쏘기 위한 기능 22.04.07 by승주
            //목표물 방향 =목표물 위치-자신의 위치 22.04.07 by승주
            Vector3 dirVec = player.transform.position - transform.position;

            rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);


        }

        else if (enemyName == "L")
        {
            //Instantiate() 매개변수 오브젝트를 생성하는 함수 22.04.07 by승주
            //bullet의 위치를 지정 해주는 기능 22.04.07 by승주
            GameObject bulletR = scrollObjectManager.MakeObj("BulletEnemyB");
            bulletR.transform.position = transform.position + Vector3.right * 0.3f;

            GameObject bulletL = scrollObjectManager.MakeObj("BulletEnemyB");
            bulletL.transform.position = transform.position + Vector3.left * 0.3f;


            //Rigidbody2D를 가져와 Addforce()로 총알 발사를 시켜주는 기능 22.04.07 by승주
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

            //enemy생성 직 후 player 변수를 넘겨줌으로써 enemy가 Prefab에서 나온 후 enemy가 playr에게 bullet을 쏘기 위한 기능 22.04.07 by승주
            //목표물 방향 =목표물 위치-자신의 위치 22.04.07 by승주
            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

            //벡터가 단위 값(1)로 변환된 변수 22.04.08 by승주
            //Enemy Bullet Speed 설정 기능 22.04.08 by승주
            rigidR.AddForce(dirVecR.normalized * 6, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 6, ForceMode2D.Impulse);


        }

        //bullet을 다 발사 헀을 경우 다시 재장전을 위해 딜레이 변수를 0으로 초기화 시키는 기능 22.04.07 by승주
        curShotDelay = 0;

    }





    //Bullet 발사 후 장전 하는 기능 22.04.07 by승주
    void Reload()
    {

        curShotDelay += Time.deltaTime;
    }






    //player가 발사한 bullet을 맞으면 enemy가 데미지를 받게 하는 기능 22.04.07 by승주
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

            //피격시 enemy의 spriteRenderer가 하얀색으로 바뀌게 하는 기능 22.04.07 by승주
            //평소에는 sprite는 0 피격시 sprite는 1 시켜주는 기능 22.04.07 by승주
            spriteRenderer.sprite = sprites[1];

            //바꾼 sprite를 시간 차를 주고 다시 되돌리는 기능 (Invoke) 22.04.07 by승주
            Invoke("ReturnSprite", 0.1f);

        }


        //만약에 health가 0이하 일 경우 파괴 되는 기능 22.04.07 by승주
        if (health <= 0)
        {


            //enemy가 Destory가 되면 player에게 score를 더해주는 기능 22.04.11 by승주
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;


            //health가 zero일 떄 item을 Drop 할 수 있게(Radom) 하는 기능 22.04.12 by승주
            //Boss가 아이템 떨어트릴 확률 0으로 만드는 기능 22.04.15 by승주
            int ran = enemyName == "B" ? 0 : Random.Range(0, 10);

            //item Drop 활률 기능 22.04.12 by승주
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

            //Boss Kill하면 Stage Clear 기능 22.04.21 by승주
            if (enemyName == "B")
            {
                scrollGameManager.StageEnd();
            }

        }
    }




    //피격시 enemy의 spritRenderer가 바뀌었던 하얀색이 다시 원래 상태로 돌아오게 해주는 기능 22.04.07 by승주
    void ReturnSprite()
    {
        //평소에는 sprite는 0 피격시 sprite는 1 시켜주는 기능 22.04.07 by승주
        spriteRenderer.sprite = sprites[0];
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        //bullet과 마찬가지로 바깥으로 나간 후에는 삭제 시키는 기능 22.04.07 by승주
        if (collision.gameObject.tag == "BorderBullet" && enemyName != "B")
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }

        //player의 bullet과 부딪히면 삭제 시키는 기능 22.04.07 by승주
        else if (collision.gameObject.tag == "PlayerBullet")
        {

            Bullet bullet = collision.gameObject.GetComponent<Bullet>();

            OnHit(bullet.dmg);

            //bullet이 enemy에게 피격시 bullet도 삭제시키는 기능 22.04.07 by승주
            collision.gameObject.SetActive(false);


        }


    }

    public static implicit operator Enemy(CardGameEntity v)
    {
        throw new System.NotImplementedException();
    }
}
