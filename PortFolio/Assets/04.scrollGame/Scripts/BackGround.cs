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
        //camera ���� ���� ��� 22.04.13 by����
        viewHeight = Camera.main.orthographicSize * 2;
    }

    void Update()
    {
        //tranform�� �̿��� �̵� ���� 22.04.13 by����
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime;
        transform.position = curPos + nextPos;

        //�۷ι� ����
        if (sprites[endIndex].position.y < viewHeight * (-1))
        {
            //Sprite ���� ��Ű�� ��� 22.04.13 by����
            Vector3 backSpritePos = sprites[startIndex].localPosition;
            Vector3 frontSpritePos = sprites[endIndex].localPosition;

            //Scene���� �ø��� ������ local ���� 22.04.13 by����
            sprites[endIndex].transform.localPosition = backSpritePos + Vector3.up * 15;

            //�̵��� �Ϸ�Ǹ� endIndex�� startIndex�� ���� ��Ű�� ��� 22.04.13 by����
            int startIndexSave = startIndex;
            startIndex = endIndex;
            endIndex = (startIndexSave - 1 == -1) ? sprites.Length - 1 : startIndexSave - 1;
        }
    }
}
