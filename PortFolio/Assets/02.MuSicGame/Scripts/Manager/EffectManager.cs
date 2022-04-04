using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EffectManager : MonoBehaviour
{
    [SerializeField] Animator noteHitAnimator = null;
    string hit = "Hit";

    [SerializeField] Animator judgementAnimator = null;

    //교체할 이미지 변수 선언.
    [SerializeField] UnityEngine.UI.Image judgementImage = null;

    //퍼팩트 쿨 굳 베드 담을 이미지 담기(배열).
    [SerializeField] Sprite[] judgementSprite = null;


   
    public void JudgementEffect(int p_num)
    {
        //피마리터 값에 맞는 판정 이미지 스프라이트 교체
        judgementImage.sprite = judgementSprite[p_num];

        judgementAnimator.SetTrigger(hit);
    }

    public void NoteHitEffect()
    {
        noteHitAnimator.SetTrigger(hit);
    }
}
