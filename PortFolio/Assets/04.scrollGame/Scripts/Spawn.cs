using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//구조체 22.04.13 by승주
public class Spawn 
{
    //spawn 시간 기능 22.04.13 by승주
    public float delay;

    // Enemy 비행기 타입 기능 22.04.13 by승주
    public string type;

    //spawn point 기능 22.04.13 by승주
    //ScrollGmaemanager가 배열[]로 관리하고 있다 22.04.13 by승주
    public int point;
}

//Enemy를 누구를 소환 할 지 정하는 기능 22.04.13 by승주
//C# Script가 아닌 일반 Text파일(메모장)로 관리한다. 22.04.13 by승주


