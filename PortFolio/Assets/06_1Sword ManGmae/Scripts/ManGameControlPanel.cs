using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//IPointerDownHandler =��ġ ���� ���     22.04.29 ����
//IPointerUpHandler     =��ġ ���� ���     22.04.29 ����
//IDragHandler            =�巡�� ���� ���  2204.29 ����
public class ManGameControlPanel : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public ManGamePlayer player;

    public RectTransform pad;
    public RectTransform stick;

    public Transform camBox;
    public Vector3 camPosition;

    //�̵� ���� ��� 22.04.29 ����
    Vector3 move;

    //�Ȱ� �ִ� �� Ȯ�� �ϴ� ��� 22.04.29 ����
    bool walking;

    //dash ��� 22.04.29 ����
    float releaseTime;


    //IDragHandler            =�巡�� ���� ���  2204.29 ����
    public void OnDrag(PointerEventData eventData)
    {
        //drag�� �� stick�� �����̰� �ϴ� ��� 22.04.29
        stick.position = eventData.position;

        //skick�� pad���� ����� ���ϰ� ���ֵδ� ��� 22.04.29
        stick.localPosition = Vector2.ClampMagnitude(
            eventData.position - (Vector2)pad.position, pad.rect.width * 0.5f);

        //�̵� ���� �������� ��� 22.04.29 ����
        move = new Vector3(stick.localPosition.x, 0, stick.localPosition.y).normalized;

        //�ִϸ��̼� ���� ��� 22.04.29 ����
        if (!walking)
        {
            //�ߺ� ���� ���� ��� 22.04.29 ����
            walking = true;

            player.playerAni.SetBool("Walk", true);
        }
    }

    //IPointerDownHandler =��ġ ���� ���     22.04.29 ����
    public void OnPointerDown(PointerEventData eventData)
    {

        //�հ����� �� ���� pad�� ��Ÿ���� �� ��� 22.04.29 ����
        pad.position = eventData.position;

        //pad ��Ÿ���� �ϴ� ��� 22.04.29 ����
        pad.gameObject.SetActive(true);

        //�հ����� ������ �ۼ��� �ڷ�ƾ �۵� �ǰ� �ϴ�  ���22.04.29 ����
        StartCoroutine("PlayerMove");
    }

    //IPointerUpHandler     =��ġ ���� ���     22.04.29 ����
    public void OnPointerUp(PointerEventData eventData)
    {
        //�հ����� ��ٰ� ���� �ð��� �������� dash�� �۵��ϰ� �ϴ� ��� 22.04.29 ����
        //magnitude-> Veter�� ���̷� ��ȯ �ϴ� ��� 22.04.29 ����
        if (releaseTime < 0.2f && stick.localPosition.magnitude > pad.rect.width * 0.4f)
        {
            //dash ��� 22.04.29 ����
            Dash();

        }

        //�հ����� ��ٰ� ���� �ð��� �������� attack�� �۵��ϰ� �ϴ� ��� 22.04.29 ����
        //magnitude-> Veter�� ���̷� ��ȯ �ϴ� ��� 22.04.29 ����
        if (releaseTime < 0.2f && stick.localPosition.magnitude <= pad.rect.width * 0.4f)
        {
            //attack ��� 22.04.29 ����
            player.Attack();
        }


        //stick���� �հ����� ���� ���� ��ġ�� ���ư��� ��� 22.04.29 ����
        stick.localPosition = Vector2.zero;

        //pad ������� �ϴ� ��� 22.04.29 ����
        pad.gameObject.SetActive(false);

        //�հ����� ���� move�� 0 �̵Ǿ� �������� �ʰ� �Ǵ� ��� 22.04.29 ����
        move = Vector3.zero;

        //�հ����� ���� �ۼ��� �ڷ�ƾ ���� �ǰ� �ϴ� ���22.04.29 ����
        StopCoroutine("PlayerMove");

        //�ߺ� ���� ���� ��� 22.04.29 ����
        walking = false;

        player.playerAni.SetBool("Walk", false);

    }

    //player �̵� �ڷ�ƾ ��� 22.04.29 ����
    IEnumerator PlayerMove()
    {
        releaseTime = 0;
        while (true)
        {
            releaseTime += Time.deltaTime;

            //�̵������� ���� ���� �۵� �ϴ� ��� 22.04.29 ����
            if (move != Vector3.zero)
            {
                //enableMove�� true �� ���� �����̰� �ϴ� ��� 22.04.29 ����
                if (player.enableMove)

                    player.playerRigid.velocity = move * player.moveSpeed;

                //ȸ�� ��� 22.04.29 ����
                //Quaternion.Slerp[A, B, C]-> A�� ȸ������ B�� ȸ�������� C �ӵ��� �����ϸ鼭 ȸ�� �ϴ� ��� 22.04.29 ����
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(move), 5 * Time.deltaTime);

            }


            yield return null;
        }
    }

    //dash��� 22.04.29 ����
    void Dash()
    {
        //enableMove�� true �� ���� �����̰� �ϴ� ��� 22.04.29 ����
        if (player.enableMove)
        {
            player.playerAni.Play("Jump");

            //player�� �հ����� �� ������ �ٶ󺸰� �ϰ� 
            player.transform.rotation = Quaternion.LookRotation(move);


            //AddForce�� ������ ������ �ϴ� ��� 22.04.29 ����
            //ForceMode �������� �� ��� 22.04.29 ����
            player.playerRigid.AddForce(move * player.dashSpeed, ForceMode.Impulse);
        }
    }

    //LateUpdate�� ��� Update�� ȣ��� �Ŀ� ȣ�� �Ǵ� ��� 22.04.29 ����
    private void LateUpdate()
    {
        camBox.position = player.transform.position + camPosition;
    }
}
