using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ġƮ, UI, ��ŷ, ���ӿ��� ��� 22.05.02 ����
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
        //my card ��� ���22.05.02 ����
        if (Input.GetKeyDown(KeyCode.Keypad1))
            CardTrunManager.onAddCard?.Invoke(true);

        //other card ��� ��� 22.05.02 ����
        if (Input.GetKeyDown(KeyCode.Keypad2))
            CardTrunManager.onAddCard?.Invoke(false);

        //trun �ѱ�� ��� 22.05.02 ����
        if (Input.GetKeyDown(KeyCode.Keypad3))
            CardTrunManager.Inst.EndTurn();

        //���� card�� ������ ���� �ϴ� ��� 22.05.04 ����
        if (Input.GetKeyDown(KeyCode.Keypad4))
            CardManager.Inst.TryPutCard(false);
    }

    public void StartGame()
    {
        StartCoroutine(CardTrunManager.Inst.StartGameCo());
    }

    //??? String�� ������ ���ϴ� string�� ��� �� �� �ְ� ���ִ� ��� 22.05.02 ���� 
    public void Notification(string message)
    {
       
        cardNotificationPanel.Show(message);
    }
}
