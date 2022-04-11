using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollGameItem : MonoBehaviour
{
    public string type;
    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        //Item speed �߰� ��� 22.04.11 by����
        rigid.velocity = Vector2.down * 0.3f;
    }

   
}
