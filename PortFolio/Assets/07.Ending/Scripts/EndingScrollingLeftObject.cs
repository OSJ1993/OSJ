using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScrollingLeftObject : MonoBehaviour
{
    // 계속 왼쪽으로 움직이는 기능 22.05.13 승주
    public float speed;
    

   
    void Update()
    {
        //초당 speed의 속도로 왼쪽으로 평행이동 기능 
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
