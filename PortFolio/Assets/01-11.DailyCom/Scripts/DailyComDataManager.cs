using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameChoice
{
    MusicGame, CardGame, scrollGame, SwordManGmae
}

public class DailyComDataManager : MonoBehaviour
{
    public static DailyComDataManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;
    }
}
