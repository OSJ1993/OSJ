using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //// 싱글톤 패턴을 사용하기 위한 인스턴스 변수
    //private static GameManager _instance;
    //// 인스턴스에 접근하기 위한 프로퍼티
    //public static GameManager Instance
    //{
    //    get
    //    {
    //        // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
    //        if (!_instance)
    //        {
    //            _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
    //
    //            if (_instance == null)
    //                Debug.Log("no Singleton obj");
    //        }
    //        return _instance;
    //    }
    //}
    //
    //public GameObject[] gameObjects;
    //public bool[] gameClears;
    //public bool[] gameFirstClear;
    //
    //private void Awake()
    //{
    //
    //
    //    if (_instance == null)
    //    {
    //        _instance = this;
    //    }
    //    // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
    //    else if (_instance != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //    // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
    //    DontDestroyOnLoad(gameObject);
    //
    //
    //    GameObject[] gameObjects = new GameObject[4];
    //    bool[] gameClears = new bool[4];
    //    bool[] gameFirstClear = new bool[4];
    //
    //    for (int i = 0; i < gameObjects.Length; i++)
    //    {
    //        if (gameClears[i] == false)
    //        {
    //            if (gameFirstClear[i] == false)
    //            {
    //            }
    //        }
    //    }
    //}
    //
    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        SceneManager.LoadScene(3);
    //        Debug.Log("나와라~~");
    //    }
    //    
    //}

    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;

    //상태 저장용 변수 기능 22.05.16 승주
    public bool isAction;


    public void Action(GameObject scanObj)
    {

        //UI 숨기기 &보여주기 구현 기능 22.05.16 승주
        if (isAction)
        {//Exit Action 기능 22.05.16 승주
            isAction = false;

        }
        else
        {//Enter Action 기능 22.05.16 승주
            isAction = true;
            scanObject = scanObj;
            talkText.text = "이것의 이름은 \n"  + scanObject.name + "이라고 한다..";
        }

        talkPanel.SetActive(isAction);
    }
}

