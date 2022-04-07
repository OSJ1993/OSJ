using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    //Player가 4방향 경계에 닿았다는 걸 알려주는 기능 22.04.07 by승주
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    //Player의 speed(속도)를 지정해주는 기능 22.04.07 by승주
    public float speed;

    //Player의 bulletObjB를 발사하기 위해 power값 기능 22.04.07 by승주
    public float power;

    //bullet이 발사 되는 속도 딜레이를 위한 기능 max(최대 실제 딜레이), cur(현재 한발 발사한 후 충전되는 딜레이) 22.04.07 by승주
    public float maxShotDealy;
    public float curShotDelay;

    //bullet Prefab을 저장할 수 있는 기능 22.04.07 by승주
    public GameObject bulletObjA;
    public GameObject bulletObjB;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Fire();
        Reload();
    }

    //Player 움직임 기능 22.04.07 by승주
    void Move()
    {
        //GetAxisRaw()를 통한 방향 값 추출 22.04.07 by승주
        float h = Input.GetAxisRaw("Horizontal");

        //플래그 변수를 사용하여 경계 이상 넘지 못하도록 값을 제한 하는 기능 22.04.07 by승주
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1)) h = 0;

        float v = Input.GetAxisRaw("Vertical");

        //플래그 변수를 사용하여 경계 이상 넘지 못하도록 값을 제한 하는 기능 22.04.07 by승주
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1)) v = 0;

        //Player의 현재 위치를 가져오는 기능 22.04.07 by승주
        Vector3 curPos = transform.position;

        //물리적인 이동인 아닌 transform 이동에는 Time.DeltaTimne을 사용해야한다 22.04.07 by승주
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        //Player가 다음에 이동할 위치와 속도를 지정해주는 기능 22.04.07 by승주
        transform.position = curPos + nextPos;


        //key를 눌렀을 때 key를 땠을 때 Animation 작동 시켜주는 기능 22.04.07 by승주
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)h);
        }

    }

    //Bullet 발사 기능 22.04.07 by승주
    void Fire()
    {
        //Button을 누르지 않았을 경우 Bullet이 나가지 않게 하는 기능 22.04.07 by승주
        if (!Input.GetButton("Fire1"))
            return;

        //curShotDelay(현재) Shot 딜레이가 maxShotDelay를 넘지 않았다면 장전이 안된 걸 알 수 있게 해주는 기능 22.04.07 by승주
        if (curShotDelay < maxShotDealy)
            return;

        switch (power)
        {

            //power one 한발 짜리 파워(기본 한발 bullet) 22.04.07 by승주
            case 1:

                //Instantiate() 매개변수 오브젝트를 생성하는 함수 22.04.07 by승주
                //bullet의 위치를 지정 해주는 기능 22.04.07 by승주
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);

                //Rigidbody2D를 가져와 Addforce()로 총알 발사를 시켜주는 기능 22.04.07 by승주
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                break;


            //power two 두발 짜리 파워(기본 두발 bullet) 22.04.07 by승주
            case 2:

                //Instantiate() 매개변수 오브젝트를 생성하는 함수 22.04.07 by승주
                //bullet의 위치를 지정 해주는 기능 그냥 쏘면 위치가 겹치기 때문에 Vector3.right * 0.1f, Vector3.left * 0.1f로 총알 위치를 오른쪽, 왼쪽으로 이동시켜서 발사 시켜주는 기능  22.04.07 by승주
                GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.1f, transform.rotation);
                GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.1f, transform.rotation);

                //Rigidbody2D를 가져와 Addforce()로 총알 발사를 시켜주는 기능 22.04.07 by승주
                //bullet의 위치를 지정 해주는 기능 그냥 쏘면 위치가 겹치기 때문에 Vector3.right * 0.1f, Vector3.left * 0.1f로 총알 위치를 오른쪽, 왼쪽으로 이동시켜서 발사 시켜주는 기능  22.04.07 by승주
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);


                break;

                //power three 세발 짜리 파워(기본 두발 + 강한 한발 bullet) 22.04.07 by승주
            case 3:

                //Instantiate() 매개변수 오브젝트를 생성하는 함수 22.04.07 by승주
                //bullet의 위치를 지정 해주는 기능 그냥 쏘면 위치가 겹치기 때문에 Vector3.right * 0.1f, Vector3.left * 0.1f로 총알 위치를 오른쪽, 왼쪽으로 이동시켜서 발사 시켜주는 기능  22.04.07 by승주
                GameObject bulletRR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.35f, transform.rotation);
                GameObject bulletCC = Instantiate(bulletObjB, transform.position, transform.rotation);
                GameObject bulletLL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.35f, transform.rotation);


                //Rigidbody2D를 가져와 Addforce()로 총알 발사를 시켜주는 기능 22.04.07 by승주
                //bullet의 위치를 지정 해주는 기능 그냥 쏘면 위치가 겹치기 때문에 Vector3.right * 0.1f, Vector3.left * 0.1f로 총알 위치를 오른쪽, 왼쪽으로 이동시켜서 발사 시켜주는 기능  22.04.07 by승주
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();


                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);


                break;

        }


        //bullet을 다 발사 헀을 경우 다시 재장전을 위해 딜레이 변수를 0으로 초기화 시키는 기능 22.04.07 by승주
        curShotDelay = 0;
    }


    //Bullet 발사 후 장전 하는 기능 22.04.07 by승주
    void Reload()
    {

        curShotDelay += Time.deltaTime;
    }

    //Player가 Boder Collider2D에 닿으면 뚫리지 않게 해주는 기능 22.04.07 by승주
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }
}
