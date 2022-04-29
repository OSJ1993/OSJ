using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ManGameHitManager : MonoBehaviour
{
    public static ManGameHitManager instance;

    public TextMeshProUGUI[] dmgText;
    public Camera cam;

    Vector3 upPosition = new Vector3(0, 2, 0);

    private void Awake()
    {
        //�̱��� ���� ��� 22.04.29 ����
        if (instance == null) instance = this;
    }

    //damageText���� ���� ��� 22.04.29 ����
    public void DmgTextOn(Vector3 hitPosition,int dmg)
    {
        for (int i = 0; i < dmgText.Length; i++)
        {
            //i��° damageText�� �������� �ʴٸ� �̹� ���� �ִ� text�� pass�ϰ� �� ���� �� �����ִ� damgeText�� �ѱ� ���� ��� 22.04.29 ����
            if (!dmgText[i].gameObject.activeSelf)
            {
                //�� ���� �ִ� Text�� ��ġ�� hitPosition �̴� ��� 22.04.29 ����
                //Camera.WorldToScreenPotin(A) => ���� ��ǥ�� A�� ȭ�� ��ǥ�� ����
                dmgText[i].transform.position = cam.WorldToScreenPoint(hitPosition+ upPosition);

                //�� �����ִ� damageText�� Text�� ������ �������� ������ Damage�� ��� ��� 22.04.29 ����
                dmgText[i].text = dmg.ToString();
                dmgText[i].gameObject.SetActive(true);

                //���̳����� ȿ���� ���ؼ� Damage�� ���� fontSize ��ȭ ��Ű�� ��� 22.04.29 ����
                dmgText[i].fontSize = dmg * 0.5f;

                //text�ϳ��� ������ �ٸ� text�� ������ �ʰ� �ϴ� ��� 22.04.29
                return;
                

            }
        }
    }
}
