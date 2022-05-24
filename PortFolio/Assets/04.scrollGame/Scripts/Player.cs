using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//IPointerDownHandler =터치 시작 기능     22.04.25 승주
//IPointerUpHandler     =터치 종료 기능     22.04.25 승주
//IDragHandler            =드래그 감지 기능  2205.25 승주


public class Player : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    //---------------------------------------------------------------------
    public RectTransform pad;
    public RectTransform stick;
    //---------------------------------------------------------------------

    //Player가 4방향 경계에 닿았다는 걸 알려주는 기능 22.04.07 by승주
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    public int life;
    public int score;

    //Player의 speed(속도)를 지정해주는 기능 22.04.07 by승주
    public float speed;

    //Player의 power 최대값 현재값 기능 22.04.07 by승주
    public int maxPower;
    public int power;

    //player의 boom 최대값 현재값 기능 22.04.11 by승주
    public int maxBoom;
    public int boom;

    //bullet이 발사 되는 속도 딜레이를 위한 기능 max(최대 실제 딜레이), cur(현재 한발 발사한 후 충전되는 딜레이) 22.04.07 by승주
    public float maxShotDealy;
    public float curShotDelay;

    //bullet Prefab을 저장할 수 있는 기능 22.04.07 by승주
    public GameObject bulletObjA;
    public GameObject bulletObjB;

    public GameObject boomEffect;

    public ScrollGameManager scrollGameManager;
    public ScrollGameObjectManager scrollObjectManager;

    //피격 중복 방지 위한 기능 22.04.11 by승주
    public bool isHit;

    public bool isBoomTime;

    public GameObject[] followers;

    //player가 죽고 respawn됬을 때 잠깐 무적상태 기능 22.04.21 by승주
    public bool isRespawnTime;

    Animator anim;

    SpriteRenderer spriteRenderer;

    //어디를 눌렀는 지 알려주는 기능 22.04.21 by승주
    public bool[] joyControl;

    //버튼을 눌렀는 지 알려주는 기능 22.04.21 by승주 
    public bool isControl;

    public bool isButtonA;
    public bool isButtonB;

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {

        Unbeatable();
        Invoke("Unbeatable", 3);
    }

    void Unbeatable()
    {

        isRespawnTime = !isRespawnTime;
        if (isRespawnTime)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);

            for (int index = 0; index < followers.Length; index++)
            {
                followers[index].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }

        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
            for (int index = 0; index < followers.Length; index++)
            {
                followers[index].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }

        }

    }

    void Update()
    {
        Move();
        Fire();
        Boom();
        Reload();
    }

    public void JoyPanel(int type)
    {
        for (int index = 0; index < 9; index++)
        {
            joyControl[index] = index == type;
        }
    }

    public void JoyDown()
    {
        isControl = true;
    }

    public void JoyUp()
    {
        isControl = false;
    }

    //Player 움직임 기능 22.04.07 by승주
    void Move()
    {
        //GetAxisRaw()를 통한 방향 값 추출 22.04.07 by승주
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (joyControl[0] ||
            joyControl[1] ||
            joyControl[2] ||
            joyControl[3] ||
            joyControl[4] ||
            joyControl[5] ||
            joyControl[6] ||
            joyControl[7] ||
            joyControl[8]
            )
        {
            //Joy control Value 22.04.21 by 승주
            if (joyControl[0]) { h = -1; v = 1; }
            if (joyControl[1]) { h = 0; v = 1; }
            if (joyControl[2]) { h = 1; v = 1; }
            if (joyControl[3]) { h = -1; v = 0; }
            if (joyControl[4]) { h = 0; v = 0; }
            if (joyControl[5]) { h = 1; v = 0; }
            if (joyControl[6]) { h = -1; v = -1; }
            if (joyControl[7]) { h = 0; v = -1; }
            if (joyControl[8]) { h = 1; v = -1; }


        }
        {

            anim.SetInteger("Input", (int)h);

        }

        //플래그 변수를 사용하여 경계 이상 넘지 못하도록 값을 제한 하는 기능 22.04.07 by승주
        //if ((isTouchRight && h == 1) || (isTouchLeft && h == -1) || !isControl) h = 0;


        //플래그 변수를 사용하여 경계 이상 넘지 못하도록 값을 제한 하는 기능 22.04.07 by승주
        //if ((isTouchTop && v == 1) || (isTouchBottom && v == -1 || !isControl)) v = 0;

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

    public void ButtonADown()
    {
        isButtonA = true;
    }

    public void ButtonAUp()
    {
        isButtonA = false;
    }

    public void ButtonBDown()
    {
        isButtonB = true;
    } 
    public void ButtonBUp()
    {
        isButtonB = false;
    }

    //Bullet 발사 기능 22.04.07 by승주
    void Fire()
    {
        //Button을 누르지 않았을 경우 Bullet이 나가지 않게 하는 기능 22.04.07 by승주
        //if (!Input.GetButton("Fire1"))
        //   return;

        if (!isButtonA)
            return;

        //curShotDelay(현재) Shot 딜레이가 maxShotDelay를 넘지 않았다면 장전이 안된 걸 알 수 있게 해주는 기능 22.04.07 by승주
        if (curShotDelay < maxShotDealy)
            return;

        switch (power)
        {

            //power one 한발 짜리 파워(기본 한발 bullet) 22.04.07 by승주
            case 1:


                //bullet의 위치를 지정 해주는 기능 22.04.07 by승주
                GameObject bullet = scrollObjectManager.MakeObj("BulletPlayerA");
                bullet.transform.position = transform.position;

                //Rigidbody2D를 가져와 Addforce()로 총알 발사를 시켜주는 기능 22.04.07 by승주
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                break;


            //power two 두발 짜리 파워(기본 두발 bullet) 22.04.07 by승주
            case 2:


                //bullet의 위치를 지정 해주는 기능 그냥 쏘면 위치가 겹치기 때문에 Vector3.right * 0.1f, Vector3.left * 0.1f로 총알 위치를 오른쪽, 왼쪽으로 이동시켜서 발사 시켜주는 기능  22.04.07 by승주
                GameObject bulletR = scrollObjectManager.MakeObj("BulletPlayerA");
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;


                GameObject bulletL = scrollObjectManager.MakeObj("BulletPlayerA");
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;


                //Rigidbody2D를 가져와 Addforce()로 총알 발사를 시켜주는 기능 22.04.07 by승주
                //bullet의 위치를 지정 해주는 기능 그냥 쏘면 위치가 겹치기 때문에 Vector3.right * 0.1f, Vector3.left * 0.1f로 총알 위치를 오른쪽, 왼쪽으로 이동시켜서 발사 시켜주는 기능  22.04.07 by승주
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);


                break;

            //power three 세발 짜리 파워(기본 두발 + 강한 한발 bullet) 22.04.07 by승주
            default:

                //bullet의 위치를 지정 해주는 기능 그냥 쏘면 위치가 겹치기 때문에 Vector3.right * 0.1f, Vector3.left * 0.1f로 총알 위치를 오른쪽, 왼쪽으로 이동시켜서 발사 시켜주는 기능  22.04.07 by승주
                GameObject bulletRR = scrollObjectManager.MakeObj("BulletPlayerA");
                bulletRR.transform.position = transform.position + Vector3.right * 0.35f;


                GameObject bulletCC = scrollObjectManager.MakeObj("BulletPlayerB");
                bulletCC.transform.position = transform.position;


                GameObject bulletLL = scrollObjectManager.MakeObj("BulletPlayerA");
                bulletLL.transform.position = transform.position + Vector3.left * 0.35f;



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


    void Boom()
    {

        //if (!Input.GetButton("Fire2"))
        //return;

        if (!isButtonB)
            return;

        //이미 폭탄을 쏘고 있는 지 확인 하기 위한 기능 22.04.11 by승주
        if (isBoomTime)
           
            return;

        if (boom == 0)
            return;

        boom--;
        isBoomTime = true;
        scrollGameManager.UpdateBoomIcon(boom);
        


        //boomEffect 보이게 해주는 기능 22.04.11 by승주
        boomEffect.SetActive(true);
        

        //boom Sprite 시간차 비활성화 시키는 기능 22.04.12 by승주
        Invoke("OffBoomEffect", 3f);

        //Boom 맞고 Enemy 제거 하는 기능 22.04.11 by승주
        GameObject[] enemiesL = scrollObjectManager.GetPool("EnemyL");
        GameObject[] enemiesM = scrollObjectManager.GetPool("EnemyM");
        GameObject[] enemiesS = scrollObjectManager.GetPool("EnemyS");

        for (int index = 0; index < enemiesL.Length; index++)
        {
            if (enemiesL[index].activeSelf)
            {

                Enemy enemyLogic = enemiesL[index].GetComponent<Enemy>();

                //enemy에게 dmg를 주는 기능 22.04.11 by승주
                enemyLogic.OnHit(1000);
            }
        }
        for (int index = 0; index < enemiesM.Length; index++)
        {
            if (enemiesM[index].activeSelf)
            {

                Enemy enemyLogic = enemiesM[index].GetComponent<Enemy>();

                //enemy에게 dmg를 주는 기능 22.04.11 by승주
                enemyLogic.OnHit(1000);
            }
        }
        for (int index = 0; index < enemiesS.Length; index++)
        {
            if (enemiesS[index].activeSelf)
            {

                Enemy enemyLogic = enemiesS[index].GetComponent<Enemy>();

                //enemy에게 dmg를 주는 기능 22.04.11 by승주
                enemyLogic.OnHit(1000);

            }
            
        }


        //Boom 맞고 Enemy의 bullet 제거 22.04.11 by승주
        GameObject[] bulletsA = scrollObjectManager.GetPool("BulletEnemyA");
        GameObject[] bulletsB = scrollObjectManager.GetPool("BulletEnemyB");

        for (int index = 0; index < bulletsA.Length; index++)
            if (bulletsA[index].activeSelf)
            {
                bulletsA[index].SetActive(false);

            }
        for (int index = 0; index < bulletsB.Length; index++)
            if (bulletsB[index].activeSelf)
            {
                bulletsB[index].SetActive(false);

            }


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

        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            //RespawnTime때 맞지 않게 하는 기능 22.04.21 by승주
            if (isRespawnTime)
                return;


            //한번에 두번 맞는 걸 못 하게 하기 위한 기능 22.04.11 by승주
            //Hit시 로직 실행 못 하게 하는 기능 22.04.11 by승주
            if (isHit)
                return;

            //Hit가 아니면 로직 실행 시키는 기능 22.04.11 by승주
            isHit = true;

            //player의 life 기능 ScrollGameManager에 UpdateLifeIcon를 받아서 실행(UI부분) 22.04.11 by승주
            life--;
            scrollGameManager.UpdateLifeIcon(life);
            scrollGameManager.CallExplosion(transform.position, "P");

            //만약에 life가 zero라면 GameOver 실행 시키는 기능 22.04.11 by승주
            if (life == 0)
            {
                scrollGameManager.GameOver();
            }
            //life가 zero가 아니고 life가 남아있다면 RespawnPlayer를 실행 시키는 기능  22.04.11 by승주
            else
            {
                //player파괴 되지 않고 player 목숨 잃고 다시 나오는 기능 22.04.08 by승주
                //player 복귀 시키는 기능은 ScroolGameManager에서 22.04.08 by승주

                scrollGameManager.RespawnPlayer();

            }


            gameObject.SetActive(false);


        }

        //item 기능 22.04.11 by승주
        else if (collision.gameObject.tag == "Item")
        {
            ScrollGameItem item = collision.gameObject.GetComponent<ScrollGameItem>();

            switch (item.type)
            {
                case "Coin":
                    score += 1000;
                    break;

                case "Power":
                    //power와 maxPower가 같다면 점수를 올려주는 기능 22.04.11 by승주
                    //maxPower로 무한정 power가 강해지는 것을 막아주는 기능 22.04.11 by승주
                    if (power == maxPower)
                        score += 500;
                    //maxPower가 아닐 경우 power를 up 시켜주는 기능 22.04.11 by승주
                    else
                    {
                        power++;
                        AddFollower();
                    }
                    break;

                //필살기 기능 22.04.11 by승주
                case "Boom":
                    if (boom == maxBoom)
                        score += 700;
                    else
                    {
                        boom++;
                        scrollGameManager.UpdateBoomIcon(boom);
                    }

                    break;
            }

            //item 먹으면 item 삭제 시키는 기능 22.04.11 by승주
            collision.gameObject.SetActive(false);
        }

    }

    void OffBoomEffect()
    {
        boomEffect.SetActive(false);
        isBoomTime = false;
    }

    void AddFollower()
    {
        //player의 power가 4라면 첫번쨰 follower를 활성화 시키는 기능 22.04.14 by승주
        if (power == 4)
            followers[0].SetActive(true);
        else if (power == 5)
            followers[1].SetActive(true);
        else if (power == 6)
            followers[2].SetActive(true);
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

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
