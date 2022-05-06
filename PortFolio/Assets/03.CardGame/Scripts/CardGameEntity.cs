using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CardGameEntity : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] SpriteRenderer entity;
    [SerializeField] SpriteRenderer characher;
    [SerializeField] TMP_Text nameTMP;
    [SerializeField] TMP_Text attackTMP;
    [SerializeField] TMP_Text healthTMP;
    [SerializeField] GameObject sleepParticle;

    public int attack;
    public int health;


    //�ڱ� �ڽ����� �ľ��ϴ� ��� 22.05.03 ����
    public bool isMine;

    //boss���� �ľ��ϴ� ��� 22.05.03 ����
    public bool isBossOrEmpty;

    //���� �� �ִ� �������� Ȯ�� �ϴ� ��� 22.05.06 ����
    public bool attackable;

    //??(���� ����??) ������ ���� ��� 22.05.04 ����
    public Vector3 originPos;

    int liveCount;

     void Start()
    {
        CardTrunManager.OnTurnStarted += OnTurnStated;
    }

     void OnDestroy()
    {
        CardTrunManager.OnTurnStarted -= OnTurnStated;
    }

    //myTurn�� �� 
    void OnTurnStated(bool myTurn)
    {
        //isBossOrEmpty��� return �����ְ� 
        if (isBossOrEmpty)
            return;

        //myTrun�� �� ���� ���� ���� liveCount(������ Count)�� ���������ִ� ��� 22.05.04 ����
        if (isMine == myTurn)
            liveCount++;

        //sleepParticle�� SetActive�� liveCount�� 0�̶�� ���ڴ� ��� ��� 22.05.04 ����
        sleepParticle.SetActive(liveCount < 1);

    }

    public void Setup(Item item)
    {
        attack = item.attack;
        health = item.health;

        this.item = item;
        characher.sprite = this.item.sprite;
        nameTMP.text = this.item.name;
        attackTMP.text = attack.ToString();
        healthTMP.text = health.ToString();
    }

    void OnMouseDown()
    {
        if (isMine)
            CardGameEntityManager.Inst.EntityMouseDown(this);
    }

    void OnMouseUp()
    {
        if (isMine)
            CardGameEntityManager.Inst.EntityMouseUp();
    }

    void OnMouseDrag()
    {
        if (isMine)
            CardGameEntityManager.Inst.EntityMouseDrag();
    }

    //pos�� ���ϴ� ��ġ useDotween�� ����ϸ� Dotween�� ����ؼ� dotweenTime ��ŭ �̵� �ϴ� ��� 22.05.04 ����
    public void MoveTransform(Vector3 pos, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
            transform.DOMove(pos, dotweenTime);

        //false��� �׳� ����ġ(??)�� �����̵� �ϴ� ��� 22.05.04 ����
        else
            transform.position = pos;
    }
}
