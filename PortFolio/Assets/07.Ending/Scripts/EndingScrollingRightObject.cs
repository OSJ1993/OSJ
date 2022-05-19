using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScrollingRightObject : MonoBehaviour
{
    public float speed;






    void Update()
    {

        //초당 speed의 속도로 오른쪽으로 평행이동 기능 22.05.13 승주
        transform.Translate(Vector3.right * speed * Time.deltaTime);


    }

}

