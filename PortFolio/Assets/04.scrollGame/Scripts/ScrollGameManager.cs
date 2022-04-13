using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//IO=Input=�Է�, Output=��� �̶�� �� 22.04.13 by����
using System.IO;

public class ScrollGameManager : MonoBehaviour
{
    public string[] enemyObjs;

    //enmey Prefab �迭�� ���� ��ġ ��� 22.04.07 by����
    public Transform[] spawnPoints;

    //enemy ���� ������ ��Ű�� ��� 22.04.07 by����
    public float nextSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;

    //UI 22.04.11 by����
    public Text scoreText;
    public Image[] lifeImage;
    public Image[] boomImage;
    public GameObject gameOverSet;

    public ScrollGameObjectManager scrollObjectManager;


    //Enemy ������ ���õ� ���� ���� 22.04.13 by����
    //<Spwan>�� ��� �ִ� List ��� 22.04.13 by����
    public List<Spawn> spawnList;

    //Enemy�� ��ȯ �� �� �ְ� ���ִ� ��� 22.04.13 by����
    public int spawnIndex;

    //��� ReadSpawn�� ������ �� �� ��� 22.04.13 by����
    public bool spawnEnd;


    void Awake()
    {
        spawnList = new List<Spawn>();

        enemyObjs = new string[] { "EnemyS", "EnemyM", "EnemyL" };

        ReadSpawnFile();
    }

    // Spawn Scrtps���� ������� text(�޸���) �ҷ����� ��� 22.04.13 by����
    void ReadSpawnFile()
    {
        //���� �ʱ�ȭ ��� 22.04.13 by����
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        //ReadSpawnFile �б� ��� 22.04.13 by����
        //as:  Spawn���� ������� textFile(�޸���)�� �´� �� �ƴ� �� Ȯ�� �ϴ� ��� 22.04.13 by����
        TextAsset textFile = Resources.Load("Stage_0") as TextAsset;


        //StringReader: File ���� ���ڿ� ������ �б� Class ���(using System.IO���� ���� Class) 22.04.13 by���� 
        StringReader stringReader = new StringReader(textFile.text);

        //while������ �ؽ�Ʈ ������ ���� �ٴٸ� ������ �ݺ� �ϴ� ��� 22.04.13 by����
        while (stringReader != null)
        {
            //ReadLine(): �ؽ�Ʈ �����͸� �� �پ� ��ȯ(�ڵ� �� �ٲ�) �ϴ� ��� 22.04.13 by����
            string line = stringReader.ReadLine();

            Debug.Log(line);

            if (line == null)
                break;

            //ReadSpawnDate ���� ��� 22.04.13 by����
            //���� �� ���� ����ü�� �̿��ؾ� �Ѵ�. 22.04.13 by����
            Spawn spawnDate = new Spawn();

            //split(): ������ ���� ���ڷ� ���ڿ��� ������ �Լ� ��� 22.04.13 by����
            spawnDate.delay = float.Parse(line.Split(',')[0]);
            spawnDate.type = line.Split(',')[1];
            spawnDate.point = int.Parse(line.Split(',')[2]);

            //����ü ������ ü��� ����Ʈ�� �����ϴ� ��� 22.04.13 by����
            spawnList.Add(spawnDate);

        }

        //stringReader�� ����� ������ ������ ���� �� �� �ݱ�! 22.04.13 by����
        //TextFile(�޸���) �ݱ� ��� 22.04.13 by����
        stringReader.Close();

        //ù��° Enemy�� spawn���� ���� �ð� delay��Ű�� ��� 22.04.13 by����
        nextSpawnDelay = spawnList[0].delay;

    }

    void Update()
    {
        //curSpawnDelay�� ���� �帣�� �ִ� �ð� 22.04.07 by����
        curSpawnDelay += Time.deltaTime;

        //���࿡ curSpawnDelay���� nextSpawnDelay���� ũ�ٸ� enemy�� ��ȯ ��Ű�� ��� 22.04.07 by����
        //spawn�� �� ��� �� �� ��� �� Ȯ�� �ϱ� ����(!spawnEnd) ��� 22.04.13 by����
        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }

        //UI Score Update ��� 22.04.11 by����
        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);

    }

    //enemy ��ȯ ��Ű�� ��� 22.04.07 by����
    void SpawnEnemy()
    {
        int enemyIndex = 0;

        switch (spawnList[spawnIndex].type)
        {
            case "S":
                enemyIndex = 0;
                break;

            case "M":
                enemyIndex = 1;
                break;

            case "L":
                enemyIndex = 2;
                break;

        }


        int enemyPoint = spawnList[spawnIndex].point;

        //Radom���� ������ enemy Prefab, ���� ��ġ�� �����ִ� ��� 22.04.07 by����
        GameObject enemy = scrollObjectManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();

        //Prefab�� �ִ� enemy�� Scene�� �ִ� spawn���� spawn�Ǽ� Scene�� �ִ� player���� ���� �� �� �ְ� ���ֱ� ���� ����  22.04.07 by����
        enemyLogic.player = player;
        enemyLogic.scrollObjectManager = scrollObjectManager;

        //enemy �ӵ��� GameManager�� ���� �ϴ� ��� 22.04.07 by����
        //enemy�� if�� �����ʿ��� else if�� ���ʿ��� spawn�ǰ� ���ִ� ��� 22.04.07 by����
        //else�� enemy�� ������ �Ʒ��� spawn�ǰ� ���ִ� ��� 22.04.07 by����
        if (enemyPoint == 5 || enemyPoint == 6)
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else if (enemyPoint == 7 || enemyPoint == 8)
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }

        //ReadSpawn index ���� ��� 22.04.13 by����
        spawnIndex++;
        if (spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        //enemy ������ �Ϸ� �Ǹ� ���� enemy ������ ���� delay ���� ��� 22.04.13 by����
        nextSpawnDelay = spawnList[spawnIndex].delay;
    }


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
