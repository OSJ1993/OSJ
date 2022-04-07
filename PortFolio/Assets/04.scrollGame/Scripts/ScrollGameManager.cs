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

    public GameObject player;

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
        int ranPoint = Random.Range(0, 9);

        //Radom���� ������ enemy Prefab, ���� ��ġ�� �����ִ� ��� 22.04.07 by����
        GameObject enemy = Instantiate(enemyObjs[ranEnemy],
                                      spawnPoints[ranPoint].position,
                                      spawnPoints[ranPoint].rotation);


        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();

        //Prefab�� �ִ� enemy�� Scene�� �ִ� spawn���� spawn�Ǽ� Scene�� �ִ� player���� ���� �� �� �ְ� ���ֱ� ���� ����  22.04.07 by����
        enemyLogic.player = player;

        //enemy �ӵ��� GameManager�� ���� �ϴ� ��� 22.04.07 by����
        //enemy�� if�� �����ʿ��� else if�� ���ʿ��� spawn�ǰ� ���ִ� ��� 22.04.07 by����
        //else�� enemy�� ������ �Ʒ��� spawn�ǰ� ���ִ� ��� 22.04.07 by����
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
