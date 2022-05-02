using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer card;
    [SerializeField] SpriteRenderer character;
    [SerializeField] TMP_Text nameTMP;
    [SerializeField] TMP_Text attackTMP;
    [SerializeField] TMP_Text healthTMP;
    [SerializeField] Sprite cardFront;
    [SerializeField] Sprite cardBack;

    public Item item;
    bool isFront;

    public PRS originPRS;

    //item을 먼저 넣게 하고 isForont로 item카드가 앞 뒷면을 확인 하는 기능 22.04.05 by승주
    public void Setup(Item item, bool isForont)
    {
        this.item = item;
        this.isFront = isForont;

        //앞면이라면  22.04.05 by승주
        if (this.isFront)
        {
            character.sprite = this.item.sprite;
            nameTMP.text = this.item.name;
            attackTMP.text = this.item.attack.ToString();
            healthTMP.text = this.item.health.ToString();
        }
        //뒷면이라면  22.04.05 by승주
        else
        {
            card.sprite = cardBack;
            nameTMP.text = "";
            attackTMP.text = "";
            healthTMP.text = "";

        }
    }

    //Mouse가 collider위에 올라가 있는 상태라면 Update처럼 계속 호출 하는 기능 22.05.02 승주
    void OnMouseOver()
    {
        if (isFront)
            CardManager.Inst.CardMouseOver(this);
    }

    //Mouse가 Collider위에서 내려간 상태라면 CardMouseExit 기능을 실행 22.05.02 승주
    void OnMouseExit()
    {
        if (isFront)
            CardManager.Inst.CardMouseExit(this);
    }

    //mouseDown는 mouse를딱 누르는 시점(한 프레임) 호출 기능 22.05.02 승주
     void OnMouseDown()
    {
        if (isFront)
            CardManager.Inst.CardMouseDown();
    }

    //mouseUp은 mouse를 떼는 시점(한 프레임) 호출 하는 기능 22.05.02 승주
     void OnMouseUp()
    {
        if (isFront)
            CardManager.Inst.CardMouseUp();
    }


    //prs 넣으면 이동을 하는데 useDotween가 true라면 Dotween을 사용해서 부드럽게 움직이고 useDotween가 false 그냥 위치로 일반적으로 움직이는 기능 라면 기능 22.05.02 승주
    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
        {
            
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }
}
