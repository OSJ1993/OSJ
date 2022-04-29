using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManGameDisableObj : MonoBehaviour
{

    //해당 Object가 활성화 될 때마다 실행되는 함수 기능 22.04.29 승주
    private void OnEnable()
    {
        Invoke("DestroyObj", 0.5f);
    }

    //다시 비활성화 시킬 기능 22.04.29 승주
    void DestroyObj()
    {
        gameObject.SetActive(false);
    }
}
