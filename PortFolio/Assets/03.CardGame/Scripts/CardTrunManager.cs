using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class CardTrunManager : MonoBehaviour
{
    //싱글턴 기능 22.05.02 승주
    public static CardTrunManager Inst { get; private set; }
    void Awake() => Inst = this;


    //Develop는 Tooltip을 달아둬서 마우스를 올렸을 때 글자가 뜨게 하는 기능 (테스터 모드 사용 가능 )22.05.02 승주
    [Header("Develop")]
    [SerializeField] [Tooltip("시작 턴 모드를 정합니다.")] ETurnMode eTurnMode;
    
    [SerializeField] [Tooltip("카드 배분이 매우 빨라집니다.")] bool fastMode;

    //card 추가 이벤트 기능 22.05.02 승주
    [SerializeField] [Tooltip("시작 카드 개수를 정합니다.")] int startCardCount;

    //일반적인 Enspector에 보이는 Properties 기능 22.05.02 승주
    [Header("Properties")]

    //game 끝나면 isLoading을 true로 하면 카드와 엔티티 클릭 방지 기능 22.05.02 승주
    public bool isLoading;

    public bool myTurn;

    //최초로 시작 할 때 누구 turn으로 할 지 정하는 기능 22.05.02 승주
    enum ETurnMode { Random, my, other }

    //card 추가 이벤트 기능 22.05.02 승주
    WaitForSeconds delay05 = new WaitForSeconds(0.5f);

    WaitForSeconds delay07 = new WaitForSeconds(0.7f);
    
    public static Action<bool> onAddCard;

    //trun 시작 이벤트 기능 22.05.04 승주
    public static event Action<bool> OnTurnStarted;


    //card 뽑는 순서 정하는 기능 22.05.02 승주
    void GameSetup()
    {
        //card 분배 속도 빠르게 하는 기능 22.05.02 승주
        if (fastMode)
            delay05 = new WaitForSeconds(0.05f);

        switch (eTurnMode)
        {
            case ETurnMode.Random:
                myTurn = Random.Range(0, 2) == 0;
                break;

            case ETurnMode.my:
                myTurn = true;
                break;

            case ETurnMode.other:
                myTurn = false;
                break;
        }

    }

    public IEnumerator StartGameCo()
    {
        GameSetup();
        isLoading = true;

        //상대 card 내 card 총 갯수를 배분 후 끝나는 기능 22.05.02 승주
        for(int i =0; i< startCardCount; i++)
        {
            yield return delay05;
            onAddCard?.Invoke(false);
            yield return delay05;
            onAddCard.Invoke(true);
        }
        StartCoroutine(StartTurnCo());

    }

    //turn이 시작 되는 기능 22.05.02 승주
    IEnumerator StartTurnCo()
    {
        isLoading = true;

        if (myTurn)
            CardGameManager.Inst.Notification("My Trun");

        //card 배분 기능 22.05.04 승주
        yield return delay07;
        //turn 차례에 card를 하나 추가시켜주는 기능 22.05.02 승주
        onAddCard?.Invoke(myTurn);

        yield return delay07;
        isLoading = false;


        OnTurnStarted?.Invoke(myTurn);
    }

    public void EndTurn()
    {
        myTurn = !myTurn;
        StartCoroutine(StartTurnCo());
    }
}
