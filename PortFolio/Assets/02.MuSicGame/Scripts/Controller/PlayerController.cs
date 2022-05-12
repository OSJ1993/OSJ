using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool s_canPressKey = true;

    //이동
    //얼만큼 빠른 속도로 이동 시킬 지.
    [SerializeField] float moveSpeed = 3;

    //움직일 방향.
    Vector3 dir = new Vector3();

    //목적지.
    public Vector3 destPos = new Vector3();

    //원위치 시키기 위한 좌표값 /22.03.25 by승주
    Vector3 originPos = new Vector3();


    //회전 /22.03.23 by 승주
    [SerializeField] float spinSpeed = 270;

    //회전시킬 방향 /22.03.23 by 승주
    Vector3 rotDir = new Vector3();

    //얼만큼 회전시킬 지 목표 회전 값. /22.03.23 by 승주
    Quaternion destRot = new Quaternion();

    //들썩거리는 반동 함수 /22.03.23 by 승주
    [SerializeField] float recoilPosY = 0.25f;
    [SerializeField] float recoilSpeed = 1.5f;

    //급발진 방지 굴러가는 동안 노트 판정을 막는 함수 /22.03.23 by승주
    bool canMove = true;

    //추락 중인지 아닌 지 확인 하는 기능 /22.03.25 by승주
    bool isFalling = false;


    //가짜큐브를 먼저 돌려 놓고 그 돌아간 만큼의 값으 목표 회전값으로 삼음. /22.03.23 by승주
    [SerializeField] Transform fakeCube = null;

    //회전시킬 객체 진짜큐브./22.03.23 by승주
    [SerializeField] Transform realCube = null;

    TimingManager theTimingManager;
    CameraController theCam;
    Rigidbody myRigid;
    StatusManager theStatus;

    void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
        theStatus = FindObjectOfType<StatusManager>();
        theCam = FindObjectOfType<CameraController>();
        myRigid = GetComponentInChildren<Rigidbody>();

        //자기 위치 기억. 22.03.26 by승주
        originPos = transform.position;
    }

    //처음 있던 위치로 돌려보내는 기능 22.03.29 by승주
    public void Initialized()
    {
        transform.position = Vector3.zero;
        destPos = Vector3.zero;
        realCube.localPosition = Vector3.zero;
        canMove = true;
        s_canPressKey = true;
        isFalling = false;
        myRigid.useGravity = false;
        myRigid.isKinematic = true;
    }


    void Update()
    {
        if (MusicGameManager.instance.isStartGame)
        {

            CheckFalling();

            
            //매 프레임 마다 키가 눌렸는지 확인해야함.
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
            {
                if (canMove && s_canPressKey && !isFalling)
                {
                    Calc();
            
                    // 판정 체크.
                    //Space가 눌리면 타이밍 판정할 수 있게 체크타이밍 호출.
                    if (theTimingManager.CheckTiming())
                    {
                        //올바른 판정일 때만 움직이게.
                        StartAction();
            
                    }
            
                }
            }
            
        }
    }

    //계산 22.03.24 by승주
    void Calc()
    {
        //어느 방향으로 눌렸는 지 알기 위함.
        // 입력값 or 위 방향키 W키=1, or 아래 방향키 S키=-1 없을 시 =0
        dir.Set(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));

        //이동 목표값 계산(목적지)
        destPos = transform.position + new Vector3(-dir.x, 0, dir.z);

        //회전 목표값 계산 /22.03.23 by승주
        rotDir = new Vector3(-dir.z, 0f, -dir.x);

        //RotateAround : 태양 주변을 공전하는 지구 등을 구현할 때 사용 /22.03.23 by 승주
        //RotateAround(공전 대상,) 회전 축, 회전 값)을 이용한 <편법 회전 구현> /22.03.23 by 승주
        //RotateAround를 이용해서 fakeCube를 회전시킨 다음에 그 값을 결과값에 저장 PlayerController에 붙어있는 스크립트의 객체 값을 넣어주고 여기에 방향, spinSieed넣어주기 /22.03.23 by 승주
        fakeCube.RotateAround(transform.position, rotDir, spinSpeed);

        //fakeCube 결과 값을 destRot의 rotation에 넣어주기. /22.03.23 by 승주
        destRot = fakeCube.rotation;
    }

    void StartAction()
    {



        StartCoroutine(MoveCo());
        StartCoroutine(SpinCo());
        StartCoroutine(RecoilCo());
        StartCoroutine(theCam.ZoomCam());
    }

    IEnumerator MoveCo()
    {
        canMove = false;

        //A 좌표와 B 좌표간의 거리차를 반환 SqrMagnitude: 제곱근을 리턴 ex: SqrMagnitude(4) =2
        while (Vector3.SqrMagnitude(transform.position - destPos) >= 0.001f)
        {
            // 자연스럽게 목적지까지 큐브를 이동 /22.03.23 by 승주
            transform.position = Vector3.MoveTowards(transform.position, destPos, moveSpeed * Time.deltaTime);

            //반복문안에서 한 프레임씩 쉬면서 돌려주게 만드는 함수 /22.03.23 by승주
            yield return null;
        }

        //while문을 빠져 나가면 아주 근소한 차이가 있을 수 있기 떄문에 자기 자신의 위치를 destPos값을 대체 /22.03.23 by승주
        transform.position = destPos;

        canMove = true;
    }

    IEnumerator SpinCo()
    {
        //while문을 통해 반복 실행.Quaternion과 Angle 값의 차 구하는 함수. /22.03.23 by승주
        //realCube의 회전값 목표지점의 회전값의 차이가 0.5f보다 클 때만 반복실행. /22.03.23 by승주
        while (Quaternion.Angle(realCube.rotation, destRot) > 0.5f)
        {
            //자기 자신의 회전 값에서 목표 회전 값 spinSpeed를 넣어주면 realCue가 회전하는 함수. /22.03.23 by승주
            realCube.rotation = Quaternion.RotateTowards(realCube.rotation, destRot, spinSpeed * Time.deltaTime);
            yield return null;
        }

        //마지막에 반복문을 빠져 나오면 목표값으로 완전히 일치하게 해주는 함수./22.03.23 by승주
        realCube.rotation = destRot;

    }

    IEnumerator RecoilCo()
    {
        //realCube position y값이 recoilPosY값보다 작을 경우 반복문 실행. /22.03.23 by승주
        while (realCube.position.y < recoilPosY)
        {
            realCube.position += new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }

        //반동이 최고 높이까지 올라가면 반복문이 끝나면 반복문을 통해서 원래 위치로 내려주는 함수 /22.03.23 by승주
        //realCube position.y가 0 보다 커질 때 까지 반복 실행./22.03.23 by승주
        while (realCube.position.y > 0)
        {
            realCube.position -= new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }

        realCube.localPosition = new Vector3(0, 0, 0);
    }



    void CheckFalling()
    {
        //isFalling false일 때만 기능 사용 /22.03.25 by승주

        if (!isFalling && canMove)
        {
            //자기 자신 위치에서 아래 방향으로 레이져를 쏘는 기능 false일 때 추락. /22.03.25 by승주
            if (!Physics.Raycast(transform.position, Vector3.down, 1.1f))
            {
                Falling();
            }

        }
    }

    //추락하는 기능 22.03.25
    void Falling()
    {
        isFalling = true;


        myRigid.useGravity = true;
        myRigid.isKinematic = false;
    }

    //추락 전으로 되돌리는 기능 22.03.26 by승주
    public void ResetFalling()
    {
        //체력감소 22.03.27 by승주
        theStatus.DecreaseHP(1);

        AudioManager.instance.PlaySFX("Falling");

        //플레이어가 죽으면 리셋 시키면 안되기에 계속 추락시키는 기능 /22.03.27 by승주
        if (!theStatus.Isdead())
        {
            isFalling = false;
            myRigid.useGravity = false;
            myRigid.isKinematic = true;

            transform.position = originPos;
            //cube도 원위치 시켜줘야함 이유는 부모만 원위치 시키고 자식은 원위치 안 시키면 Rigidbody가 없는 부모 객체는 낭떠러지 위에. 자식 객체(그래픽)은 추락 하기 때문이다. /22.03.26 by승주
            realCube.localPosition = new Vector3(0, 0, 0);
        }


    }
}

