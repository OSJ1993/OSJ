using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBackgroundRightLoop : MonoBehaviour
{
    //����� ���� ���� ��� 22.05.13 ����
    private float width;

    void Awake()
    {
        //���� ���̸� �����ϴ� ��� 22.05.13 ����
        BoxCollider2D backgroundCollder = GetComponent<BoxCollider2D>();
        width = backgroundCollder.size.x;
        
    }


    void Update()
    {
        //���� ��ġ�� �������� ���������� width �̻� �̵����� �� ��ġ�� ���ġ �ϴ� ��� 22.05.13 ����
        if (transform.position.x >= width)
        {
            Reposition();
           
        } 
    }

    private void Reposition()
    {
        //���� ��ġ���� ���������� ���� ���� *2��ŭ �̵� ��� 22.05.13 ����
        Vector2 offset = new Vector2(-width * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
    }
}

