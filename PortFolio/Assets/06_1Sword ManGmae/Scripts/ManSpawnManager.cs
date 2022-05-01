using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManSpawnManager : MonoBehaviour
{
    public static ManSpawnManager instance;

    public Transform player;

    public GameObject[] enemy;
    public GameObject boss;

    public TextMeshProUGUI waveText;

    public GameObject title;

    public int waveStep;

    //enemy가 생성되는 횟수 제한 기능 22.05.01 승주
    int spawnNum;


    private void Awake()
    {
        //누구라도 쉽게 접근 하기 위해서 싱글턴 기능 22.05.01 승주
        if (instance == null) instance = this;

        
    }

    public  void WaveStart()
    {
        title.SetActive(false);
        waveText.gameObject.SetActive(false);

        //한 wave당 enemy 수 10으로 제한 하는 기능 22.05.01 승주
        spawnNum = 10;

        //wave가 올가가게 하는 기능 22.05.01 승주
        waveStep += 1;
        //wave가 올라가면  text도 올라가게 하는 기능 22.05.01 승주
        waveText.text = string.Format("Wave {0}", waveStep);
        waveText.gameObject.SetActive(true);

        StartCoroutine("SpawnEnemy");
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            Spawn();

            //enemy 10마리 잡으면 boss 나오게 하는 기능 22.05.01 승주
            //spawnNum이 0보다 작거나 같아지면 
            if (spawnNum <= 0)
            {
                StopCoroutine("SpawnEnemy");
                //boss 랜덤 생성 기능 22.05.01 승주
                boss.transform.position = new Vector3(
                    Random.Range(-17.5f, 17.5f), 0,
                    Random.Range(-18.5f, 18.5f));

                //wave 단계가 올라갈수록 boss 능력 증가 기능 22.05.01 승주
                boss.GetComponent<ManGameEnemy>().StatSet(waveStep);

                //boss 타겟은 player 기능 22.05.01 승주
                boss.GetComponent<ManGameEnemy>().target = player;
                boss.SetActive(true);
            }
        }
    }


    void Spawn()
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            if (!enemy[i].activeSelf)
            {
                //enemy 랜덤 생성 기능 22.05.01 승주
                enemy[i].transform.position = new Vector3(
                    Random.Range(-17.5f, 17.5f), 0,
                    Random.Range(-18.5f, 18.5f));

                //wave 단계가 올라갈수록 enemy능력치 증가 기능 22.05.01 승주
                enemy[i].GetComponent<ManGameEnemy>().StatSet(waveStep);

                //enemy의 타겟은 player 기능 22.05.01 승주
                enemy[i].GetComponent<ManGameEnemy>().target = player;
                enemy[i].SetActive(true);

                //enemy가 한 마리 나올 떄 마다 spawnNum가 1씩 줄어드는 기능 22.05.01 승주
                spawnNum -= 1;
                return;
            }
        }
    }
}
