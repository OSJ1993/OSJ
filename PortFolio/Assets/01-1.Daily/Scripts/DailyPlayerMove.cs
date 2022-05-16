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

    //십자 이동 기능(플래그 변수) 22.05.16 승주
    bool isHorizonMove;

    Animator anim;

    //현재 바라보고 있는 방향 값을 가진 변수 필요 기능 22.05.16 승주
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
        //player move value 기능 22.05.16 승주
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //체크 버튼 기능 22.05.16 승주
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        //버튼 다운으로 수평이동 체크 기능 22.05.16 승주
        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = h != 0;

        //Aniation 기능 22.05.16 승주
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

        //Direction 기능 22.05.16 승주
        if (vDown && v == 1) dirVec = Vector3.up;
        else if (vDown && v == -1) dirVec = Vector3.down;
        else if (hDown && h == -1) dirVec = Vector3.left;
        else if (hDown && h == 1) dirVec = Vector3.right;

        //Scan Object 기능 22.05.16 승주
        if (Input.GetButtonDown("Jump") && scanObject != null)
            manager.Action(scanObject);
    }

    void FixedUpdate()
    {
        //움직이는 기능 22.05.16 승주
        //플래그 변수로 수평, 수직 이동 결정 기능 22.05.16 승주
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);

        rigid2D.velocity = moveVec * Speed;

        //Ray
        Debug.DrawRay(rigid2D.position, dirVec * 1f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid2D.position, dirVec, 1f, LayerMask.GetMask("DailyObj"));

        //앞에 사물이 있는지 확인 하는 기능 22.05.16 승주
        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }

        //앞에 사물이 없는지 확인 하는 기능 22.05.16승주
        else
            scanObject = null;
    }
}
