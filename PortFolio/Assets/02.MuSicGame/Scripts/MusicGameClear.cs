using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicGameClear : MonoBehaviour
{
    //현재 스테이지 번호, 오픈한 스테이지 번호 기능 22.05.12 승주
    public GameObject stageNumObject;
    int leveat;

    void Start()
    {
        Button[] stages = stageNumObject.GetComponentsInChildren<Button>();

        leveat = PlayerPrefs.GetInt("levelReached");
        print(leveat);
        for(int i = leveat +1; i< stages.Length; i++)
        {
            stages[i].interactable = false;
        }
    }

    
    void Update()
    {
        
    }
}
