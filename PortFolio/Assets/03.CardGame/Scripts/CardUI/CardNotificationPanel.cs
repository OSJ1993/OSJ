using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CardNotificationPanel : MonoBehaviour
{
    [SerializeField] TMP_Text notificationTMP;

    public void Show(string message)
    {
        notificationTMP.text = message;

        //DoTween 기능 22.05.02 승주
        //Sequence => 하나의 변환을 가진 Tween들을 시간과 순서에 맞춰 배열하여 연속된 하나의 장면을 구성 기능 22.05.02 승주
        Sequence sequence = DOTween.Sequence()

            //DoTween Ease.InOutQuad 느리다가 점점 빠르게 하는 기능 22.05.02 승주
            //DoTween Ease를 검색해보면 많은 기능이 있다 22.05.02 승주

            //Append Scale이 0에서 커졌다가
            .Append(transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutQuad))
            
            //0.9초 대기 한 후
            .AppendInterval(0.9f)
            
            //scale이 다시 작아지는 기능 22.05.02 승주
            .Append(transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InOutQuad));

    }

    
    void Start() => ScaleZero();

    // [ContextMenu] Inspector에서 우클릭으로 제어할 수 있는 기능 22.05.02 승주
    [ContextMenu("ScaleOne")]
    void ScaleOne() => transform.localScale = Vector3.one;

    
    [ContextMenu("ScaleZero")]
    public void ScaleZero() => transform.localScale = Vector3.zero;




    
}
