using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //CanvasGroup ������Ʈ�� ������ ���� ��� 22.05.19 ����
    public CanvasGroup fadeCg;

    //Fade in ó�� �ð� ��� 22.05.19 ����
    [Range(0.5f, 2.0f)]
    public float fadeDuration = 1.0f;

    //ȣ���� ���� �� �ε� ����� ������ ��ųʸ� ��� 22.05.19 ����
    public Dictionary<string, LoadSceneMode> loadScenes = new Dictionary<string, LoadSceneMode>();

    //ȣ���� ���� ���� ���� ��� 22.05.19 ����
    void InitSceneInfo()
    {
        //ȣ���� ���� ������ ��ųʸ��� �߰� ��� 22.05.19 ����
        loadScenes.Add("00.0Intro", LoadSceneMode.Additive);
        loadScenes.Add("01-1.Daily", LoadSceneMode.Additive);
        //loadScenes.Add("01.MusicGame", LoadSceneMode.Additive);
        //loadScenes.Add("02.CardGame", LoadSceneMode.Additive);
        //loadScenes.Add("03.scrollGame", LoadSceneMode.Additive);
        //loadScenes.Add("04Sword ManGmae", LoadSceneMode.Additive);
        //loadScenes.Add("05.Ending", LoadSceneMode.Additive);
    }

    IEnumerator Start()
    {
        InitSceneInfo();

        //ó�� ���İ��� ����(������)��� 22.05.19 ����
        fadeCg.alpha = 1.0f;

        //�������� ���� �ڷ�ƾ���� ȣ�� �ϴ� ��� 22.05.19 ����
        foreach (var _loadScene in loadScenes)
        {
            yield return StartCoroutine(LoadScene(_loadScene.Key, _loadScene.Value));
        }

        //Fade In �Լ� ȣ�� ��� 22.05.19 ����
        StartCoroutine(Fade(0.0f));

    }

    IEnumerator LoadScene(string sceneName, LoadSceneMode mode)
    {
        //�񵿱� ������� ���� �ε��ϰ� �ε尡 �Ϸ�� ������ ����ϴ� ��� 22.05.19 ����
        yield return SceneManager.LoadSceneAsync(sceneName, mode);

        //ȣ��� ���� Ȱ��ȭ ��Ű�� ��� 22.05.19 ����
        Scene loadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(loadedScene);
    }

    //Fade In/Out ��Ű�� ��� 22.05.19 ����
    IEnumerator Fade(float finalAlpha)
    {
        //����Ʈ���� ������ ���� �����ϱ� ���� �������� ���� Ȱ��ȭ ��Ű��  ��� 22.05.19 ����
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("00.0Intro"));
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("01-1.Daily"));
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("01.MusicGame"));
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("02.CardGame"));
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("03.scrollGame"));
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("04Sword ManGmae"));
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("05.Ending"));
        fadeCg.blocksRaycasts = true;

        //���밪 �Լ��� ������� ��� �ϴ� ��� 22.05.19 ����
        float fadeSpeed = Mathf.Abs(fadeCg.alpha - finalAlpha) / fadeDuration;

        //���İ� ���� ��� 22.05.19 ����
        while (!Mathf.Approximately(fadeCg.alpha, finalAlpha))
        {
            //MoveToward �Լ��� Lerp �Լ��� ������ �Լ��� ���İ����� ���� �ϴ� ��� 22.05.19 ����
            fadeCg.alpha = Mathf.MoveTowards(fadeCg.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
            yield return null;

        }

        fadeCg.blocksRaycasts = false;

        //fade In�� �Ϸ�� �� SceneLoader ���� ����(Unload)��Ű�� ��� 22.05.19 ����
        SceneManager.UnloadSceneAsync("00-1.SceneLoader");
        //SceneManager.UnloadSceneAsync("01-1.Daily");
        //SceneManager.UnloadSceneAsync("01.MusicGame");
        //SceneManager.UnloadSceneAsync("02.CardGame");
        //SceneManager.UnloadSceneAsync("03.scrollGame");
        //SceneManager.UnloadSceneAsync("04Sword ManGmae");
        //SceneManager.UnloadSceneAsync("05.Ending");
        

    }



    void Update()
    {

    }
}
