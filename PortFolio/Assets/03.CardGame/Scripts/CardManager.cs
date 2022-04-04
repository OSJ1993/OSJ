using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    //싱글턴으로 만든 이유 Manager는 하나만 존재하기 때문. 22.04.04 by승주
    public static CardManager Inst { get; private set; }
    void Awake() => Inst = this;

    //item을 가져오기 위해 Field로 선언 하고 인스팩트에 보이게 하는 기능 22.04.04 by승주
    [SerializeField] ItemSO itemSO;


    List<Item> itemBuffer;

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
        itemBuffer = new List<Item>();
        for (int i=0; i<itemSO.items.Length; i++)
        {
            //item 하나만 먼저 가져와서 각 itme의 퍼센트만큼 for문을 돌려서 itmeBuffer에 다 채워준다 퍼센트에 합이 100개니까 총 100개가 채워주는 기능  22.04.04 by승주
            Item item = itemSO.items[i];
            for (int j = 0; j < item.percent; j++)
                itemBuffer.Add(item);
        }

        //같은 카드가 연속으로 나오지 않게 Random으로 섞어주는 기능  22.04.04 by승주
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
        //숫자패드 1번을 누르면 popItem을 호출하여 아이템이 튕겨져 나오게 해주는 기능  22.04.04 by승주
        if (Input.GetKeyDown(KeyCode.Keypad1))
            print(popItem().name);
    }
}
