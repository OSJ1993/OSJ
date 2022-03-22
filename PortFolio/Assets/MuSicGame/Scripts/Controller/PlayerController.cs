using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //얼만큼 빠른 속도로 이동 시킬 지.
    [SerializeField] float moveSpeed = 3;

    //움직일 방향.
    Vector3 dir = new Vector3();

    //목적지.
    Vector3 destPos = new Vector3();

    TimingManager theTimingManager;

    void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
    }


    void Update()
    {
        //매 프레임 마다 키가 눌렸는지 확인해야함.
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {
            // 판정 체크.
            //Space가 눌리면 타이밍 판정할 수 있게 체크타이밍 호출.
            if (theTimingManager.CheckTiming())
            {
                //올바른 판정일 때만 움직이게.
                StartAction();

            }
        }
    }

    void StartAction()
    {
        //어느 방향으로 눌렸는 지 알기 위함.
        // 입력값 or 위 방향키 W키=1, or 아래 방향키 S키=-1 없을 시 =0
        dir.Set(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));

        // 이동 목표값 계산(목적지)
        destPos = transform.position + new Vector3(-dir.x, 0, dir.z);
    }

   /* IEnumerator MoveCo()
    {
        //A 좌표와 B 좌표간의 거리차를 반환 SqrMagnitude: 제곱근을 리턴 ex: SqrMagnitude(4) =2
        while (Vector3.SqrMagnitude(transform.position - destPos) != 0.001f)
        {
           
        }*/
    }

