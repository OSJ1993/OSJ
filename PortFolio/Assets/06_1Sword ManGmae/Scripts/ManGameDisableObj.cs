using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManGameDisableObj : MonoBehaviour
{

    //�ش� Object�� Ȱ��ȭ �� ������ ����Ǵ� �Լ� ��� 22.04.29 ����
    private void OnEnable()
    {
        Invoke("DestroyObj", 0.5f);
    }

    //�ٽ� ��Ȱ��ȭ ��ų ��� 22.04.29 ����
    void DestroyObj()
    {
        gameObject.SetActive(false);
    }
}
