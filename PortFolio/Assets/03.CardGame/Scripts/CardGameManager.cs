using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 치트, UI, 랭킹, 게임오버 기능 22.05.02 승주
public class CardGameManager : MonoBehaviour
{
    public static CardGameManager Inst { get; private set; }
    void Awake() => Inst = this;


    [Multiline(10)]
    [SerializeField] string cheatInfo;

    [SerializeField] CardNotificationPanel notificationPanel;

    [SerializeField] CardGameResultPanel resultPanel;

    [SerializeField] CardGameTitlePanel titlePanel;
    [SerializeField] CardGameCameraEffect cameraEffect;

    [SerializeField] GameObject endTurnBtn;

    WaitForSeconds delay2 = new WaitForSeconds(2);

    void Start()
    {
        UISetup();
    }

    void UISetup()
    {
        notificationPanel.ScaleZero();
        resultPanel.ScaleZero();
        titlePanel.Active(true);
        cameraEffect.SetGrayScale(false);
    }




    void Update()
    {
#if UNITY_EDITOR
        InputCheatKey();
#endif
    }

    void InputCheatKey()
    {
        //my card 얻기 기능22.05.02 승주
        if (Input.GetKeyDown(KeyCode.Keypad1))
            CardGameTrunManager.onAddCard?.Invoke(true);

        //other card 얻기 기능 22.05.02 승주
        if (Input.GetKeyDown(KeyCode.Keypad2))
            CardGameTrunManager.onAddCard?.Invoke(false);

        //trun 넘기는 기능 22.05.02 승주
        if (Input.GetKeyDown(KeyCode.Keypad3))
            CardGameTrunManager.Inst.EndTurn();

        //적이 card를 강제로 내게 하는 기능 22.05.04 승주
        if (Input.GetKeyDown(KeyCode.Keypad4))
            CardManager.Inst.TryPutCard(false);

        //나에게 damage 19를 주는 기능 22.05.09 승주
        if (Input.GetKeyDown(KeyCode.Keypad5))
            CardGameEntityManager.Inst.DamageBoss(true, 19);

        //적에게 damage 19를 주는 기능 22.05.09 승주
        if (Input.GetKeyDown(KeyCode.Keypad6))
            CardGameEntityManager.Inst.DamageBoss(false, 19);
    }

    public void StartGame()
    {
        StartCoroutine(CardGameTrunManager.Inst.StartGameCo());
    }

    //??? String을 넣으면 원하는 string을 출력 할 수 있게 해주는 기능 22.05.02 승주 
    public void Notification(string message)
    {

        notificationPanel.Show(message);
    }

    public IEnumerator GameOver(bool isMywin)
    {
        CardGameTrunManager.Inst.isLoading = true;
        endTurnBtn.SetActive(false);
        yield return delay2;

        CardGameTrunManager.Inst.isLoading = true;
        resultPanel.Show(isMywin ? "Winner" : "Loser");
        cameraEffect.SetGrayScale(true);
    }
}
