using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScrollingLeftObject : MonoBehaviour
{
    // ��� �������� �����̴� ��� 22.05.13 ����
    public float speed;
    

   
    void Update()
    {
        //�ʴ� speed�� �ӵ��� �������� �����̵� ��� 
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
