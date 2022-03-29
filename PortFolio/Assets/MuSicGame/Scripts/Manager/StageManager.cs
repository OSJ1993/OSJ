using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject stage = null;
    GameObject currentStage;
    Transform[] stagePlates;

    //길 나타는 연출 /22.03.23 by승주
    //평소에는 밑에 있다가 다음 plate가 나올 때 자연스럽게 위로 올라오면서 맞춰주게 해주는 함수./22.03.23 by승주
    [SerializeField] float offsetY = 3;
    [SerializeField] float plateSpeed = 10;

    //큐브가 얼만큼 움직였는 지 확인하는 함수 /22.03.23 by승주
    int stepCount = 0;

    //총 plate갯수를 선언/22.03.23 by승주
    int totalPlateCount = 0;


    //게임 시작 하면 기존에 있던 Stage 제거 하는 기능. 22.03.29 by승주
    public void RemoveStage()
    {
        if (currentStage != null)
            Destroy(currentStage);
    }

    public void SettingStage()
    {
        //Stage가 새로 생성되면 StepCount를 0으로게임 시작할 때 마다 초기화 기능. 22.03.29 by승주
        stepCount = 0;

        currentStage = Instantiate(stage, Vector3.zero, Quaternion.identity);

        // Stage에 있는 plates들을 꺼내와서 stagePlates 저장.  /22.03.23 by승주
        stagePlates = currentStage.GetComponent<Stage>().plates;

        //배열 길이 만큼 넣어준다. //22.03.23 by승주
        totalPlateCount = stagePlates.Length;

        //시작과 동시에 밑으로 내려가게 하는 함수 //22.03.23 by승주
        for (int i = 0; i < totalPlateCount; i++)
        {
            //stagePlates의 모든 index의 position값을 자기 자신의 위치에서 아래로만 가게 하는 함수  /22.03.23 by승주
            stagePlates[i].position = new Vector3(stagePlates[i].position.x,
                                                  stagePlates[i].position.y + offsetY,
                                                  stagePlates[i].position.z);
        }
    }

    //다음 plate를 보여주게 하는 함수 /22.03.23 by승주
    public void ShowNextPlate()
    {
        //stepCount가 총 갯수를 넘지 않을 때만 활성화 시키는 함수 /22.03.23 by승주
        if (stepCount < totalPlateCount)

            StartCoroutine(MovePlateCo(stepCount++));


    }

    IEnumerator MovePlateCo(int p_num)
    {
        //stagePlates에 있는 index를 활성화 시켜주는 함수 /22.03.23 by승주
        //index의 기준은 발걸음 수 /22.03.23 by승주
        stagePlates[p_num].gameObject.SetActive(true);

        //얼만큼 올라와야하는 지 목표값(목적지) /22.03.24 by승주
        Vector3 t_destPos = new Vector3(stagePlates[p_num].position.x,
                                        stagePlates[p_num].position.y - offsetY,
                                        stagePlates[p_num].position.z);

        //반복문을 통해 천천히 올려주는 함수 /22.03.24 by승주
        while (Vector3.SqrMagnitude(stagePlates[p_num].position - t_destPos) >= 0.001f)
        {
            //stagePlates가 거의 0이 될 떄까지 반복문 실행 /22.03.24 by승주
            stagePlates[p_num].position = Vector3.Lerp(stagePlates[p_num].position, t_destPos, plateSpeed * Time.deltaTime);
            yield return null;
        }

        //반복문이 나왔을 때 위치를 깔끔하게 t_destPos로 교체 /22.03.24 by승주
        stagePlates[p_num].position = t_destPos;
    }

}
