using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollGameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;

    //enmey Prefab �迭�� ���� ��ġ ��� 22.04.07 by����
    public Transform[] spawnPoints;

    //enemy ���� ������ ��Ű�� ��� 22.04.07 by����
    public float maxSpawnDelay;
    public float curSpawnDelay;

    void Update()
    {
        //curSpawnDelay�� ���� �帣�� �ִ� �ð� 22.04.07 by����
        curSpawnDelay += Time.deltaTime;

        //���࿡ curSpawnDelay���� maxSpawnDelay���� ũ�ٸ� enemy�� ��ȯ ��Ű�� ��� 22.04.07 by����
        if (curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();

            //�����̸� Random �ϰ� �ؼ� enemy�� Random �����ϴ� ��� 22.04.07 by����
            maxSpawnDelay = Random.Range(0.5f, 3f);

            curSpawnDelay = 0;
        }
    }

    //enemy ��ȯ ��Ű�� ��� 22.04.07 by����
    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 5);

        //Radom���� ������ enemy Prefab, ���� ��ġ�� �����ִ� ��� 22.04.07 by����
        Instantiate(enemyObjs[ranEnemy],
                    spawnPoints[ranPoint].position,
                    spawnPoints[ranPoint].rotation);
    }
}
