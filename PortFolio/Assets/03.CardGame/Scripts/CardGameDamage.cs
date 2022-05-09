using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CardGameDamage : MonoBehaviour
{
    [SerializeField] TMP_Text damageTMP;
    Transform tr;

    //Setuptransform�� �� �� � ��ġ�� spawn�� �ؾ��ϴ� �� ���ϴ� ��� 22.05.09 ����
    public void SetupTransform(Transform tr)
    {
        this.tr = tr;
    }

    void Update()
    {
        //Entiy�� �׾ ������� ��ġ�� ã�� �� ���� ������ null�� �ƴ� ���� ���� ��ġ�� tr�� position���� �ִ� ��� 22.05.09 ���� 
        if (tr != null)
            transform.position = tr.position;
    }

    public void Damaged(int damage)
    {
        //damage�� 0���� �۰ų� ������ return���� ǥ�� �� �ϴ� ��� 22.05.09 ����
        if (damage <= 0)
            return;

        //damage ���� �� ������sprite�� ���� ������ �ؼ� ������ ���� �տ� ���� �� �ְ� ���ִ� ��� 22.05.09 ����
        GetComponent<Order>().SetOrder(1000);

        //damage�� ������ $"-{damage}"�� ��ŭ damage�� ���� �� �� �� �ְ� ���ִ� text ��� 22.05.09 ����
        damageTMP.text = $"-{damage}";


        //damage �޾��� �� sprite ũ�� ���� ��� 22.05.09 ����
        Sequence sequence = DOTween.Sequence()
            .Append(transform.DOScale(Vector3.one * 1.8f, 0.5f).SetEase(Ease.InOutBack))
            .AppendInterval(1.2f)
            .Append(transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutBack))
            .OnComplete(() => Destroy(gameObject));
    }
}
