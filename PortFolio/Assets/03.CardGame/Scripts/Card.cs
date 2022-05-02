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

    //item�� ���� �ְ� �ϰ� isForont�� itemī�尡 �� �޸��� Ȯ�� �ϴ� ��� 22.04.05 by����
    public void Setup(Item item, bool isForont)
    {
        this.item = item;
        this.isFront = isForont;

        //�ո��̶��  22.04.05 by����
        if (this.isFront)
        {
            character.sprite = this.item.sprite;
            nameTMP.text = this.item.name;
            attackTMP.text = this.item.attack.ToString();
            healthTMP.text = this.item.health.ToString();
        }
        //�޸��̶��  22.04.05 by����
        else
        {
            card.sprite = cardBack;
            nameTMP.text = "";
            attackTMP.text = "";
            healthTMP.text = "";

        }
    }

    //Mouse�� collider���� �ö� �ִ� ���¶�� Updateó�� ��� ȣ�� �ϴ� ��� 22.05.02 ����
    void OnMouseOver()
    {
        if (isFront)
            CardManager.Inst.CardMouseOver(this);
    }

    //Mouse�� Collider������ ������ ���¶�� CardMouseExit ����� ���� 22.05.02 ����
    void OnMouseExit()
    {
        if (isFront)
            CardManager.Inst.CardMouseExit(this);
    }

    //mouseDown�� mouse���� ������ ����(�� ������) ȣ�� ��� 22.05.02 ����
     void OnMouseDown()
    {
        if (isFront)
            CardManager.Inst.CardMouseDown();
    }

    //mouseUp�� mouse�� ���� ����(�� ������) ȣ�� �ϴ� ��� 22.05.02 ����
     void OnMouseUp()
    {
        if (isFront)
            CardManager.Inst.CardMouseUp();
    }


    //prs ������ �̵��� �ϴµ� useDotween�� true��� Dotween�� ����ؼ� �ε巴�� �����̰� useDotween�� false �׳� ��ġ�� �Ϲ������� �����̴� ��� ��� ��� 22.05.02 ����
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
