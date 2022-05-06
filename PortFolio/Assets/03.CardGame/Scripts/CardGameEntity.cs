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


    //자기 자신인지 파악하는 기능 22.05.03 승주
    public bool isMine;

    //boss인지 파악하는 기능 22.05.03 승주
    public bool isBossOrEmpty;

    //떄릴 수 있는 상태인지 확인 하는 기능 22.05.06 승주
    public bool attackable;

    //??(무슨 정렬??) 정렬을 위한 기능 22.05.04 승주
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

    //myTurn일 때 
    void OnTurnStated(bool myTurn)
    {
        //isBossOrEmpty라면 return 시켜주고 
        if (isBossOrEmpty)
            return;

        //myTrun이 막 돌아 왔을 때는 liveCount(생존한 Count)를 증가시켜주는 기능 22.05.04 승주
        if (isMine == myTurn)
            liveCount++;

        //sleepParticle에 SetActive의 liveCount가 0이라면 잠자는 모습 기능 22.05.04 승주
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

    //pos에 원하는 위치 useDotween를 사용하면 Dotween를 사용해서 dotweenTime 만큼 이동 하는 기능 22.05.04 승주
    public void MoveTransform(Vector3 pos, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
            transform.DOMove(pos, dotweenTime);

        //false라면 그냥 그위치(??)로 순간이동 하는 기능 22.05.04 승주
        else
            transform.position = pos;
    }
}
