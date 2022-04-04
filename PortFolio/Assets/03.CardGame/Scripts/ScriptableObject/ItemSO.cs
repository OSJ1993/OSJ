using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Item
{
    //이름 22.04.04 by승주
    public string name;

    //공격 22.04.04 by승주
    public int attack;

    //체력 22.04.04 by승주
    public int health;

    //스프라이트 22.04.04 by승주
    public Sprite sprite;

    //카드에서 뽑힐 확률 퍼센트 22.04.04 by승주
    public float percent;
}

//유니티 ScriptableObject
[CreateAssetMenu(fileName = "ItemSo", menuName = "Scriptable Object/ItemSO")]
public class ItemSO : ScriptableObject
{
    // Class Item을 담을 배열 22.04.04 by승주
    public Item[] items;
}
