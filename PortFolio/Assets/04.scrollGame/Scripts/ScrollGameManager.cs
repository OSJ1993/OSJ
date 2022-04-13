using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScrollGameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;

    //enmey Prefab �迭�� ���� ��ġ ��� 22.04.07 by����
    public Transform[] spawnPoints;

    //enemy ���� ������ ��Ű�� ��� 22.04.07 by����
    public float maxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;

    //UI 22.04.11 by����
    public Text scoreText;
    public Image[] lifeImage;
    public Image[] boomImage;
    public GameObject gameOverSet;


   
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

        //UI Score Update ��� 22.04.11 by����
        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);

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

    //
    public void UpdateLifeIcon(int life)
    {
        //Image�� �ϴ� ���� ���·� �ΰ� ������ ������ �����ִ� ���  22.04.11 by����
        for (int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0f);
        }

        


        //���� index�� 0�̰� index�� OnTriggerEnter2D�ִ� ���� life����  22.04.11 by����
        //�����ִ� life������ŭ�� Ȱ��ȭ �����ִ� ���  22.04.11 by����

        for (int index = 0; index < life; index++)
        {

            lifeImage[index].color = new Color(1, 1, 1, 1f);
        }
    }



    public void UpdateBoomIcon(int boom)
    {
        //Image�� �ϴ� ���� ���·� �ΰ� boom image ������ �����ִ� ���  22.04.11 by����
        for (int index = 0; index < 3; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 0.3f);
        }

        //���� index�� 0�̰� index�� OnTriggerEnter2D�ִ� ���� life����  22.04.11 by����
        //�����ִ� boom������ŭ�� Ȱ��ȭ �����ִ� ���  22.04.11 by����
        for (int index = 0; index < boom; index++)
        {

            boomImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    //player ���� ��Ű�� ��� 22.04.08 by����
    public void RespawnPlayer()
    {
        //player ���� ��Ű�� �ð� ���� �α� ���� Invoke() ��� //22.04.10 by����
        Invoke("RespawnPlayerExe", 2f);
    }

    void RespawnPlayerExe()
    {

        player.transform.position = Vector3.down * 3;
        player.SetActive(true);

        //Player���� ������ bool ������ �ٽ� �ʱ�ȭ �����ִ� ��� 22.04.11 by����
        Player playerLogic = player.GetComponent<Player>();
        playerLogic.isHit = false;

    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene("03.scrollGame");
    }
}
