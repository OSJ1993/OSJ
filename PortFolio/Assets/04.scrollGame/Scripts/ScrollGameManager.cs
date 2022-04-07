using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollGameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;

    //enmey Prefab 배열과 생성 위치 기능 22.04.07 by승주
    public Transform[] spawnPoints;

    //enemy 생성 딜레이 시키는 기능 22.04.07 by승주
    public float maxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;

    void Update()
    {
        //curSpawnDelay는 현재 흐르고 있는 시간 22.04.07 by승주
        curSpawnDelay += Time.deltaTime;

        //만약에 curSpawnDelay값이 maxSpawnDelay보다 크다면 enemy를 소환 시키는 기능 22.04.07 by승주
        if (curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();

            //딜레이를 Random 하게 해서 enemy를 Random 생성하는 기능 22.04.07 by승주
            maxSpawnDelay = Random.Range(0.5f, 3f);

            curSpawnDelay = 0;
        }
    }

    //enemy 소환 시키는 기능 22.04.07 by승주
    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 9);

        //Radom으로 정해진 enemy Prefab, 생성 위치를 정해주는 기능 22.04.07 by승주
        GameObject enemy = Instantiate(enemyObjs[ranEnemy],
                                      spawnPoints[ranPoint].position,
                                      spawnPoints[ranPoint].rotation);


        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();

        //Prefab에 있는 enemy가 Scene에 있는 spawn에서 spawn되서 Scene에 있는 player에게 접근 할 수 있게 해주기 위한 변수  22.04.07 by승주
        enemyLogic.player = player;

        //enemy 속도를 GameManager가 관리 하는 기능 22.04.07 by승주
        //enemy가 if는 오른쪽에서 else if는 왼쪽에서 spawn되게 해주는 기능 22.04.07 by승주
        //else는 enemy가 위에서 아래로 spawn되게 해주는 기능 22.04.07 by승주
        if (ranPoint == 5 || ranPoint == 6)
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else if (ranPoint == 7 || ranPoint == 8)
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }
    }
}
