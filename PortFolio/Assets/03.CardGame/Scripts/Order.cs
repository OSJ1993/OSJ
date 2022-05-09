using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    // ���ʿ� �ִ� Renderer���� ������ �ִ� ��� 22.04.04 by����
    [SerializeField] Renderer[] backRenderers;

    // �߾ӿ� �ִ� Renderer���� ������ �ִ� ��� 22.04.04 by����
    [SerializeField] Renderer[] midleRenderers;

    //sortingLayerName���� sortingLayerName ���ִ� ���(���̾� �̸� ����) 22.04.04 by����
    [SerializeField] string sortingLayerName;

    int originOrder;

    //ī�尡 Ȯ�� �Ǹ� �� ������ ���� �ϴ� ��� 22.04.04 by����
    public void SetOriginOrder(int originOrder)
    {
        this.originOrder = originOrder;
        SetOrder(originOrder);
    }


    //�� ������ �� ���� SetOrder�� isMostFront�� true��� 100 false��� originOrder�� �ϱ� ���� ��� 22.04.04 by����
    public void SetMostFrontOrder(bool isMostFront)
    {
        SetOrder(isMostFront ? 100 : originOrder);
    }

   


    //public���� �ܺο��� order�� �Է� �ϸ� 22.04.04 by����
    public void SetOrder(int order)
    {
        
        // order�� 10�� ���Ѵ� ī�尡 0,1,2�� �Ǹ� ��ġ�� ������ ������ 10���� ������ ����ִ� ��� 22.04.04 by����
        int mulOrder = order * 10;

        //�� ������ sortingLayerName�� �������� order�� mulOrder�� ���� ���ִ� ��� 22.04.04 by����
        foreach (var renderer in backRenderers)
        {
            
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = mulOrder;
            
        }

        //�� �� ���� ������ sortingLayerName�� �Ȱ����� mulOrder�� �÷��� 1�� �ؼ� ���� foreach�� ���� ��ĭ �� �տ� ���̰� �ϴ� ��� 22.04.04 by���� 
        foreach (var renderer in midleRenderers)
        {
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = mulOrder + 1;
        }
    }
}
