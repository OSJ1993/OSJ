using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBackgroundRightLoop : MonoBehaviour
{
    //배경의 가로 길이 기능 22.05.13 승주
    private float width;

    void Awake()
    {
        //가로 길이를 측정하는 기능 22.05.13 승주
        BoxCollider2D backgroundCollder = GetComponent<BoxCollider2D>();
        width = backgroundCollder.size.x;
        
    }


    void Update()
    {
        //현재 위치가 원점에서 오른쪽으로 width 이상 이동했을 때 위치를 재배치 하는 기능 22.05.13 승주
        if (transform.position.x >= width)
        {
            Reposition();
           
        } 
    }

    private void Reposition()
    {
        //현재 위치에서 오른쪽으로 가로 길이 *2만큼 이동 기능 22.05.13 승주
        Vector2 offset = new Vector2(-width * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
    }
}

