using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScrollingRightObject : MonoBehaviour
{
    public float speed;






    void Update()
    {

        //�ʴ� speed�� �ӵ��� ���������� �����̵� ��� 22.05.13 ����
        transform.Translate(Vector3.right * speed * Time.deltaTime);


    }

}

