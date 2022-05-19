using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //CanvasGroup 컴포넌트를 저장할 변수 기능 22.05.19 승주
    public CanvasGroup fadeCg;

    //Fade in 처리 시간 기능 22.05.19 승주
    [Range(0.5f, 2.0f)]
    public float fadeDuration = 1.0f;

    //호출할 씬과 씬 로드 방식을 저장할 딕셔너리 기능 22.05.19 승주
    public Dictionary<string, LoadSceneMode> loadScenes = new Dictionary<string, LoadSceneMode>();

    //호출할 씬의 정보 설정 기능 22.05.19 승주
    void InitSceneInfo()
    {
        //호출한 씬의 정보를 딕셔너리에 추가 기능 22.05.19 승주
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

        //처음 알파값을 설정(불투명)기능 22.05.19 승주
        fadeCg.alpha = 1.0f;

        //여러개의 씬을 코루틴으로 호출 하는 기능 22.05.19 승주
        foreach (var _loadScene in loadScenes)
        {
            yield return StartCoroutine(LoadScene(_loadScene.Key, _loadScene.Value));
        }

        //Fade In 함수 호출 기능 22.05.19 승주
        StartCoroutine(Fade(0.0f));

    }

    IEnumerator LoadScene(string sceneName, LoadSceneMode mode)
    {
        //비동기 방식으로 씬을 로드하고 로드가 완료될 때까지 대기하는 기능 22.05.19 승주
        yield return SceneManager.LoadSceneAsync(sceneName, mode);

        //호출된 씬을 활성화 시키는 기능 22.05.19 승주
        Scene loadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(loadedScene);
    }

    //Fade In/Out 시키는 기능 22.05.19 승주
    IEnumerator Fade(float finalAlpha)
    {
        //라이트맵이 깨지는 것을 방지하기 위해 스테이지 씬을 활성화 시키는  기능 22.05.19 승주
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("00.0Intro"));
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("01-1.Daily"));
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("01.MusicGame"));
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("02.CardGame"));
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("03.scrollGame"));
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("04Sword ManGmae"));
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("05.Ending"));
        fadeCg.blocksRaycasts = true;

        //절대값 함수로 백분율을 계산 하는 기능 22.05.19 승주
        float fadeSpeed = Mathf.Abs(fadeCg.alpha - finalAlpha) / fadeDuration;

        //알파값 조정 기능 22.05.19 승주
        while (!Mathf.Approximately(fadeCg.alpha, finalAlpha))
        {
            //MoveToward 함수는 Lerp 함수와 동일한 함수로 알파값으로 보간 하는 기능 22.05.19 승주
            fadeCg.alpha = Mathf.MoveTowards(fadeCg.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
            yield return null;

        }

        fadeCg.blocksRaycasts = false;

        //fade Inㅇ 완료된 후 SceneLoader 씬은 삭제(Unload)시키는 기능 22.05.19 승주
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
