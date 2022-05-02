using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//PRS =position, rotation, Scale ��� Ŭ���� ��� 22.05.02 ����
//ī�� ���� ��ġ ��� 22.05.02 ����
[System.Serializable]
public class PRS
{
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;

    public PRS(Vector3 pos, Quaternion rot, Vector3 scale)
    {
        this.pos = pos;
        this.rot = rot;
        this.scale = scale;
    }
}

public class Utils 
{
    //utils�� Hierarchy�� ������ �ٸ� scripts ���� ���� �� �� �����ϰ� static���� ���� �� �� �ִ� utils Ŭ���� ��� 22.05.02 ����
    //Get�� �ǰ� �ϴ� ��� 22.05.02 ����
    public static Quaternion QI => Quaternion.identity;

    public static Vector3 MousePos
    {
        get
        {
            //Screen���� WorldPoint�� ��ȯ�ϴ� ��� 22.05.02 ����
            Vector3 result = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            result.z = -10;
            return result;
        }
    }
}
