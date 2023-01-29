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

    public DailyTalkManager talkManager;
    public DailyQuestManager questManager;

    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public GameObject helpUI;

    //���� ����� ���� ��� 22.05.16 ����
    public bool isAction;

    public int talkIndex;

    public bool sceneChangeNPC = false;

    private void Start()
    {
        HelpCheck();
    }

    private void HelpCheck()
    {
        Debug.Log("HelpCkeck In!!!");
        // Ʃ�丮���� ���°� ����� ������ ���Ͽ�
        if (PlayerPrefs.GetInt("Tutorial_Start") == 0)
        {
            Debug.Log("����!!!");
            // ���� �����̶�� ���� UI ����
            helpUI.SetActive(true);
            PlayerPrefs.SetInt("Tutorial_Start", 1);

            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("���ʰ� �ƴϾ�!");
            helpUI.SetActive(false);
        }
    }

    public void Action(GameObject scanObj)
    {
        //UI ����� &�����ֱ� ���� ��� 22.05.16 ����

        //Enter Action ��� 22.05.16 ����
        scanObject = scanObj;
        DailyObjData objData = scanObject.GetComponent<DailyObjData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNPC)
    {
        //Set Talk Data ��� 22.05.17 ����
        int questTalkIndex = questManager.GetQusetTalkIndex(id);

        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);



        //Talk End ��� 22.05.17 ����
        if (talkData == null)
        {
            isAction = false;

            talkIndex = 0;
            if(sceneChangeNPC) StartCoroutine(SceneChage());
            return;
        }


        if (isNPC)
        {

            talkText.text = talkData;

        }
        else
        {
            talkText.text = talkData;

        }

        isAction = true;



        talkIndex++;
    }

    IEnumerator SceneChage()
    {
        yield return new WaitForSeconds(0.1f);
        yield return SceneManager.LoadSceneAsync("01-11.DailyCom");
    }
}

