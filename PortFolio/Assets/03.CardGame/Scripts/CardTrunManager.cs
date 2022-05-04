using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class CardTrunManager : MonoBehaviour
{
    //�̱��� ��� 22.05.02 ����
    public static CardTrunManager Inst { get; private set; }
    void Awake() => Inst = this;


    //Develop�� Tooltip�� �޾Ƶּ� ���콺�� �÷��� �� ���ڰ� �߰� �ϴ� ��� (�׽��� ��� ��� ���� )22.05.02 ����
    [Header("Develop")]
    [SerializeField] [Tooltip("���� �� ��带 ���մϴ�.")] ETurnMode eTurnMode;
    
    [SerializeField] [Tooltip("ī�� ����� �ſ� �������ϴ�.")] bool fastMode;

    //card �߰� �̺�Ʈ ��� 22.05.02 ����
    [SerializeField] [Tooltip("���� ī�� ������ ���մϴ�.")] int startCardCount;

    //�Ϲ����� Enspector�� ���̴� Properties ��� 22.05.02 ����
    [Header("Properties")]

    //game ������ isLoading�� true�� �ϸ� ī��� ��ƼƼ Ŭ�� ���� ��� 22.05.02 ����
    public bool isLoading;

    public bool myTurn;

    //���ʷ� ���� �� �� ���� turn���� �� �� ���ϴ� ��� 22.05.02 ����
    enum ETurnMode { Random, my, other }

    //card �߰� �̺�Ʈ ��� 22.05.02 ����
    WaitForSeconds delay05 = new WaitForSeconds(0.5f);

    WaitForSeconds delay07 = new WaitForSeconds(0.7f);
    
    public static Action<bool> onAddCard;

    //trun ���� �̺�Ʈ ��� 22.05.04 ����
    public static event Action<bool> OnTurnStarted;


    //card �̴� ���� ���ϴ� ��� 22.05.02 ����
    void GameSetup()
    {
        //card �й� �ӵ� ������ �ϴ� ��� 22.05.02 ����
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

        //��� card �� card �� ������ ��� �� ������ ��� 22.05.02 ����
        for(int i =0; i< startCardCount; i++)
        {
            yield return delay05;
            onAddCard?.Invoke(false);
            yield return delay05;
            onAddCard.Invoke(true);
        }
        StartCoroutine(StartTurnCo());

    }

    //turn�� ���� �Ǵ� ��� 22.05.02 ����
    IEnumerator StartTurnCo()
    {
        isLoading = true;

        if (myTurn)
            CardGameManager.Inst.Notification("My Trun");

        //card ��� ��� 22.05.04 ����
        yield return delay07;
        //turn ���ʿ� card�� �ϳ� �߰������ִ� ��� 22.05.02 ����
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
