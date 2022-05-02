using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    //�̱������� ���� ���� Manager�� �ϳ��� �����ϱ� ����. 22.04.04 by����
    public static CardManager Inst { get; private set; }
    void Awake() => Inst = this;

    //item�� �������� ���� Field�� ���� �ϰ� �ν���Ʈ�� ���̰� �ϴ� ��� 22.04.04 by����
    [SerializeField] ItemSO itemSO;

    [SerializeField] GameObject cardPrefab;

    [SerializeField] List<Card> myCards;
    [SerializeField] List<Card> otherCards;

    [SerializeField] Transform cardSpawnPoint;
    [SerializeField] Transform myCardLeft;
    [SerializeField] Transform myCardRight;
    [SerializeField] Transform otherCardLeft;
    [SerializeField] Transform otherCardRight;
    [SerializeField] ECardState eCardState;


    List<Item> itemBuffer;

    Card selectCard;
    bool isMyCardDrag;
    bool onMyCardArea;

    //� �� card�� �ø��� � �� drag�� �ؾ� �ϴ� �� ���ϴ� ���¸ӽ� ��� 22.05.02 ����
    enum ECardState { Nothing, CanMouseOver,CanMouseDrag}

    //itemBuffer���� item�� �ϳ��� �̾Ƽ� �� �� �ְ� ���ִ� ��� 22.04.04 by����
    //�츮 �Ǵ� ���� ī�带 ���� ������
    public Item popItem()
    {
        if (itemBuffer.Count == 0)
            SetupItemBuffer();

        //�� �տ� �ִ� index�� �̾Ƽ� 
        Item item = itemBuffer[0];

        //RemoveAt(0)�� �ؼ�
        itemBuffer.RemoveAt(0);

        //return item�� �̾� ���� ���ִ� ���  22.04.04 by����
        return item;
    }

    void SetupItemBuffer()
    {
        //itemBuffer �����⸦ ����� �ְ� itemSo��items[�迭 10�� ����� ����] ���� 10�� �̰�  22.04.04 by����
        itemBuffer = new List<Item>(100);
        for (int i = 0; i < itemSO.items.Length; i++)
        {
            //item �ϳ��� ���� �����ͼ� �� itme�� �ۼ�Ʈ��ŭ for���� ������ itmeBuffer�� �� ä���ش� �ۼ�Ʈ�� ���� 100���ϱ� �� 100���� ä���ִ� ���  22.04.04 by����
            Item item = itemSO.items[i];
            for (int j = 0; j < item.percent; j++)
                itemBuffer.Add(item);
        }

        //���� ī�尡 �������� ������ �ʰ� Random���� �����ִ� ���  22.04.04 by����
        for (int i = 0; i < itemBuffer.Count; i++)
        {
            int rand = Random.Range(i, itemBuffer.Count);
            Item temp = itemBuffer[i];
            itemBuffer[i] = itemBuffer[rand];
            itemBuffer[rand] = temp;
        }
    }

    void Start()
    {
        SetupItemBuffer();

        //card�߰� ��� 22.05.02 ����
        CardTrunManager.onAddCard += AddCard;
    }

    void OnDestroy()
    {
        //card ���� ��� 22.05.02 ����
        CardTrunManager.onAddCard -= AddCard;
    }

    void Update()
    {
        if (isMyCardDrag)
            cardDrag();

        DetectCardArea();
        SetEcardState();
    }

    



    //MyPlayer�� OtherPlayer�� ���� ī�尡 �ٸ��� ������ MyPlayer�� �ո� OtherPlayer �޸� ������ �ϴ� ��� 22.04.05 by����
    void AddCard(bool isMine)
    {
        var cardObject = Instantiate(cardPrefab, cardSpawnPoint.position, Utils.QI);
        var card = cardObject.GetComponent<Card>();
        card.Setup(popItem(), isMine);

        //isMine�� true��� myCards�� Add�� isMine�� false��� otherCards�� Add�� ���ִ� ��� (���� ī�� List�̱� ������ ������)  22.04.06 by����
        (isMine ? myCards : otherCards).Add(card);


        SetOriginOrder(isMine);
        CardAlignment(isMine);
    }

    //isMine�� ���� Count�� �� 22.04.06 by����
    void SetOriginOrder(bool isMine)
    {

        int count = isMine ? myCards.Count : otherCards.Count;

        //for���� ���� myCard���� otherCard���� �˷��� targetCard�� �˾ƾ� �ϱ� ������  22.04.06 by����
        //targetCard�� ���縦 �Ѵٸ� Order���� ������ �ؼ� ������ ���� �� SetOriginOrder���� �������� ���  22.04.06 by����
        //SetOriginOrder �ҷ����� originOrder�� ä������ �ǰ� SetOrder�� ���ؼ� ������ �Ǵ� ���  22.04.06 by����
        for (int i = 0; i < count; i++)
        {


            var targetCard = isMine ? myCards[i] : otherCards[i];
            targetCard?.GetComponent<Order>().SetOriginOrder(i);
        }
    }

    //DoTween���� ī�� �����̴� ��� 22.05.02 ����
    void CardAlignment(bool isMine)
    {
        List<PRS> originCardPRSs = new List<PRS>();
        if (isMine)
            originCardPRSs = RoundAlignment(myCardLeft, myCardRight, myCards.Count, 0.5f, Vector3.one * 1.3f);
        else
            originCardPRSs = RoundAlignment(otherCardLeft, otherCardRight, otherCards.Count, -0.5f, Vector3.one * 1.3f);

        var targetCards = isMine ? myCards : otherCards;

        for (int i = 0; i < targetCards.Count; i++)
        {
            var targetCard = targetCards[i];

            //card ũ�� ���� ��� 22.05.02 ����
            targetCard.originPRS = originCardPRSs[i];
            targetCard.MoveTransform(targetCard.originPRS, true, 1.3f);
        }
    }

    //card�� �ձ۰� ���� �ϴ� ��� 22.05.02 ����
    List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float helght, Vector3 scale)
    {
        //0���� 1���� ���� �߿��� �ڱ� ���� ��ġ�� ��� ���� �Ǿ�� �ϴ� �� �� �ϴ� ��� 22.05.02 ����
        float[] objLerps = new float[objCount];

        List<PRS> results = new List<PRS>(objCount);

        switch (objCount)
        {
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2: objLerps = new float[] { 0.27f, 0.73f }; break;
            case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;

            default:
                float interval = 1f / (objCount - 1f);
                for (int i = 0; i < objCount; i++)
                    objLerps[i] = interval * i;
                break;
        }

        //���� ������ ��� 22.05.02 ����
        for (int i = 0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            var targetRot = Utils.QI;

            //card�� 4�� �̻��̸� ȸ�� ��Ű�� ��� 22.05.02 ����
            if (objCount >= 4)
            {
                //������ �ϱ� ������ ������ ����� �ؾ� �Ѵ� 22.05.02 ����
                float curve = Mathf.Sqrt(Mathf.Pow(helght, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                curve = helght >= 0 ? curve : -curve;
                targetPos.y += curve;
                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
            }

            //card�� 4�� �̸��̶�� ȸ�� ��Ű�� �ʰ� �ٷ� ��� �Ǵ� ��� 22.05.02 ����
            results.Add(new PRS(targetPos, targetRot, scale));
        }
        return results;
    }

    //��� card�� �ø��� ������ �ϴ� ������ gameManager�� �Ѱ��ֱ� ���� ��� 22.05.02 ����

    #region MyCard

    public void CardMouseOver(Card card)
    {
        if(eCardState == ECardState.Nothing)
            return;

        selectCard = card;
        EnlargeCard(true, card);
    }
    public void CardMouseExit(Card card)
    {
        EnlargeCard(false, card);
    }

    //card �巡�� ��� 22.05.02 ����
    //card�� ������ ���� ��� 22.05.02 ����
    public void CardMouseDown()
    {
        if (eCardState != ECardState.CanMouseDrag)
            return;

        isMyCardDrag = true;
    }

    //card�� mouse�� �����ִٰ� ���� ���� ��� 22.05.02 ����
    public void CardMouseUp()
    {
        isMyCardDrag = false;

        if (eCardState != ECardState.CanMouseDrag)
            return;
    }

    //drag�ؼ� card�� �ʵ忡 �� �ִٸ� 
    private void cardDrag()
    {
        if (!onMyCardArea)
        {
            //��ġ�� �Ű��ִ� ��� 22.05.02 ����
            selectCard.MoveTransform(new PRS(Utils.MousePos, Utils.QI, selectCard.originPRS.scale), false);
        }
    }

    void DetectCardArea()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utils.MousePos, Vector3.forward);

        int layer = LayerMask.NameToLayer("MyCardArea");
        onMyCardArea = Array.Exists(hits, x => x.collider.gameObject.layer == layer);
    }

    //card Ȯ���ϴ� ��� 22.05.02 ����
    void EnlargeCard(bool isEnlarge, Card card)
    {
        
        if (isEnlarge)
        {
            //Ȯ�� ���� �� ī�� ��ġ ��� 22.05.02 ����
            Vector3 enlargePos = new Vector3(card.originPRS.pos.x-0.9f, -3.0f, -10f);

            //card�� ���콺�� �÷��� �� card�� ��ŭ Ȯ���ų �� ��� 22.05.02 ����
            card.MoveTransform(new PRS(enlargePos, Utils.QI, Vector3.one * 1.9f), false);
        }
        else
            card.MoveTransform(card.originPRS, false);

        card.GetComponent<Order>().SetMostFrontOrder(isEnlarge);
    }

     void SetEcardState()
    {
        //game�� ���� �Ǳ⵵ ���� mouse �÷��� ���� ���� �ʰ� �ϴ� ��� 22.05.02 ����
        if (CardTrunManager.Inst.isLoading)
            eCardState = ECardState.Nothing;

        //mytrun�� �ƴ϶�� mouse�� �ø� �� �ְ� ���ִ� ��� 22.05.02 ����
        else if (!CardTrunManager.Inst.myTurn)
            eCardState = ECardState.CanMouseOver;

        //mytrun�� ���ȿ��� mouse�� drag�� �� �ִ� ��� 22.05.02 ����
        else if (CardTrunManager.Inst.myTurn)
            eCardState = ECardState.CanMouseDrag;
    }
    #endregion

}
