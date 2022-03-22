using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{

    //스피드 설정.
    public float noteSpeed = 400;

    //맞춘 도트에 이미지만 제거하고 해당 노트가 파괴되지 않도록 수정.
    UnityEngine.UI.Image noteImage;


    //OnEnable 객체가 활성화 될 때마다 호출되는 함수.
    void OnEnable()
    {
        if (noteImage == null)
            noteImage = GetComponent<Image>();

        noteImage.enabled = true;
    }


    void Update()
    {
        //이 스크립트가 붙어있는 객체 로컬 포지션 값을 오른쪽으로 1초에 noteSpeed 값 만큼 이동할 수 있게 만들기.
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;
    }

    //이 함수가 호출되면 그 이미지에 enabled 비활성화(false) 사라지게 만들기.
    public void HideNote()
    {
        noteImage.enabled = false;
    }

    public bool GetNoteFlag()
    {
        return noteImage.enabled;
    }
}
