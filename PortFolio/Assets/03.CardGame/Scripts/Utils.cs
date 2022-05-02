using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//PRS =position, rotation, Scale 담는 클래스 기능 22.05.02 승주
//카드 원본 위치 기능 22.05.02 승주
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
    //utils는 Hierarchy는 없지만 다른 scripts 에서 접근 할 때 간단하게 static으로 접근 할 수 있는 utils 클래스 기능 22.05.02 승주
    //Get만 되게 하는 기능 22.05.02 승주
    public static Quaternion QI => Quaternion.identity;

    public static Vector3 MousePos
    {
        get
        {
            //Screen에서 WorldPoint로 변환하는 기능 22.05.02 승주
            Vector3 result = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            result.z = -10;
            return result;
        }
    }
}
