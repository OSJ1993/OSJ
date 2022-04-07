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
        int ranPoint = Random.Range(0, 5);

        //Radom으로 정해진 enemy Prefab, 생성 위치를 정해주는 기능 22.04.07 by승주
        Instantiate(enemyObjs[ranEnemy],
                    spawnPoints[ranPoint].position,
                    spawnPoints[ranPoint].rotation);
    }
}
