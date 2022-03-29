using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject stage = null;
    GameObject currentStage;
    Transform[] stagePlates;

    //�� ��Ÿ�� ���� /22.03.23 by����
    //��ҿ��� �ؿ� �ִٰ� ���� plate�� ���� �� �ڿ������� ���� �ö���鼭 �����ְ� ���ִ� �Լ�./22.03.23 by����
    [SerializeField] float offsetY = 3;
    [SerializeField] float plateSpeed = 10;

    //ť�갡 ��ŭ �������� �� Ȯ���ϴ� �Լ� /22.03.23 by����
    int stepCount = 0;

    //�� plate������ ����/22.03.23 by����
    int totalPlateCount = 0;


    //���� ���� �ϸ� ������ �ִ� Stage ���� �ϴ� ���. 22.03.29 by����
    public void RemoveStage()
    {
        if (currentStage != null)
            Destroy(currentStage);
    }

    public void SettingStage()
    {
        //Stage�� ���� �����Ǹ� StepCount�� 0���ΰ��� ������ �� ���� �ʱ�ȭ ���. 22.03.29 by����
        stepCount = 0;

        currentStage = Instantiate(stage, Vector3.zero, Quaternion.identity);

        // Stage�� �ִ� plates���� �����ͼ� stagePlates ����.  /22.03.23 by����
        stagePlates = currentStage.GetComponent<Stage>().plates;

        //�迭 ���� ��ŭ �־��ش�. //22.03.23 by����
        totalPlateCount = stagePlates.Length;

        //���۰� ���ÿ� ������ �������� �ϴ� �Լ� //22.03.23 by����
        for (int i = 0; i < totalPlateCount; i++)
        {
            //stagePlates�� ��� index�� position���� �ڱ� �ڽ��� ��ġ���� �Ʒ��θ� ���� �ϴ� �Լ�  /22.03.23 by����
            stagePlates[i].position = new Vector3(stagePlates[i].position.x,
                                                  stagePlates[i].position.y + offsetY,
                                                  stagePlates[i].position.z);
        }
    }

    //���� plate�� �����ְ� �ϴ� �Լ� /22.03.23 by����
    public void ShowNextPlate()
    {
        //stepCount�� �� ������ ���� ���� ���� Ȱ��ȭ ��Ű�� �Լ� /22.03.23 by����
        if (stepCount < totalPlateCount)

            StartCoroutine(MovePlateCo(stepCount++));


    }

    IEnumerator MovePlateCo(int p_num)
    {
        //stagePlates�� �ִ� index�� Ȱ��ȭ �����ִ� �Լ� /22.03.23 by����
        //index�� ������ �߰��� �� /22.03.23 by����
        stagePlates[p_num].gameObject.SetActive(true);

        //��ŭ �ö�;��ϴ� �� ��ǥ��(������) /22.03.24 by����
        Vector3 t_destPos = new Vector3(stagePlates[p_num].position.x,
                                        stagePlates[p_num].position.y - offsetY,
                                        stagePlates[p_num].position.z);

        //�ݺ����� ���� õõ�� �÷��ִ� �Լ� /22.03.24 by����
        while (Vector3.SqrMagnitude(stagePlates[p_num].position - t_destPos) >= 0.001f)
        {
            //stagePlates�� ���� 0�� �� ������ �ݺ��� ���� /22.03.24 by����
            stagePlates[p_num].position = Vector3.Lerp(stagePlates[p_num].position, t_destPos, plateSpeed * Time.deltaTime);
            yield return null;
        }

        //�ݺ����� ������ �� ��ġ�� ����ϰ� t_destPos�� ��ü /22.03.24 by����
        stagePlates[p_num].position = t_destPos;
    }

}
