using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CardGameDamage : MonoBehaviour
{
    [SerializeField] TMP_Text damageTMP;
    Transform tr;

    //Setuptransform을 할 때 어떤 위치에 spawn을 해야하는 지 정하는 기능 22.05.09 승주
    public void SetupTransform(Transform tr)
    {
        this.tr = tr;
    }

    void Update()
    {
        //Entiy가 죽어서 사라지면 위치를 찾을 수 없기 때문에 null이 아닐 때는 현재 위치를 tr에 position으로 넣는 기능 22.05.09 승주 
        if (tr != null)
            transform.position = tr.position;
    }

    public void Damaged(int damage)
    {
        //damage가 0보다 작거나 같으면 return으로 표시 안 하는 기능 22.05.09 승주
        if (damage <= 0)
            return;

        //damage 받을 때 나오는sprite를 제일 앞으로 해서 무조건 제일 앞에 보일 수 있게 해주는 기능 22.05.09 승주
        GetComponent<Order>().SetOrder(1000);

        //damage를 받으면 $"-{damage}"로 얼만큼 damage가 갔는 지 알 수 있게 해주는 text 기능 22.05.09 승주
        damageTMP.text = $"-{damage}";


        //damage 받았을 때 sprite 크기 조절 기능 22.05.09 승주
        Sequence sequence = DOTween.Sequence()
            .Append(transform.DOScale(Vector3.one * 1.8f, 0.5f).SetEase(Ease.InOutBack))
            .AppendInterval(1.2f)
            .Append(transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutBack))
            .OnComplete(() => Destroy(gameObject));
    }
}
