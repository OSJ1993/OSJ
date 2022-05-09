using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGameEndTrunBtn : MonoBehaviour
{

    [SerializeField] Sprite active;
    [SerializeField] Sprite inactive;
    [SerializeField] Text btnText;


    void Start()
    {
        //CardTrunManager.OnTurnStarted가 되면 Setup을 넣어서 내trun 일 때 isActive가 true가 되고 내 trun이 아닐 때 false가 되게 하는 기능 22.05.04 승주
        Setup(false);
        CardGameTrunManager.OnTurnStarted += Setup;
    }

    void OnDestroy()
    {
        CardGameTrunManager.OnTurnStarted -= Setup;
    }

    //Setup을 할 때 
    public void Setup(bool isActive)
    {
        //현재 Image의 sprite를 isActive에 따라 active 아니면  inactive를 넣어주고 
        GetComponent<Image>().sprite = isActive ? active : inactive;

        //현재 Button을 가져와서 interactable(활성화, 비활성화 기능 )또한 isActive에 따라 넣어주는 기능 22.05.04 승주
        GetComponent<Button>().interactable = isActive;

        //btnText.Color는 isActive따라 Color32색을 정해주는 기능 22.05.04 승주
        //Color와 Color32의 차이점 : Color32는 0~255숫자를 표현하는 방식이고 Color는 0~1숫자를 표현 하는 방식 22.05.04 승주
        btnText.color = isActive ? new Color32(255, 195, 90, 255) : new Color32(55, 55, 55, 255);
    }


    
}
