using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DailyPlayerMove : MonoBehaviour
{
    public float speed;

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

    //Mobile key 입력 받게 해줄 기능 22.06.09승주
    int up_Value;
    int down_Value;
    int left_Value;
    int right_Value;
    bool up_Down;
    bool down_Down;
    bool left_Down;
    bool right_Down;
    bool up_Up;
    bool down_Up;
    bool left_Up;
    bool right_Up;


    void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    void FixedUpdate()
    {
        //움직이는 기능 22.05.16 승주
        //플래그 변수로 수평, 수직 이동 결정 기능 22.05.16 승주
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);

        rigid2D.velocity = moveVec * speed;

        //Ray
        Debug.DrawRay(rigid2D.position, dirVec * 1f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid2D.position, dirVec, 1f, LayerMask.GetMask("DailyObj"));

        //앞에 사물이 있는지 확인 하는 기능 22.05.16 승주
        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
            if (rayHit.transform.CompareTag("SceneChangeNPC"))
            {
                manager.sceneChangeNPC = true;


            }
        }

        //앞에 사물이 없는지 확인 하는 기능 22.05.16승주
        else
            scanObject = null;
    }



    void Move()
    {
        //player move value 기능 22.05.16 승주
        //PC버젼 +  Mobile 버젼
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal") + right_Value + left_Value;
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical") + up_Value + down_Value;




        // //체크 버튼 기능 22.05.16 승주
        // //상태 변수르 사용하여 player 이동 제한 하는 기능 22.05.16 승주
        //PC버전 + Mobile버전
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal") || right_Down || left_Down;
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical") || up_Down || down_Down;
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal") || right_Up || left_Up;
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical") || up_Up || down_Up;



        // //버튼 다운으로 수평이동 체크 기능 22.05.16 승주
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
        //
        // //Direction 기능 22.05.16 승주
        if (vDown && v == 1) dirVec = Vector3.up;
        else if (vDown && v == -1) dirVec = Vector3.down;
        else if (hDown && h == -1) dirVec = Vector3.left;
        else if (hDown && h == 1) dirVec = Vector3.right;

        //Scan Object 기능 22.05.16 승주
        if (Input.GetButtonDown("Jump") && scanObject != null)
            manager.Action(scanObject);

        //Mobile var 초기화 기능 22.06.09 승주
        up_Down = false;
        down_Down = false;
        left_Down = false;
        right_Down = false;
        up_Up = false;
        down_Up = false;
        left_Up = false;
        right_Up = false;


    }

    public void ButtonDown(string type)
    {
        switch (type)
        {
            case "U":
                up_Value = 1;
                up_Down = true;
                break;

            case "D":
                down_Value = -1;
                down_Down = true;
                break;

            case "L":
                left_Value = -1;
                left_Down = true;
                break;

            case "R":
                right_Value = 1;
                right_Down = true;
                break;

                case "A":
                if (scanObject != null)
                    manager.Action(scanObject);
                break;
        }
    }

    public void ButtonUp(string type)
    {
        switch (type)
        {
            case "U":
                up_Value = 0;
                up_Up = true;
                break;

            case "D":
                down_Value = 0;
                down_Up = true;
                break;

            case "L":
                left_Value = 0;
                left_Up = true;
                break;

            case "R":
                right_Value = 0;
                right_Up = true;
                break;
        }
    }


}
