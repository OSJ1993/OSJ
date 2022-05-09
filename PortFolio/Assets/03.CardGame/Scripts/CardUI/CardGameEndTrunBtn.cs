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
        //CardTrunManager.OnTurnStarted�� �Ǹ� Setup�� �־ ��trun �� �� isActive�� true�� �ǰ� �� trun�� �ƴ� �� false�� �ǰ� �ϴ� ��� 22.05.04 ����
        Setup(false);
        CardGameTrunManager.OnTurnStarted += Setup;
    }

    void OnDestroy()
    {
        CardGameTrunManager.OnTurnStarted -= Setup;
    }

    //Setup�� �� �� 
    public void Setup(bool isActive)
    {
        //���� Image�� sprite�� isActive�� ���� active �ƴϸ�  inactive�� �־��ְ� 
        GetComponent<Image>().sprite = isActive ? active : inactive;

        //���� Button�� �����ͼ� interactable(Ȱ��ȭ, ��Ȱ��ȭ ��� )���� isActive�� ���� �־��ִ� ��� 22.05.04 ����
        GetComponent<Button>().interactable = isActive;

        //btnText.Color�� isActive���� Color32���� �����ִ� ��� 22.05.04 ����
        //Color�� Color32�� ������ : Color32�� 0~255���ڸ� ǥ���ϴ� ����̰� Color�� 0~1���ڸ� ǥ�� �ϴ� ��� 22.05.04 ����
        btnText.color = isActive ? new Color32(255, 195, 90, 255) : new Color32(55, 55, 55, 255);
    }


    
}
