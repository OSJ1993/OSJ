using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//IPointerDownHandler =터치 시작 기능     22.04.29 승주
//IPointerUpHandler     =터치 종료 기능     22.04.29 승주
//IDragHandler            =드래그 감지 기능  2204.29 승주
public class ManGameControlPanel : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public ManGamePlayer player;

    public RectTransform pad;
    public RectTransform stick;

    public Transform camBox;
    public Vector3 camPosition;

    //이동 방향 기능 22.04.29 승주
    Vector3 move;

    //걷고 있는 지 확인 하는 기능 22.04.29 승주
    bool walking;

    //dash 기능 22.04.29 승주
    float releaseTime;


    //IDragHandler            =드래그 감지 기능  2204.29 승주
    public void OnDrag(PointerEventData eventData)
    {
        //drag할 때 stick이 움직이게 하는 기능 22.04.29
        stick.position = eventData.position;

        //skick이 pad에서 벗어나지 못하게 가둬두는 기능 22.04.29
        stick.localPosition = Vector2.ClampMagnitude(
            eventData.position - (Vector2)pad.position, pad.rect.width * 0.5f);

        //이동 방향 정해지는 기능 22.04.29 승주
        move = new Vector3(stick.localPosition.x, 0, stick.localPosition.y).normalized;

        //애니메이션 구현 기능 22.04.29 승주
        if (!walking)
        {
            //중복 실행 방지 기능 22.04.29 승주
            walking = true;

            player.playerAni.SetBool("Walk", true);
        }
    }

    //IPointerDownHandler =터치 시작 기능     22.04.29 승주
    public void OnPointerDown(PointerEventData eventData)
    {

        //손가락을 댄 곳에 pad가 나타나게 할 기능 22.04.29 승주
        pad.position = eventData.position;

        //pad 나타나게 하는 기능 22.04.29 승주
        pad.gameObject.SetActive(true);

        //손가락이 닿으면 작성한 코루틴 작동 되게 하는  기능22.04.29 승주
        StartCoroutine("PlayerMove");
    }

    //IPointerUpHandler     =터치 종료 기능     22.04.29 승주
    public void OnPointerUp(PointerEventData eventData)
    {
        //손가락을 댔다가 떼는 시간을 기준으로 dash가 작동하게 하는 기능 22.04.29 승주
        //magnitude-> Veter를 길이로 변환 하는 기능 22.04.29 승주
        if (releaseTime < 0.2f && stick.localPosition.magnitude > pad.rect.width * 0.4f)
        {
            //dash 기능 22.04.29 승주
            Dash();

        }

        //손가락을 댔다가 떼는 시간을 기준으로 attack이 작동하게 하는 기능 22.04.29 승주
        //magnitude-> Veter를 길이로 변환 하는 기능 22.04.29 승주
        if (releaseTime < 0.2f && stick.localPosition.magnitude <= pad.rect.width * 0.4f)
        {
            //attack 기능 22.04.29 승주
            player.Attack();
        }


        //stick에서 손가락을 떼면 원래 위치로 돌아가는 기능 22.04.29 승주
        stick.localPosition = Vector2.zero;

        //pad 사라지게 하는 기능 22.04.29 승주
        pad.gameObject.SetActive(false);

        //손가락을 떼면 move는 0 이되어 움직이지 않게 되는 기능 22.04.29 승주
        move = Vector3.zero;

        //손가락이 떼면 작성한 코루틴 중지 되게 하는 기능22.04.29 승주
        StopCoroutine("PlayerMove");

        //중복 실행 방지 기능 22.04.29 승주
        walking = false;

        player.playerAni.SetBool("Walk", false);

    }

    //player 이동 코루틴 기능 22.04.29 승주
    IEnumerator PlayerMove()
    {
        releaseTime = 0;
        while (true)
        {
            releaseTime += Time.deltaTime;

            //이동방향이 있을 때만 작동 하는 기능 22.04.29 승주
            if (move != Vector3.zero)
            {
                //enableMove가 true 일 때만 움직이게 하는 기능 22.04.29 승주
                if (player.enableMove)

                    player.playerRigid.velocity = move * player.moveSpeed;

                //회전 기능 22.04.29 승주
                //Quaternion.Slerp[A, B, C]-> A의 회전값을 B의 회전값으로 C 속도로 보정하면서 회전 하는 기능 22.04.29 승주
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(move), 5 * Time.deltaTime);

            }


            yield return null;
        }
    }

    //dash기능 22.04.29 승주
    void Dash()
    {
        //enableMove가 true 일 때만 움직이게 하는 기능 22.04.29 승주
        if (player.enableMove)
        {
            player.playerAni.Play("Jump");

            //player가 손가락을 민 방향을 바라보게 하고 
            player.transform.rotation = Quaternion.LookRotation(move);


            //AddForce로 앞으로 나가게 하는 기능 22.04.29 승주
            //ForceMode 순간적인 힘 기능 22.04.29 승주
            player.playerRigid.AddForce(move * player.dashSpeed, ForceMode.Impulse);
        }
    }

    //LateUpdate는 모든 Update가 호출된 후에 호출 되는 기능 22.04.29 승주
    private void LateUpdate()
    {
        camBox.position = player.transform.position + camPosition;
    }
}
