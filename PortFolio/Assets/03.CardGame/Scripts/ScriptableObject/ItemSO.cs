using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Item
{
    //�̸� 22.04.04 by����
    public string name;

    //���� 22.04.04 by����
    public int attack;

    //ü�� 22.04.04 by����
    public int health;

    //��������Ʈ 22.04.04 by����
    public Sprite sprite;

    //ī�忡�� ���� Ȯ�� �ۼ�Ʈ 22.04.04 by����
    public float percent;
}

//����Ƽ ScriptableObject
[CreateAssetMenu(fileName = "ItemSo", menuName = "Scriptable Object/ItemSO")]
public class ItemSO : ScriptableObject
{
    // Class Item�� ���� �迭 22.04.04 by����
    public Item[] items;
}
