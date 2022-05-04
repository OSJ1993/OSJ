using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 치트, UI, 랭킹, 게임오버 기능 22.05.02 승주
public class CardGameManager : MonoBehaviour
{
    public static CardGameManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] CardNotificationPanel cardNotificationPanel;

    void Start()
    {
        StartGame();
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
            CardTrunManager.onAddCard?.Invoke(true);

        //other card 얻기 기능 22.05.02 승주
        if (Input.GetKeyDown(KeyCode.Keypad2))
            CardTrunManager.onAddCard?.Invoke(false);

        //trun 넘기는 기능 22.05.02 승주
        if (Input.GetKeyDown(KeyCode.Keypad3))
            CardTrunManager.Inst.EndTurn();

        //적이 card를 강제로 내게 하는 기능 22.05.04 승주
        if (Input.GetKeyDown(KeyCode.Keypad4))
            CardManager.Inst.TryPutCard(false);
    }

    public void StartGame()
    {
        StartCoroutine(CardTrunManager.Inst.StartGameCo());
    }

    //??? String을 넣으면 원하는 string을 출력 할 수 있게 해주는 기능 22.05.02 승주 
    public void Notification(string message)
    {
       
        cardNotificationPanel.Show(message);
    }
}
