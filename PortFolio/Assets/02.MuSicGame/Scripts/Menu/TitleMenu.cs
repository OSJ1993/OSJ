using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    [SerializeField] GameObject goStageUI = null;

    public GameObject back;
    
    public void BtnPlay()
    {
        goStageUI.SetActive(true);
        this.gameObject.SetActive(false);

        back.SetActive(false);

        //Result result = GetComponent<Result>();
        //result =
    }

    public void BtnReStart()
    {
        goStageUI.SetActive(true);
        this.gameObject.SetActive(false);

        
    }
}
