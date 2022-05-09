using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameCameraEffect : MonoBehaviour
{
    [SerializeField] Material effectMat;

    //game중에 작동 하는 기능 22.05.09 승주
    void OnRenderImage(RenderTexture _src, RenderTexture _dest)
    {
        if (effectMat == null)
            return;

        //소스RenderTexture를 목적지가 effectMat를 넣으면 effectMat의 maintext가 목적지가 되서 목적지에 붙여 넣는 기능 22.05.09 승주
        Graphics.Blit(_src, _dest, effectMat);
    }

     void OnDestroy()
    {
        SetGrayScale(false);
    }

    public void SetGrayScale(bool isGrayscale)
    {
        effectMat.SetFloat("_GrayscaleAmount", isGrayscale ? 1 : 0);
        effectMat.SetFloat("_DarkAmount", isGrayscale ? 0.12f : 0);
    }
}
