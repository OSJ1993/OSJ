using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    //이펙트가 항상 나오는 게 아닌 일정 콤보 이상부터 등장.
    [SerializeField] GameObject goComboImage = null;
    [SerializeField] Text txtCombo = null;

    //현재 몇 콤보인 지 나타낼 변수.
    int currentCombo = 0;

    Animator myAnim;
    string animComboUp = "ComboUp";


    void Start()
    {
        myAnim = GetComponent<Animator>();

        //처음부터 보여지지 않게 하기.
        txtCombo.gameObject.SetActive(false);
        goComboImage.SetActive(false);
    }


    //콤보 증가 함수.
    public void IncrcascCombo(int p_num = 1)
    {
        //파라미터를 넘기지 않으면 디폴트로 1을 삼겠다.
        currentCombo += p_num;

        //그렇게 해서 증가한 콤보 수를 텍스트로 표현 3자리마다 ,(콤마)를 찍어주겠다.
        txtCombo.text = string.Format("{0:,##0}", currentCombo);

        //콤보 텍스트, 콤보 이미지는 3콤보 이상부터 등장하도록 조건문 작성.
        if (currentCombo > 2)
        {
            txtCombo.gameObject.SetActive(true);
            goComboImage.SetActive(true);

            myAnim.SetTrigger(animComboUp);
        }

    }

    //현재 콤보를 알 수 있게.
    public int GetCurrentCombo()
    {
        return currentCombo;
    }


    //콤보 Reset
    public void ResetCombo()
    {
        currentCombo = 0;
        txtCombo.text = "0";
        txtCombo.gameObject.SetActive(false);
        goComboImage.SetActive(false);
    }
}
