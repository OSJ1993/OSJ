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

        //DoTween ��� 22.05.02 ����
        //Sequence => �ϳ��� ��ȯ�� ���� Tween���� �ð��� ������ ���� �迭�Ͽ� ���ӵ� �ϳ��� ����� ���� ��� 22.05.02 ����
        Sequence sequence = DOTween.Sequence()

            //DoTween Ease.InOutQuad �����ٰ� ���� ������ �ϴ� ��� 22.05.02 ����
            //DoTween Ease�� �˻��غ��� ���� ����� �ִ� 22.05.02 ����

            //Append Scale�� 0���� Ŀ���ٰ�
            .Append(transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutQuad))
            
            //0.9�� ��� �� ��
            .AppendInterval(0.9f)
            
            //scale�� �ٽ� �۾����� ��� 22.05.02 ����
            .Append(transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InOutQuad));

    }

    
    void Start() => ScaleZero();

    // [ContextMenu] Inspector���� ��Ŭ������ ������ �� �ִ� ��� 22.05.02 ����
    [ContextMenu("ScaleOne")]
    void ScaleOne() => transform.localScale = Vector3.one;

    
    [ContextMenu("ScaleZero")]
    public void ScaleZero() => transform.localScale = Vector3.zero;




    
}
