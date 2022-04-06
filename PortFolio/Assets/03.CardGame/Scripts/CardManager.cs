using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    List<Item> itemBuffer;

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
        itemBuffer = new List<Item>();
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
    }

    void Update()
    {
        //�����е� 1���� ������ popItem�� ȣ���Ͽ� �������� ƨ���� ������ ���ִ� ���  22.04.04 by����
        if (Input.GetKeyDown(KeyCode.Keypad1))
            AddCard(true);

        if (Input.GetKeyDown(KeyCode.Keypad2))
            AddCard(false);

    }

    //MyPlayer�� OtherPlayer�� ���� ī�尡 �ٸ��� ������ MyPlayer�� �ո� OtherPlayer �޸� ������ �ϴ� ��� 22.04.05 by����
    void AddCard(bool isMine)
    {
        var cardObject = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
        var card = cardObject.GetComponent<Card>();
        card.Setup(popItem(), isMine);

        //isMine�� true��� myCards�� Add�� isMine�� false��� otherCards�� Add�� ���ִ� ��� (���� ī�� List�̱� ������ ������)  22.04.06 by����
        (isMine ? myCards : otherCards).Add(card);
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
}
