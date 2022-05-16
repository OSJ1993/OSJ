using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyPlayerMove : MonoBehaviour
{
    public float Speed;

    public GameManager manager;

    Rigidbody2D rigid2D;
    float h;
    float v;

    //���� �̵� ���(�÷��� ����) 22.05.16 ����
    bool isHorizonMove;

    Animator anim;

    //���� �ٶ󺸰� �ִ� ���� ���� ���� ���� �ʿ� ��� 22.05.16 ����
    Vector3 dirVec;

    GameObject scanObject;

    void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //player move value ��� 22.05.16 ����
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //üũ ��ư ��� 22.05.16 ����
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        //��ư �ٿ����� �����̵� üũ ��� 22.05.16 ����
        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = h != 0;

        //Aniation ��� 22.05.16 ����
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);

        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {

            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
            anim.SetBool("isChange", false);

        //Direction ��� 22.05.16 ����
        if (vDown && v == 1) dirVec = Vector3.up;
        else if (vDown && v == -1) dirVec = Vector3.down;
        else if (hDown && h == -1) dirVec = Vector3.left;
        else if (hDown && h == 1) dirVec = Vector3.right;

        //Scan Object ��� 22.05.16 ����
        if (Input.GetButtonDown("Jump") && scanObject != null)
            manager.Action(scanObject);
    }

    void FixedUpdate()
    {
        //�����̴� ��� 22.05.16 ����
        //�÷��� ������ ����, ���� �̵� ���� ��� 22.05.16 ����
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);

        rigid2D.velocity = moveVec * Speed;

        //Ray
        Debug.DrawRay(rigid2D.position, dirVec * 1f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid2D.position, dirVec, 1f, LayerMask.GetMask("DailyObj"));

        //�տ� �繰�� �ִ��� Ȯ�� �ϴ� ��� 22.05.16 ����
        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }

        //�տ� �繰�� ������ Ȯ�� �ϴ� ��� 22.05.16����
        else
            scanObject = null;
    }
}
