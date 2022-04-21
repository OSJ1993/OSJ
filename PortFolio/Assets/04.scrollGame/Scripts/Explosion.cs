using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    //���� ������Ʈ�� ������ ��Ȱ��ȸ �ǵ��� ���ִ� ��� 22.04.21 by����
     void OnEnable()
    {
        Invoke("Disable", 2f);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    public void StartExplosion(string target)
    {
        anim.SetTrigger("OnExplosion");

        //��Ȱ��ȭ �Ǵ� ����� ũ�⿡ ���� ������ ��ȭ �ִ� ��� 22.04.21 by����
        switch (target)
        {


            case "S":
                transform.localScale = Vector3.one * 0.7f;
                break;

            case "M":
            case "P":
                transform.localScale = Vector3.one * 1f;
                break;

            case "L":
                transform.localScale = Vector3.one * 2f;
                break;

            case "B":
                transform.localScale = Vector3.one * 3f;
                break;

        }
    }
}
