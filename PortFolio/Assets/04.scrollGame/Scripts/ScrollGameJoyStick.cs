using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollGameJoyStick : MonoBehaviour,

    IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public enum JoystickType { Move }
    public JoystickType joystickType;
    public float sensitivity = 1f; // ���� �ΰ���

   


    public Player player;

    private Image imageBackground; // ���̽�ƽ UI �� ��� �̹��� ����
    private Image imageController; // ���̽�ƽ UI �� ��Ʈ�ѷ�(�ڵ�) �̹��� ����
    private Vector2 touchPosition; // ���̽�ƽ�� ���������� �ܺ� Ŭ�������� ����� �� �ֵ��� ���� ���� ����

    public float horizontal { get { return touchPosition.x * sensitivity; } }
    public float vertical { get { return touchPosition.y * sensitivity; } }

    private void Awake()
    {
        imageBackground = GetComponent<Image>();
        imageController = transform.GetChild(0).GetComponent<Image>();
    }

    /// <summary>
    /// ��ġ ���� �� 1ȸ ����
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    // IPointerDownHandler �������̽��� �θ�� ��ӹ��� ��� �����ؾߵǴ� �޼ҵ�
    // �ش� ��ũ��Ʈ�� ������ �ִ� ������Ʈ�� ��ġ���� �� �޼ҵ尡 1ȸ ����ȴ�.
    {
        //Debug.Log("��ġ ���� : " + eventData);

        //---------------------------------------------------------------------5.27
        //pad.position = eventData.position;

        //pad.gameObject.SetActive(true);

        StartCoroutine("PlayerMove");

       

        Debug.Log("pad On");

        //-------------------------------------------------------------------5.27


    }
    /// <summary>
    /// ��ġ ������ �� �� ������ ����
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    // IDragHandler �������̽��� �θ�� ��ӹ��� ��� �����ؾߵǴ� �޼ҵ�
    // �ش� ��ũ��Ʈ�� ������ �ִ� ������Ʈ�� ��ġ���¿��� �巡�� ���� �� �޼ҵ尡 �� ������ ����ȴ�.
    {
        // touchPosition = Vector2.zero;

        //stick.position = eventData.position;

        // ���̽�ƽ�� ��ġ�� ��� �ֵ� ������ ���� �����ϱ� ����
        // 'touchPosition'�� ��ġ ���� �̹����� ���� ��ġ�� ��������
        // �󸶳� ������ �ִ����� ���� �ٸ��� ���´�.
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(imageBackground.rectTransform, eventData.position, eventData.pressEventCamera, out touchPosition))
        {
            // touchPosition ���� ����ȭ [0 ~ 1]
            // touchPosition�� �̹��� ũ��� ����
            touchPosition.x = (touchPosition.x / imageBackground.rectTransform.sizeDelta.x);
            touchPosition.y = (touchPosition.y / imageBackground.rectTransform.sizeDelta.y);

            // touchPosition ���� ����ȭ [-n ~ n]
            // ����(-1), �߽�(0), ������(1)�� �����ϱ� ���� touchPosition.x*2-1
            // �Ʒ�(-1), �߽�(0), ��(1)�� �����ϱ� ���� touchPosition.y*2-1
            // �� ������ Pivot�� ���� �޶�����. (���ϴ� Pivot ����)
            switch (joystickType)
            {
                case JoystickType.Move: // (���ϴ� Pivot ����)
                    touchPosition = new Vector2(touchPosition.x * 2 - 1, touchPosition.y * 2 - 1);
                    break;

            }

            // touchPosition ���� ����ȭ [-1 ~ 1]
            // ���� ���̽�ƽ ��� �̹��� ������ ��ġ�� ������ �Ǹ� -1 ~ 1���� ū ���� ���� �� �ִ�.
            // �� �� normailzed�� �̿��� -1 ~ 1 ������ ������ ����ȭ
            touchPosition = (touchPosition.magnitude > 1) ? touchPosition.normalized : touchPosition;

            // ���� ���̽�ƽ ��Ʈ�ѷ� �̹��� �̵�
            // touchPosition�� -1 ~ 1 ������ �������̱� ������ �״�� ����ϰԵǸ�, ��Ʈ�ѷ��� �������� ���� �����.
            // �Ͽ�, ��� ũ�⸦ ���ؼ� ����Ѵ�.(��, �߽��� �������� ���� -1, ������ 1 �̱� ������ ��� ũ���� ������ ����.)
            // TIP. ��Ʈ�ѷ��� ��� �̹��� �ٱ����� Ƣ����� �ϰ� ���� �ʴٸ�, �����ִ� ���� �� ũ�� �����ؾ� �Ѵ�.
            Vector2 controllerPosition = new Vector2(touchPosition.x * imageBackground.rectTransform.sizeDelta.x / 2,
                                                     touchPosition.y * imageBackground.rectTransform.sizeDelta.y / 2);
            imageController.rectTransform.anchoredPosition = controllerPosition;

            //Debug.Log("��ġ&�巡�� : " + eventData);
        }
    }
    /// <summary>
    /// ��ġ ���� �� 1ȸ ����
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    // IPointerUpHandler �������̽��� �θ�� ��ӹ��� ��� �����ؾߵǴ� �޼ҵ�
    // �ش� ��ũ��Ʈ�� ������ �ִ� ������Ʈ�� ��ġ�Ͽ��ٰ� ������ �� �޼ҵ尡 1ȸ ����ȴ�.
    {
        // ��ġ ���� �� �̹����� ��ġ�� �߾����� �ٽ� �ű��.
        imageController.rectTransform.anchoredPosition = Vector2.zero;
        // �ٸ� ������Ʈ���� �̵� �������� ����ϱ� ������ �̵� ���⵵ �ʱ�ȭ
        touchPosition = Vector2.zero;

        //5.27---------------------------------------------------------------------------------
        //pad.gameObject.SetActive(false);

        StopCoroutine("PlayerMove");

        Debug.Log("Pad Out");

        
        //5.27---------------------------------------------------------------------------------
        //Debug.Log("��ġ ���� : " + eventData);
    }

    //5.27---------------------------------------------------------------------------------
    IEnumerator PlayerMove()
    {
        while (true)
        {
            if (touchPosition != Vector2.zero)
            {
                if (player.enbleMove)
                    player.playerRigid.velocity = touchPosition * player.speed;
            }

            yield return null;
        }

    }
    //5.27---------------------------------------------------------------------------------
}
