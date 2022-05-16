using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //// �̱��� ������ ����ϱ� ���� �ν��Ͻ� ����
    //private static GameManager _instance;
    //// �ν��Ͻ��� �����ϱ� ���� ������Ƽ
    //public static GameManager Instance
    //{
    //    get
    //    {
    //        // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
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
    //    // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
    //    else if (_instance != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //    // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.
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
    //        Debug.Log("���Ͷ�~~");
    //    }
    //    
    //}

    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;

    //���� ����� ���� ��� 22.05.16 ����
    public bool isAction;


    public void Action(GameObject scanObj)
    {

        //UI ����� &�����ֱ� ���� ��� 22.05.16 ����
        if (isAction)
        {//Exit Action ��� 22.05.16 ����
            isAction = false;

        }
        else
        {//Enter Action ��� 22.05.16 ����
            isAction = true;
            scanObject = scanObj;
            talkText.text = "�̰��� �̸��� \n"  + scanObject.name + "�̶�� �Ѵ�..";
        }

        talkPanel.SetActive(isAction);
    }
}

