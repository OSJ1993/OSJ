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
        for (int i=0; i<itemSO.items.Length; i++)
        {
            //item �ϳ��� ���� �����ͼ� �� itme�� �ۼ�Ʈ��ŭ for���� ������ itmeBuffer�� �� ä���ش� �ۼ�Ʈ�� ���� 100���ϱ� �� 100���� ä���ִ� ���  22.04.04 by����
            Item item = itemSO.items[i];
            for (int j = 0; j < item.percent; j++)
                itemBuffer.Add(item);
        }

        //���� ī�尡 �������� ������ �ʰ� Random���� �����ִ� ���  22.04.04 by����
        for (int i=0; i<itemBuffer.Count; i++)
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
            print(popItem().name);
    }
}
