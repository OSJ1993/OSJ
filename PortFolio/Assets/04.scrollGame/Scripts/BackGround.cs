using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{

    public float speed;
    public int startIndex;
    public int endIndex;
    public Transform[] sprites;

    float viewHeight;

    private void Awake()
    {
        //camera 높이 설정 기능 22.04.13 by승주
        viewHeight = Camera.main.orthographicSize * 2;
    }

    void Update()
    {
        //tranform을 이용한 이동 구현 22.04.13 by승주
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime;
        transform.position = curPos + nextPos;

        //글로벌 기준
        if (sprites[endIndex].position.y < viewHeight * (-1))
        {
            //Sprite 재사용 시키는 기능 22.04.13 by승주
            Vector3 backSpritePos = sprites[startIndex].localPosition;
            Vector3 frontSpritePos = sprites[endIndex].localPosition;

            //Scene에서 올리기 떄문에 local 기준 22.04.13 by승주
            sprites[endIndex].transform.localPosition = backSpritePos + Vector3.up * 15;

            //이동이 완료되면 endIndex가 startIndex로 갱신 시키는 기능 22.04.13 by승주
            int startIndexSave = startIndex;
            startIndex = endIndex;
            endIndex = (startIndexSave - 1 == -1) ? sprites.Length - 1 : startIndexSave - 1;
        }
    }
}
