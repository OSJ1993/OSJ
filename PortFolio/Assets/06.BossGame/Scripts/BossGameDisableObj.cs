using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGameDisableObj : MonoBehaviour
{
    //Object�� ��Ȱ��ȭ �Ǳ���� �ð� ��� 22.04.26 ����
    public float dTime;

    //Object�� Ȱ��ȭ �� �� Disable����� dTime�ڿ� ���� ��Ű�� ��� 22.04.26 ����
    private void OnEnable()
    {
        CancelInvoke();
        Invoke("Disable", dTime);
    }

    //Object�� ��Ȱ��ȭ ��Ű�� ��� 22.04.26 ����
    void Disable()
    {
        gameObject.SetActive(false);
    }
}
