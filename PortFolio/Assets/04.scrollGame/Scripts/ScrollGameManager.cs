using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//IO=Input=입력, Output=출력 이라는 뜻 22.04.13 by승주
using System.IO;

public class ScrollGameManager : MonoBehaviour
{
    public string[] enemyObjs;

    //enmey Prefab 배열과 생성 위치 기능 22.04.07 by승주
    public Transform[] spawnPoints;

    //enemy 생성 딜레이 시키는 기능 22.04.07 by승주
    public float nextSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;

    //UI 22.04.11 by승주
    public Text scoreText;
    public Image[] lifeImage;
    public Image[] boomImage;
    public GameObject gameOverSet;

    public ScrollGameObjectManager scrollObjectManager;


    //Enemy 출현에 관련된 변수 생성 22.04.13 by승주
    //<Spwan>에 담겨 있는 List 기능 22.04.13 by승주
    public List<Spawn> spawnList;

    //Enemy가 소환 될 수 있게 해주는 기능 22.04.13 by승주
    public int spawnIndex;

    //모든 ReadSpawn이 끝났을 때 쓸 기능 22.04.13 by승주
    public bool spawnEnd;


    void Awake()
    {
        spawnList = new List<Spawn>();

        enemyObjs = new string[] { "EnemyS", "EnemyM", "EnemyL" };

        ReadSpawnFile();
    }

    // Spawn Scrtps에서 만들었던 text(메모장) 불러오는 기능 22.04.13 by승주
    void ReadSpawnFile()
    {
        //변수 초기화 기능 22.04.13 by승주
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        //ReadSpawnFile 읽기 기능 22.04.13 by승주
        //as:  Spawn에서 만들었던 textFile(메모장)이 맞는 지 아닌 지 확인 하는 기능 22.04.13 by승주
        TextAsset textFile = Resources.Load("Stage_0") as TextAsset;


        //StringReader: File 내의 문자열 데이터 읽기 Class 기능(using System.IO에서 나온 Class) 22.04.13 by승주 
        StringReader stringReader = new StringReader(textFile.text);

        //while문으로 텍스트 데이터 끝애 다다를 때까지 반복 하는 기능 22.04.13 by승주
        while (stringReader != null)
        {
            //ReadLine(): 텍스트 데이터를 한 줄씩 반환(자동 줄 바꿈) 하는 기능 22.04.13 by승주
            string line = stringReader.ReadLine();

            Debug.Log(line);

            if (line == null)
                break;

            //ReadSpawnDate 생성 기능 22.04.13 by승주
            //저장 할 때는 구조체를 이용해야 한다. 22.04.13 by승주
            Spawn spawnDate = new Spawn();

            //split(): 지정한 구분 문자로 문자열을 나누는 함수 기능 22.04.13 by승주
            spawnDate.delay = float.Parse(line.Split(',')[0]);
            spawnDate.type = line.Split(',')[1];
            spawnDate.point = int.Parse(line.Split(',')[2]);

            //구조체 변수를 체우고 리스트에 저장하는 기능 22.04.13 by승주
            spawnList.Add(spawnDate);

        }

        //stringReader로 열어둔 파일은 작입이 끝난 후 꼭 닫기! 22.04.13 by승주
        //TextFile(메모장) 닫기 기능 22.04.13 by승주
        stringReader.Close();

        //첫번째 Enemy가 spawn에서 출현 시간 delay시키는 기능 22.04.13 by승주
        nextSpawnDelay = spawnList[0].delay;

    }

    void Update()
    {
        //curSpawnDelay는 현재 흐르고 있는 시간 22.04.07 by승주
        curSpawnDelay += Time.deltaTime;

        //만약에 curSpawnDelay값이 nextSpawnDelay보다 크다면 enemy를 소환 시키는 기능 22.04.07 by승주
        //spawn이 다 됬는 지 안 됬는 지 확인 하기 위한(!spawnEnd) 기능 22.04.13 by승주
        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }

        //UI Score Update 기능 22.04.11 by승주
        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);

    }

    //enemy 소환 시키는 기능 22.04.07 by승주
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

        //Radom으로 정해진 enemy Prefab, 생성 위치를 정해주는 기능 22.04.07 by승주
        GameObject enemy = scrollObjectManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();

        //Prefab에 있는 enemy가 Scene에 있는 spawn에서 spawn되서 Scene에 있는 player에게 접근 할 수 있게 해주기 위한 변수  22.04.07 by승주
        enemyLogic.player = player;
        enemyLogic.scrollObjectManager = scrollObjectManager;

        //enemy 속도를 GameManager가 관리 하는 기능 22.04.07 by승주
        //enemy가 if는 오른쪽에서 else if는 왼쪽에서 spawn되게 해주는 기능 22.04.07 by승주
        //else는 enemy가 위에서 아래로 spawn되게 해주는 기능 22.04.07 by승주
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

        //ReadSpawn index 증가 기능 22.04.13 by승주
        spawnIndex++;
        if (spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        //enemy 생성이 완료 되면 다음 enemy 생성을 위한 delay 갱신 기능 22.04.13 by승주
        nextSpawnDelay = spawnList[spawnIndex].delay;
    }


    public void UpdateLifeIcon(int life)
    {
        //Image를 일단 투명 상태로 두고 목숨대로 반투명 시켜주는 기능  22.04.11 by승주
        for (int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0f);
        }




        //현재 index는 0이고 index는 OnTriggerEnter2D있는 현재 life까지  22.04.11 by승주
        //남아있는 life갯수만큼만 활성화 시켜주는 기능  22.04.11 by승주

        for (int index = 0; index < life; index++)
        {

            lifeImage[index].color = new Color(1, 1, 1, 1f);
        }
    }



    public void UpdateBoomIcon(int boom)
    {
        //Image를 일단 투명 상태로 두고 boom image 반투명 시켜주는 기능  22.04.11 by승주
        for (int index = 0; index < 3; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 0.3f);
        }

        //현재 index는 0이고 index는 OnTriggerEnter2D있는 현재 life까지  22.04.11 by승주
        //남아있는 boom갯수만큼만 활성화 시켜주는 기능  22.04.11 by승주
        for (int index = 0; index < boom; index++)
        {

            boomImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    //player 복귀 시키는 기능 22.04.08 by승주
    public void RespawnPlayer()
    {
        //player 복귀 시키는 시간 차를 두기 위해 Invoke() 사용 //22.04.10 by승주
        Invoke("RespawnPlayerExe", 2f);
    }

    void RespawnPlayerExe()
    {

        player.transform.position = Vector3.down * 3;
        player.SetActive(true);

        //Player에서 선언한 bool 변수를 다시 초기화 시켜주는 기능 22.04.11 by승주
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
