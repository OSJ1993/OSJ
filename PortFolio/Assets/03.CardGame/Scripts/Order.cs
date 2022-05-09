using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    // 뒷쪽에 있는 Renderer들을 가져와 주는 기능 22.04.04 by승주
    [SerializeField] Renderer[] backRenderers;

    // 중앙에 있는 Renderer들을 가져와 주는 기능 22.04.04 by승주
    [SerializeField] Renderer[] midleRenderers;

    //sortingLayerName으로 sortingLayerName 해주는 기능(레이어 이름 정렬) 22.04.04 by승주
    [SerializeField] string sortingLayerName;

    int originOrder;

    //카드가 확대 되면 맨 앞으로 오게 하는 기능 22.04.04 by승주
    public void SetOriginOrder(int originOrder)
    {
        this.originOrder = originOrder;
        SetOrder(originOrder);
    }


    //맨 앞으로 올 때는 SetOrder를 isMostFront가 true라면 100 false라면 originOrder를 하기 위한 기능 22.04.04 by승주
    public void SetMostFrontOrder(bool isMostFront)
    {
        SetOrder(isMostFront ? 100 : originOrder);
    }

   


    //public으로 외부에서 order만 입력 하면 22.04.04 by승주
    public void SetOrder(int order)
    {
        
        // order에 10을 곱한다 카드가 0,1,2가 되면 겹치기 때문에 순서를 10정도 간격을 띄어주는 기능 22.04.04 by승주
        int mulOrder = order * 10;

        //맨 뒷쪽을 sortingLayerName과 곱해지는 order를 mulOrder에 대입 해주는 기능 22.04.04 by승주
        foreach (var renderer in backRenderers)
        {
            
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = mulOrder;
            
        }

        //맨 뒤 보다 앞쪽은 sortingLayerName은 똑같지만 mulOrder에 플러스 1을 해서 위에 foreach문 보다 한칸 더 앞에 보이게 하는 기능 22.04.04 by승주 
        foreach (var renderer in midleRenderers)
        {
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = mulOrder + 1;
        }
    }
}
