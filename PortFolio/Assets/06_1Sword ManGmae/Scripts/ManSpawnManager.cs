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

    //enemy�� �����Ǵ� Ƚ�� ���� ��� 22.05.01 ����
    int spawnNum;


    private void Awake()
    {
        //������ ���� ���� �ϱ� ���ؼ� �̱��� ��� 22.05.01 ����
        if (instance == null) instance = this;

        
    }

    public  void WaveStart()
    {
        title.SetActive(false);
        waveText.gameObject.SetActive(false);

        //�� wave�� enemy �� 10���� ���� �ϴ� ��� 22.05.01 ����
        spawnNum = 10;

        //wave�� �ð����� �ϴ� ��� 22.05.01 ����
        waveStep += 1;
        //wave�� �ö󰡸�  text�� �ö󰡰� �ϴ� ��� 22.05.01 ����
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

            //enemy 10���� ������ boss ������ �ϴ� ��� 22.05.01 ����
            //spawnNum�� 0���� �۰ų� �������� 
            if (spawnNum <= 0)
            {
                StopCoroutine("SpawnEnemy");
                //boss ���� ���� ��� 22.05.01 ����
                boss.transform.position = new Vector3(
                    Random.Range(-17.5f, 17.5f), 0,
                    Random.Range(-18.5f, 18.5f));

                //wave �ܰ谡 �ö󰥼��� boss �ɷ� ���� ��� 22.05.01 ����
                boss.GetComponent<ManGameEnemy>().StatSet(waveStep);

                //boss Ÿ���� player ��� 22.05.01 ����
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
                //enemy ���� ���� ��� 22.05.01 ����
                enemy[i].transform.position = new Vector3(
                    Random.Range(-17.5f, 17.5f), 0,
                    Random.Range(-18.5f, 18.5f));

                //wave �ܰ谡 �ö󰥼��� enemy�ɷ�ġ ���� ��� 22.05.01 ����
                enemy[i].GetComponent<ManGameEnemy>().StatSet(waveStep);

                //enemy�� Ÿ���� player ��� 22.05.01 ����
                enemy[i].GetComponent<ManGameEnemy>().target = player;
                enemy[i].SetActive(true);

                //enemy�� �� ���� ���� �� ���� spawnNum�� 1�� �پ��� ��� 22.05.01 ����
                spawnNum -= 1;
                return;
            }
        }
    }
}
