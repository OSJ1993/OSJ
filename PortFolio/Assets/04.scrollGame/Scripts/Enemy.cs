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
            GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);

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
            GameObject bulletR = Instantiate(bulletObjB, transform.position + Vector3.right * 0.3f, transform.rotation);
            GameObject bulletL = Instantiate(bulletObjB, transform.position + Vector3.left * 0.3f, transform.rotation);

            //Rigidbody2D를 가져와 Addforce()로 총알 발사를 시켜주는 기능 22.04.07 by승주
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

            //enemy생성 직 후 player 변수를 넘겨줌으로써 enemy가 Prefab에서 나온 후 enemy가 playr에게 bullet을 쏘기 위한 기능 22.04.07 by승주
            //목표물 방향 =목표물 위치-자신의 위치 22.04.07 by승주
            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

            //벡터가 단위 값(1)로 변환된 변수 22.04.08 by승주
            rigidR.AddForce(dirVecR.normalized * 10, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 10, ForceMode2D.Impulse);


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
        health -= dmg;

        //피격시 enemy의 spriteRenderer가 하얀색으로 바뀌게 하는 기능 22.04.07 by승주
        //평소에는 sprite는 0 피격시 sprite는 1 시켜주는 기능 22.04.07 by승주
        spriteRenderer.sprite = sprites[1];

        //바꾼 sprite를 시간 차를 주고 다시 되돌리는 기능 (Invoke) 22.04.07 by승주
        Invoke("ReturnSprite", 0.1f);

        //만약에 health가 0보다 같거나 작게 됬을 경우 파괴 되는 기능 22.04.07 by승주
        if (health <= 0)
        {

            //enemy가 Destory가 되면 player에게 score를 더해주는 기능 22.04.11 by승주
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;
            
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
