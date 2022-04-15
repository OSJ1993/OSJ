using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Object 풀링을 위한 기능 22.04.13 by승주
//쓰레기 메모리 제거 가비지 컬렉트(쌓인 조각난 메모리 비우는 기능) 기능 22.04.13 by승주

public class ScrollGameObjectManager : MonoBehaviour
{

    public GameObject enemyBPrefab;
    public GameObject enemyLPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemySPrefab;

    public GameObject itemCoinPrefab;
    public GameObject itemPowerPrefab;
    public GameObject itemBoomPrefab;

    public GameObject bulletPlayerAPrefab;
    public GameObject bulletPlayerBPrefab;
    public GameObject bulletEnemyAPrefab;
    public GameObject bulletEnemyBPrefab;
    public GameObject bulletFollowerPrefab;
    public GameObject bulletBossAPrefab;
    public GameObject bulletBossBPrefab;

    //Prefab을 생성 하여 저장할 배열 생성 22.04.13 by승주
    GameObject[] enemyB;
    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;

    GameObject[] itemCoin;
    GameObject[] itemPower;
    GameObject[] itemBoom;

    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletEnemyA;
    GameObject[] bulletEnemyB;
    GameObject[] bulletFollower;
    GameObject[] bulletBossA;
    GameObject[] bulletBossB;

    GameObject[] targetPool;

    void Awake()
    {
        //한번에 등장할 개수를 고려하여 배열 길이 할당 22.04.13 by승주
        enemyB = new GameObject[1];
        enemyL = new GameObject[10];
        enemyM = new GameObject[10];
        enemyS = new GameObject[20];

        itemCoin = new GameObject[20];
        itemPower = new GameObject[10];
        itemBoom = new GameObject[10];

        bulletPlayerA = new GameObject[100];
        bulletPlayerB = new GameObject[100];
        bulletEnemyA = new GameObject[100];
        bulletEnemyB = new GameObject[100];
        bulletFollower = new GameObject[100];
        bulletBossA = new GameObject[50];
        bulletBossB = new GameObject[1000];

        Generate();
    }

    void Generate()
    {
        //Enemy
        for (int index = 0; index < enemyB.Length; index++)
        {
            //Instantiate를 하기 위해서는 Prefab이 필요하다 22.04.13 by승주
            enemyB[index] = Instantiate(enemyBPrefab);
            enemyB[index].SetActive(false);

        }
        for (int index = 0; index < enemyL.Length; index++)
        {
            //Instantiate를 하기 위해서는 Prefab이 필요하다 22.04.13 by승주
            enemyL[index] = Instantiate(enemyLPrefab);
            enemyL[index].SetActive(false);

        }
        for (int index = 0; index < enemyM.Length; index++)
        {
            //Instantiate를 하기 위해서는 Prefab이 필요하다 22.04.13 by승주
            enemyM[index] = Instantiate(enemyMPrefab);
            enemyM[index].SetActive(false);
        }
        for (int index = 0; index < enemyS.Length; index++)
        {
            //Instantiate를 하기 위해서는 Prefab이 필요하다 22.04.13 by승주
            enemyS[index] = Instantiate(enemySPrefab);
            enemyS[index].SetActive(false);
        }

        //Item
        for (int index = 0; index < itemCoin.Length; index++)
        {
            //Instantiate를 하기 위해서는 Prefab이 필요하다 22.04.13 by승주
            itemCoin[index] = Instantiate(itemCoinPrefab);
            itemCoin[index].SetActive(false);

        }
        for (int index = 0; index < itemPower.Length; index++)
        {
            //Instantiate를 하기 위해서는 Prefab이 필요하다 22.04.13 by승주
            itemPower[index] = Instantiate(itemPowerPrefab);
            itemPower[index].SetActive(false);
        }
        for (int index = 0; index < itemBoom.Length; index++)
        {
            //Instantiate를 하기 위해서는 Prefab이 필요하다 22.04.13 by승주
            itemBoom[index] = Instantiate(itemBoomPrefab);
            itemBoom[index].SetActive(false);
        }

        //Bullet
        for (int index = 0; index < bulletPlayerA.Length; index++)
        {
            //Instantiate를 하기 위해서는 Prefab이 필요하다 22.04.13 by승주
            bulletPlayerA[index] = Instantiate(bulletPlayerAPrefab);
            bulletPlayerA[index].SetActive(false);

        }
        for (int index = 0; index < bulletPlayerB.Length; index++)
        {
            //Instantiate를 하기 위해서는 Prefab이 필요하다 22.04.13 by승주
            bulletPlayerB[index] = Instantiate(bulletPlayerBPrefab);
            bulletPlayerB[index].SetActive(false);

        }
        for (int index = 0; index < bulletEnemyA.Length; index++)
        {
            //Instantiate를 하기 위해서는 Prefab이 필요하다 22.04.13 by승주
            bulletEnemyA[index] = Instantiate(bulletEnemyAPrefab);
            bulletEnemyA[index].SetActive(false);
        }
        for (int index = 0; index < bulletEnemyB.Length; index++)
        {
            //Instantiate를 하기 위해서는 Prefab이 필요하다 22.04.13 by승주
            bulletEnemyB[index] = Instantiate(bulletEnemyBPrefab);
            bulletEnemyB[index].SetActive(false);

        }  
        for (int index = 0; index < bulletFollower.Length; index++)
        {
            //Instantiate를 하기 위해서는 Prefab이 필요하다 22.04.14 by승주
            bulletFollower[index] = Instantiate(bulletFollowerPrefab);
            bulletFollower[index].SetActive(false);

        }
        for (int index = 0; index < bulletBossA.Length; index++)
        {
            //Instantiate를 하기 위해서는 Prefab이 필요하다 22.04.13 by승주
            bulletBossA[index] = Instantiate(bulletBossAPrefab);
            bulletBossA[index].SetActive(false);

        }
        for (int index = 0; index < bulletBossB.Length; index++)
        {
            //Instantiate를 하기 위해서는 Prefab이 필요하다 22.04.14 by승주
            bulletBossB[index] = Instantiate(bulletBossBPrefab);
            bulletBossB[index].SetActive(false);

        }
    }

    //Pool 활용 22.04.13 by승주 
    public GameObject MakeObj(string type)
    {

        switch (type)
        {
            //Enemy
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;

            //Item
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemBoom":
                targetPool = itemBoom;
                break;

            //Bullet
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "BulletFollower":
                targetPool = bulletFollower;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
            case "BulletBossB":
                targetPool = bulletBossB;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            //비활성화 된 Object에 접근하여 활성화 후, 반환 시키는 기능 22.04.13 by승주 
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }

    //지정한 Object Pool을 가져오는 함수 기능 22.04.13 by승주
    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            //Enemy
            case "EnemyB":
                targetPool = enemyL;
                break; 
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;

            //Item
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemBoom":
                targetPool = itemBoom;
                break;

            //Bullet
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "BulletFollower":
                targetPool = bulletFollower;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
            case "BulletBossB":
                targetPool = bulletBossB;
                break;
        }
                return targetPool;
    }
}
