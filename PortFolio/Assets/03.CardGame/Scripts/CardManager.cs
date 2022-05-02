using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    //싱글턴으로 만든 이유 Manager는 하나만 존재하기 때문. 22.04.04 by승주
    public static CardManager Inst { get; private set; }
    void Awake() => Inst = this;

    //item을 가져오기 위해 Field로 선언 하고 인스팩트에 보이게 하는 기능 22.04.04 by승주
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

    //어떨 때 card를 올리고 어떨 때 drag를 해야 하는 지 정하는 상태머신 기능 22.05.02 승주
    enum ECardState { Nothing, CanMouseOver,CanMouseDrag}

    //itemBuffer에서 item을 하나씩 뽑아서 올 수 있게 해주는 기능 22.04.04 by승주
    //우리 또는 적이 카드를 뽑을 때마다
    public Item popItem()
    {
        if (itemBuffer.Count == 0)
            SetupItemBuffer();

        //맨 앞에 있는 index를 뽑아서 
        Item item = itemBuffer[0];

        //RemoveAt(0)번 해서
        itemBuffer.RemoveAt(0);

        //return item을 뽑아 내게 해주는 기능  22.04.04 by승주
        return item;
    }

    void SetupItemBuffer()
    {
        //itemBuffer 껍데기를 만들어 주고 itemSo에items[배열 10개 담아져 있음] 길이 10개 이고  22.04.04 by승주
        itemBuffer = new List<Item>(100);
        for (int i = 0; i < itemSO.items.Length; i++)
        {
            //item 하나만 먼저 가져와서 각 itme의 퍼센트만큼 for문을 돌려서 itmeBuffer에 다 채워준다 퍼센트에 합이 100개니까 총 100개가 채워주는 기능  22.04.04 by승주
            Item item = itemSO.items[i];
            for (int j = 0; j < item.percent; j++)
                itemBuffer.Add(item);
        }

        //같은 카드가 연속으로 나오지 않게 Random으로 섞어주는 기능  22.04.04 by승주
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

        //card추가 기능 22.05.02 승주
        CardTrunManager.onAddCard += AddCard;
    }

    void OnDestroy()
    {
        //card 제거 기능 22.05.02 승주
        CardTrunManager.onAddCard -= AddCard;
    }

    void Update()
    {
        if (isMyCardDrag)
            cardDrag();

        DetectCardArea();
        SetEcardState();
    }

    



    //MyPlayer와 OtherPlayer가 뽑은 카드가 다르기 때문에 MyPlayer면 앞면 OtherPlayer 뒷면 나오게 하는 기능 22.04.05 by승주
    void AddCard(bool isMine)
    {
        var cardObject = Instantiate(cardPrefab, cardSpawnPoint.position, Utils.QI);
        var card = cardObject.GetComponent<Card>();
        card.Setup(popItem(), isMine);

        //isMine이 true라면 myCards에 Add를 isMine이 false라면 otherCards에 Add를 해주는 기능 (같은 카드 List이기 떄문에 가능함)  22.04.06 by승주
        (isMine ? myCards : otherCards).Add(card);


        SetOriginOrder(isMine);
        CardAlignment(isMine);
    }

    //isMine에 따라서 Count를 함 22.04.06 by승주
    void SetOriginOrder(bool isMine)
    {

        int count = isMine ? myCards.Count : otherCards.Count;

        //for문을 통해 myCard인지 otherCard인지 알려면 targetCard를 알아야 하기 때문에  22.04.06 by승주
        //targetCard가 존재를 한다면 Order까지 접근을 해서 생성이 됬을 때 SetOriginOrder까지 가져오는 기능  22.04.06 by승주
        //SetOriginOrder 불러오면 originOrder가 채워지게 되고 SetOrder를 통해서 정렬이 되는 기능  22.04.06 by승주
        for (int i = 0; i < count; i++)
        {


            var targetCard = isMine ? myCards[i] : otherCards[i];
            targetCard?.GetComponent<Order>().SetOriginOrder(i);
        }
    }

    //DoTween으로 카드 움직이는 기능 22.05.02 승주
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

            //card 크기 지정 기능 22.05.02 승주
            targetCard.originPRS = originCardPRSs[i];
            targetCard.MoveTransform(targetCard.originPRS, true, 1.3f);
        }
    }

    //card를 둥글게 정렬 하는 기능 22.05.02 승주
    List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float helght, Vector3 scale)
    {
        //0부터 1까지 숫자 중에서 자기 현재 위치가 어느 정도 되어야 하는 지 정 하는 기능 22.05.02 승주
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

        //원의 방정식 기능 22.05.02 승주
        for (int i = 0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            var targetRot = Utils.QI;

            //card가 4장 이상이면 회전 시키는 기능 22.05.02 승주
            if (objCount >= 4)
            {
                //제곱을 하기 때문에 무조건 양수로 해야 한다 22.05.02 승주
                float curve = Mathf.Sqrt(Mathf.Pow(helght, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                curve = helght >= 0 ? curve : -curve;
                targetPos.y += curve;
                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
            }

            //card가 4장 미만이라면 회전 시키지 않고 바로 계산 되는 기능 22.05.02 승주
            results.Add(new PRS(targetPos, targetRot, scale));
        }
        return results;
    }

    //모든 card에 올리고 내리고 하는 정보를 gameManager에 넘겨주기 위한 기능 22.05.02 승주

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

    //card 드래그 기능 22.05.02 승주
    //card를 누르는 순간 기능 22.05.02 승주
    public void CardMouseDown()
    {
        if (eCardState != ECardState.CanMouseDrag)
            return;

        isMyCardDrag = true;
    }

    //card를 mouse를 누르있다가 떼는 순간 기능 22.05.02 승주
    public void CardMouseUp()
    {
        isMyCardDrag = false;

        if (eCardState != ECardState.CanMouseDrag)
            return;
    }

    //drag해서 card가 필드에 가 있다면 
    private void cardDrag()
    {
        if (!onMyCardArea)
        {
            //위치를 옮겨주는 기능 22.05.02 승주
            selectCard.MoveTransform(new PRS(Utils.MousePos, Utils.QI, selectCard.originPRS.scale), false);
        }
    }

    void DetectCardArea()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utils.MousePos, Vector3.forward);

        int layer = LayerMask.NameToLayer("MyCardArea");
        onMyCardArea = Array.Exists(hits, x => x.collider.gameObject.layer == layer);
    }

    //card 확대하는 기능 22.05.02 승주
    void EnlargeCard(bool isEnlarge, Card card)
    {
        
        if (isEnlarge)
        {
            //확대 했을 때 카드 위치 기능 22.05.02 승주
            Vector3 enlargePos = new Vector3(card.originPRS.pos.x-0.9f, -3.0f, -10f);

            //card에 마우스를 올렸을 때 card를 얼만큼 확대시킬 지 기능 22.05.02 승주
            card.MoveTransform(new PRS(enlargePos, Utils.QI, Vector3.one * 1.9f), false);
        }
        else
            card.MoveTransform(card.originPRS, false);

        card.GetComponent<Order>().SetMostFrontOrder(isEnlarge);
    }

     void SetEcardState()
    {
        //game이 시작 되기도 전에 mouse 올려도 실행 되지 않게 하는 기능 22.05.02 승주
        if (CardTrunManager.Inst.isLoading)
            eCardState = ECardState.Nothing;

        //mytrun이 아니라면 mouse만 올릴 수 있게 해주는 기능 22.05.02 승주
        else if (!CardTrunManager.Inst.myTurn)
            eCardState = ECardState.CanMouseOver;

        //mytrun일 동안에는 mouse를 drag할 수 있는 기능 22.05.02 승주
        else if (CardTrunManager.Inst.myTurn)
            eCardState = ECardState.CanMouseDrag;
    }
    #endregion

}
