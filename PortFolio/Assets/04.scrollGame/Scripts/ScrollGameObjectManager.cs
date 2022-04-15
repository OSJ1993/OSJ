using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Object Ǯ���� ���� ��� 22.04.13 by����
//������ �޸� ���� ������ �÷�Ʈ(���� ������ �޸� ���� ���) ��� 22.04.13 by����

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

    //Prefab�� ���� �Ͽ� ������ �迭 ���� 22.04.13 by����
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
        //�ѹ��� ������ ������ ����Ͽ� �迭 ���� �Ҵ� 22.04.13 by����
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
            //Instantiate�� �ϱ� ���ؼ��� Prefab�� �ʿ��ϴ� 22.04.13 by����
            enemyB[index] = Instantiate(enemyBPrefab);
            enemyB[index].SetActive(false);

        }
        for (int index = 0; index < enemyL.Length; index++)
        {
            //Instantiate�� �ϱ� ���ؼ��� Prefab�� �ʿ��ϴ� 22.04.13 by����
            enemyL[index] = Instantiate(enemyLPrefab);
            enemyL[index].SetActive(false);

        }
        for (int index = 0; index < enemyM.Length; index++)
        {
            //Instantiate�� �ϱ� ���ؼ��� Prefab�� �ʿ��ϴ� 22.04.13 by����
            enemyM[index] = Instantiate(enemyMPrefab);
            enemyM[index].SetActive(false);
        }
        for (int index = 0; index < enemyS.Length; index++)
        {
            //Instantiate�� �ϱ� ���ؼ��� Prefab�� �ʿ��ϴ� 22.04.13 by����
            enemyS[index] = Instantiate(enemySPrefab);
            enemyS[index].SetActive(false);
        }

        //Item
        for (int index = 0; index < itemCoin.Length; index++)
        {
            //Instantiate�� �ϱ� ���ؼ��� Prefab�� �ʿ��ϴ� 22.04.13 by����
            itemCoin[index] = Instantiate(itemCoinPrefab);
            itemCoin[index].SetActive(false);

        }
        for (int index = 0; index < itemPower.Length; index++)
        {
            //Instantiate�� �ϱ� ���ؼ��� Prefab�� �ʿ��ϴ� 22.04.13 by����
            itemPower[index] = Instantiate(itemPowerPrefab);
            itemPower[index].SetActive(false);
        }
        for (int index = 0; index < itemBoom.Length; index++)
        {
            //Instantiate�� �ϱ� ���ؼ��� Prefab�� �ʿ��ϴ� 22.04.13 by����
            itemBoom[index] = Instantiate(itemBoomPrefab);
            itemBoom[index].SetActive(false);
        }

        //Bullet
        for (int index = 0; index < bulletPlayerA.Length; index++)
        {
            //Instantiate�� �ϱ� ���ؼ��� Prefab�� �ʿ��ϴ� 22.04.13 by����
            bulletPlayerA[index] = Instantiate(bulletPlayerAPrefab);
            bulletPlayerA[index].SetActive(false);

        }
        for (int index = 0; index < bulletPlayerB.Length; index++)
        {
            //Instantiate�� �ϱ� ���ؼ��� Prefab�� �ʿ��ϴ� 22.04.13 by����
            bulletPlayerB[index] = Instantiate(bulletPlayerBPrefab);
            bulletPlayerB[index].SetActive(false);

        }
        for (int index = 0; index < bulletEnemyA.Length; index++)
        {
            //Instantiate�� �ϱ� ���ؼ��� Prefab�� �ʿ��ϴ� 22.04.13 by����
            bulletEnemyA[index] = Instantiate(bulletEnemyAPrefab);
            bulletEnemyA[index].SetActive(false);
        }
        for (int index = 0; index < bulletEnemyB.Length; index++)
        {
            //Instantiate�� �ϱ� ���ؼ��� Prefab�� �ʿ��ϴ� 22.04.13 by����
            bulletEnemyB[index] = Instantiate(bulletEnemyBPrefab);
            bulletEnemyB[index].SetActive(false);

        }  
        for (int index = 0; index < bulletFollower.Length; index++)
        {
            //Instantiate�� �ϱ� ���ؼ��� Prefab�� �ʿ��ϴ� 22.04.14 by����
            bulletFollower[index] = Instantiate(bulletFollowerPrefab);
            bulletFollower[index].SetActive(false);

        }
        for (int index = 0; index < bulletBossA.Length; index++)
        {
            //Instantiate�� �ϱ� ���ؼ��� Prefab�� �ʿ��ϴ� 22.04.13 by����
            bulletBossA[index] = Instantiate(bulletBossAPrefab);
            bulletBossA[index].SetActive(false);

        }
        for (int index = 0; index < bulletBossB.Length; index++)
        {
            //Instantiate�� �ϱ� ���ؼ��� Prefab�� �ʿ��ϴ� 22.04.14 by����
            bulletBossB[index] = Instantiate(bulletBossBPrefab);
            bulletBossB[index].SetActive(false);

        }
    }

    //Pool Ȱ�� 22.04.13 by���� 
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
            //��Ȱ��ȭ �� Object�� �����Ͽ� Ȱ��ȭ ��, ��ȯ ��Ű�� ��� 22.04.13 by���� 
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }

    //������ Object Pool�� �������� �Լ� ��� 22.04.13 by����
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
