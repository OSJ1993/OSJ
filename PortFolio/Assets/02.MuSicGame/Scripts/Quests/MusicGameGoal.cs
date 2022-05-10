using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGameGoal {

    //문자열 속성 설명 기능 22.05.10 승주
    //목표가 무엇인지 설명할 기능 22.05.10 승주
    public string Description { get; set; }

    public bool Completed { get; set; }

    public int CurrentAmount { get; set; }
    public int ReQuiredAmount { get; set; }

}