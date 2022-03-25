using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool s_canPressKey = true;

    //�̵�
    //��ŭ ���� �ӵ��� �̵� ��ų ��.
    [SerializeField] float moveSpeed = 3;

    //������ ����.
    Vector3 dir = new Vector3();

    //������.
    public Vector3 destPos = new Vector3();



    //ȸ�� /22.03.23 by ����
    [SerializeField] float spinSpeed = 270;

    //ȸ����ų ���� /22.03.23 by ����
    Vector3 rotDir = new Vector3();

    //��ŭ ȸ����ų �� ��ǥ ȸ�� ��. /22.03.23 by ����
    Quaternion destRot = new Quaternion();

    //���Ÿ��� �ݵ� �Լ� /22.03.23 by ����
    [SerializeField] float recoilPosY = 0.25f;
    [SerializeField] float recoilSpeed = 1.5f;

    //�޹��� ���� �������� ���� ��Ʈ ������ ���� �Լ� /22.03.23 by����
    bool canMove = true;

    //�߶� ������ �ƴ� �� Ȯ�� �ϴ� ��� /22.03.25 by����
    bool isFalling = false;


    //��¥ť�긦 ���� ���� ���� �� ���ư� ��ŭ�� ���� ��ǥ ȸ�������� ����. /22.03.23 by����
    [SerializeField] Transform fakeCube = null;

    //ȸ����ų ��ü ��¥ť��./22.03.23 by����
    [SerializeField] Transform realCube = null;

    TimingManager theTimingManager;
    CameraController theCam;
    Rigidbody myRigid;

    void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
        theCam = FindObjectOfType<CameraController>();
        myRigid = GetComponentInChildren<Rigidbody>();
    }


    void Update()
    {
        CheckFalling();

        //�� ������ ���� Ű�� ���ȴ��� Ȯ���ؾ���.
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {
            if (canMove && s_canPressKey && !isFalling)
            {
                Calc();

                // ���� üũ.
                //Space�� ������ Ÿ�̹� ������ �� �ְ� üũŸ�̹� ȣ��.
                if (theTimingManager.CheckTiming())
                {
                    //�ùٸ� ������ ���� �����̰�.
                    StartAction();

                }

            }
        }
    }

    //��� 22.03.24 by����
    void Calc()
    {
        //��� �������� ���ȴ� �� �˱� ����.
        // �Է°� or �� ����Ű WŰ=1, or �Ʒ� ����Ű SŰ=-1 ���� �� =0
        dir.Set(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));

        //�̵� ��ǥ�� ���(������)
        destPos = transform.position + new Vector3(-dir.x, 0, dir.z);

        //ȸ�� ��ǥ�� ��� /22.03.23 by����
        rotDir = new Vector3(-dir.z, 0f, -dir.x);

        //RotateAround : �¾� �ֺ��� �����ϴ� ���� ���� ������ �� ��� /22.03.23 by ����
        //RotateAround(���� ���,) ȸ�� ��, ȸ�� ��)�� �̿��� <��� ȸ�� ����> /22.03.23 by ����
        //RotateAround�� �̿��ؼ� fakeCube�� ȸ����Ų ������ �� ���� ������� ���� PlayerController�� �پ��ִ� ��ũ��Ʈ�� ��ü ���� �־��ְ� ���⿡ ����, spinSieed�־��ֱ� /22.03.23 by ����
        fakeCube.RotateAround(transform.position, rotDir, spinSpeed);

        //fakeCube ��� ���� destRot�� rotation�� �־��ֱ�. /22.03.23 by ����
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

        //A ��ǥ�� B ��ǥ���� �Ÿ����� ��ȯ SqrMagnitude: �������� ���� ex: SqrMagnitude(4) =2
        while (Vector3.SqrMagnitude(transform.position - destPos) >= 0.001f)
        {
            // �ڿ������� ���������� ť�긦 �̵� /22.03.23 by ����
            transform.position = Vector3.MoveTowards(transform.position, destPos, moveSpeed * Time.deltaTime);

            //�ݺ����ȿ��� �� �����Ӿ� ���鼭 �����ְ� ����� �Լ� /22.03.23 by����
            yield return null;
        }

        //while���� ���� ������ ���� �ټ��� ���̰� ���� �� �ֱ� ������ �ڱ� �ڽ��� ��ġ�� destPos���� ��ü /22.03.23 by����
        transform.position = destPos;

        canMove = true;
    }

    IEnumerator SpinCo()
    {
        //while���� ���� �ݺ� ����.Quaternion�� Angle ���� �� ���ϴ� �Լ�. /22.03.23 by����
        //realCube�� ȸ���� ��ǥ������ ȸ������ ���̰� 0.5f���� Ŭ ���� �ݺ�����. /22.03.23 by����
        while (Quaternion.Angle(realCube.rotation, destRot) > 0.5f)
        {
            //�ڱ� �ڽ��� ȸ�� ������ ��ǥ ȸ�� �� spinSpeed�� �־��ָ� realCue�� ȸ���ϴ� �Լ�. /22.03.23 by����
            realCube.rotation = Quaternion.RotateTowards(realCube.rotation, destRot, spinSpeed * Time.deltaTime);
            yield return null;
        }

        //�������� �ݺ����� ���� ������ ��ǥ������ ������ ��ġ�ϰ� ���ִ� �Լ�./22.03.23 by����
        realCube.rotation = destRot;

    }

    IEnumerator RecoilCo()
    {
        //realCube position y���� recoilPosY������ ���� ��� �ݺ��� ����. /22.03.23 by����
        while (realCube.position.y < recoilPosY)
        {
            realCube.position += new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }

        //�ݵ��� �ְ� ���̱��� �ö󰡸� �ݺ����� ������ �ݺ����� ���ؼ� ���� ��ġ�� �����ִ� �Լ� /22.03.23 by����
        //realCube position.y�� 0 ���� Ŀ�� �� ���� �ݺ� ����./22.03.23 by����
        while (realCube.position.y > 0)
        {
            realCube.position -= new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }

        realCube.localPosition = new Vector3(0, 0, 0);
    }


    void CheckFalling()
    {
        if (!isFalling)
        {
            if (!Physics.Raycast(transform.position, Vector3.down, 1.1f))
            {
                Falling();
            }

        }
    }

    void Falling()
    {
        isFalling = true;
        myRigid.useGravity = true;
        myRigid.isKinematic = false;
    }
}

