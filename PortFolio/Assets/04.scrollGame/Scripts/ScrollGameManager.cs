using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScrollGameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;

    //enmey Prefab 배열과 생성 위치 기능 22.04.07 by승주
    public Transform[] spawnPoints;

    //enemy 생성 딜레이 시키는 기능 22.04.07 by승주
    public float maxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;

    //UI 22.04.11 by승주
    public Text scoreText;
    public Image[] lifeImage;
    public Image[] boomImage;
    public GameObject gameOverSet;


   
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

        //UI Score Update 기능 22.04.11 by승주
        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);

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

    //
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
