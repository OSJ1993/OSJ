using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ġƮ, UI, ��ŷ, ���ӿ��� ��� 22.05.02 ����
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
        //my card ��� ���22.05.02 ����
        if (Input.GetKeyDown(KeyCode.Keypad1))
            CardGameTrunManager.onAddCard?.Invoke(true);

        //other card ��� ��� 22.05.02 ����
        if (Input.GetKeyDown(KeyCode.Keypad2))
            CardGameTrunManager.onAddCard?.Invoke(false);

        //trun �ѱ�� ��� 22.05.02 ����
        if (Input.GetKeyDown(KeyCode.Keypad3))
            CardGameTrunManager.Inst.EndTurn();

        //���� card�� ������ ���� �ϴ� ��� 22.05.04 ����
        if (Input.GetKeyDown(KeyCode.Keypad4))
            CardManager.Inst.TryPutCard(false);

        //������ damage 19�� �ִ� ��� 22.05.09 ����
        if (Input.GetKeyDown(KeyCode.Keypad5))
            CardGameEntityManager.Inst.DamageBoss(true, 19);

        //������ damage 19�� �ִ� ��� 22.05.09 ����
        if (Input.GetKeyDown(KeyCode.Keypad6))
            CardGameEntityManager.Inst.DamageBoss(false, 19);
    }

    public void StartGame()
    {
        StartCoroutine(CardGameTrunManager.Inst.StartGameCo());
    }

    //??? String�� ������ ���ϴ� string�� ��� �� �� �ְ� ���ִ� ��� 22.05.02 ���� 
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
