using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGameDisableObj : MonoBehaviour
{
    //Object가 비활성화 되기까지 시간 기능 22.04.26 승주
    public float dTime;

    //Object가 활성화 될 때 Disable기능을 dTime뒤에 실행 시키는 기능 22.04.26 승주
    private void OnEnable()
    {
        CancelInvoke();
        Invoke("Disable", dTime);
    }

    //Object를 비활성화 시키는 기능 22.04.26 승주
    void Disable()
    {
        gameObject.SetActive(false);
    }
}
