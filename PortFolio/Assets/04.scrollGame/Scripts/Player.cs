using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//IPointerDownHandler =��ġ ���� ���     22.04.25 ����
//IPointerUpHandler     =��ġ ���� ���     22.04.25 ����
//IDragHandler            =�巡�� ���� ���  2205.25 ����


public class Player : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    //---------------------------------------------------------------------
    public RectTransform pad;
    public RectTransform stick;
    //---------------------------------------------------------------------

    //Player�� 4���� ��迡 ��Ҵٴ� �� �˷��ִ� ��� 22.04.07 by����
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    public int life;
    public int score;

    //Player�� speed(�ӵ�)�� �������ִ� ��� 22.04.07 by����
    public float speed;

    //Player�� power �ִ밪 ���簪 ��� 22.04.07 by����
    public int maxPower;
    public int power;

    //player�� boom �ִ밪 ���簪 ��� 22.04.11 by����
    public int maxBoom;
    public int boom;

    //bullet�� �߻� �Ǵ� �ӵ� �����̸� ���� ��� max(�ִ� ���� ������), cur(���� �ѹ� �߻��� �� �����Ǵ� ������) 22.04.07 by����
    public float maxShotDealy;
    public float curShotDelay;

    //bullet Prefab�� ������ �� �ִ� ��� 22.04.07 by����
    public GameObject bulletObjA;
    public GameObject bulletObjB;

    public GameObject boomEffect;

    public ScrollGameManager scrollGameManager;
    public ScrollGameObjectManager scrollObjectManager;

    //�ǰ� �ߺ� ���� ���� ��� 22.04.11 by����
    public bool isHit;

    public bool isBoomTime;

    public GameObject[] followers;

    //player�� �װ� respawn���� �� ��� �������� ��� 22.04.21 by����
    public bool isRespawnTime;

    Animator anim;

    SpriteRenderer spriteRenderer;

    //��� ������ �� �˷��ִ� ��� 22.04.21 by����
    public bool[] joyControl;

    //��ư�� ������ �� �˷��ִ� ��� 22.04.21 by���� 
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

    //Player ������ ��� 22.04.07 by����
    void Move()
    {
        //GetAxisRaw()�� ���� ���� �� ���� 22.04.07 by����
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
            //Joy control Value 22.04.21 by ����
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

        //�÷��� ������ ����Ͽ� ��� �̻� ���� ���ϵ��� ���� ���� �ϴ� ��� 22.04.07 by����
        //if ((isTouchRight && h == 1) || (isTouchLeft && h == -1) || !isControl) h = 0;


        //�÷��� ������ ����Ͽ� ��� �̻� ���� ���ϵ��� ���� ���� �ϴ� ��� 22.04.07 by����
        //if ((isTouchTop && v == 1) || (isTouchBottom && v == -1 || !isControl)) v = 0;

        //Player�� ���� ��ġ�� �������� ��� 22.04.07 by����
        Vector3 curPos = transform.position;

        //�������� �̵��� �ƴ� transform �̵����� Time.DeltaTimne�� ����ؾ��Ѵ� 22.04.07 by����
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        //Player�� ������ �̵��� ��ġ�� �ӵ��� �������ִ� ��� 22.04.07 by����
        transform.position = curPos + nextPos;


        //key�� ������ �� key�� ���� �� Animation �۵� �����ִ� ��� 22.04.07 by����
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

    //Bullet �߻� ��� 22.04.07 by����
    void Fire()
    {
        //Button�� ������ �ʾ��� ��� Bullet�� ������ �ʰ� �ϴ� ��� 22.04.07 by����
        //if (!Input.GetButton("Fire1"))
        //   return;

        if (!isButtonA)
            return;

        //curShotDelay(����) Shot �����̰� maxShotDelay�� ���� �ʾҴٸ� ������ �ȵ� �� �� �� �ְ� ���ִ� ��� 22.04.07 by����
        if (curShotDelay < maxShotDealy)
            return;

        switch (power)
        {

            //power one �ѹ� ¥�� �Ŀ�(�⺻ �ѹ� bullet) 22.04.07 by����
            case 1:


                //bullet�� ��ġ�� ���� ���ִ� ��� 22.04.07 by����
                GameObject bullet = scrollObjectManager.MakeObj("BulletPlayerA");
                bullet.transform.position = transform.position;

                //Rigidbody2D�� ������ Addforce()�� �Ѿ� �߻縦 �����ִ� ��� 22.04.07 by����
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                break;


            //power two �ι� ¥�� �Ŀ�(�⺻ �ι� bullet) 22.04.07 by����
            case 2:


                //bullet�� ��ġ�� ���� ���ִ� ��� �׳� ��� ��ġ�� ��ġ�� ������ Vector3.right * 0.1f, Vector3.left * 0.1f�� �Ѿ� ��ġ�� ������, �������� �̵����Ѽ� �߻� �����ִ� ���  22.04.07 by����
                GameObject bulletR = scrollObjectManager.MakeObj("BulletPlayerA");
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;


                GameObject bulletL = scrollObjectManager.MakeObj("BulletPlayerA");
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;


                //Rigidbody2D�� ������ Addforce()�� �Ѿ� �߻縦 �����ִ� ��� 22.04.07 by����
                //bullet�� ��ġ�� ���� ���ִ� ��� �׳� ��� ��ġ�� ��ġ�� ������ Vector3.right * 0.1f, Vector3.left * 0.1f�� �Ѿ� ��ġ�� ������, �������� �̵����Ѽ� �߻� �����ִ� ���  22.04.07 by����
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);


                break;

            //power three ���� ¥�� �Ŀ�(�⺻ �ι� + ���� �ѹ� bullet) 22.04.07 by����
            default:

                //bullet�� ��ġ�� ���� ���ִ� ��� �׳� ��� ��ġ�� ��ġ�� ������ Vector3.right * 0.1f, Vector3.left * 0.1f�� �Ѿ� ��ġ�� ������, �������� �̵����Ѽ� �߻� �����ִ� ���  22.04.07 by����
                GameObject bulletRR = scrollObjectManager.MakeObj("BulletPlayerA");
                bulletRR.transform.position = transform.position + Vector3.right * 0.35f;


                GameObject bulletCC = scrollObjectManager.MakeObj("BulletPlayerB");
                bulletCC.transform.position = transform.position;


                GameObject bulletLL = scrollObjectManager.MakeObj("BulletPlayerA");
                bulletLL.transform.position = transform.position + Vector3.left * 0.35f;



                //Rigidbody2D�� ������ Addforce()�� �Ѿ� �߻縦 �����ִ� ��� 22.04.07 by����
                //bullet�� ��ġ�� ���� ���ִ� ��� �׳� ��� ��ġ�� ��ġ�� ������ Vector3.right * 0.1f, Vector3.left * 0.1f�� �Ѿ� ��ġ�� ������, �������� �̵����Ѽ� �߻� �����ִ� ���  22.04.07 by����
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();


                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);


                break;

        }


        //bullet�� �� �߻� ���� ��� �ٽ� �������� ���� ������ ������ 0���� �ʱ�ȭ ��Ű�� ��� 22.04.07 by����
        curShotDelay = 0;
    }


    //Bullet �߻� �� ���� �ϴ� ��� 22.04.07 by����
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

        //�̹� ��ź�� ��� �ִ� �� Ȯ�� �ϱ� ���� ��� 22.04.11 by����
        if (isBoomTime)
           
            return;

        if (boom == 0)
            return;

        boom--;
        isBoomTime = true;
        scrollGameManager.UpdateBoomIcon(boom);
        


        //boomEffect ���̰� ���ִ� ��� 22.04.11 by����
        boomEffect.SetActive(true);
        

        //boom Sprite �ð��� ��Ȱ��ȭ ��Ű�� ��� 22.04.12 by����
        Invoke("OffBoomEffect", 3f);

        //Boom �°� Enemy ���� �ϴ� ��� 22.04.11 by����
        GameObject[] enemiesL = scrollObjectManager.GetPool("EnemyL");
        GameObject[] enemiesM = scrollObjectManager.GetPool("EnemyM");
        GameObject[] enemiesS = scrollObjectManager.GetPool("EnemyS");

        for (int index = 0; index < enemiesL.Length; index++)
        {
            if (enemiesL[index].activeSelf)
            {

                Enemy enemyLogic = enemiesL[index].GetComponent<Enemy>();

                //enemy���� dmg�� �ִ� ��� 22.04.11 by����
                enemyLogic.OnHit(1000);
            }
        }
        for (int index = 0; index < enemiesM.Length; index++)
        {
            if (enemiesM[index].activeSelf)
            {

                Enemy enemyLogic = enemiesM[index].GetComponent<Enemy>();

                //enemy���� dmg�� �ִ� ��� 22.04.11 by����
                enemyLogic.OnHit(1000);
            }
        }
        for (int index = 0; index < enemiesS.Length; index++)
        {
            if (enemiesS[index].activeSelf)
            {

                Enemy enemyLogic = enemiesS[index].GetComponent<Enemy>();

                //enemy���� dmg�� �ִ� ��� 22.04.11 by����
                enemyLogic.OnHit(1000);

            }
            
        }


        //Boom �°� Enemy�� bullet ���� 22.04.11 by����
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
    //Player�� Boder Collider2D�� ������ �ո��� �ʰ� ���ִ� ��� 22.04.07 by����
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
            //RespawnTime�� ���� �ʰ� �ϴ� ��� 22.04.21 by����
            if (isRespawnTime)
                return;


            //�ѹ��� �ι� �´� �� �� �ϰ� �ϱ� ���� ��� 22.04.11 by����
            //Hit�� ���� ���� �� �ϰ� �ϴ� ��� 22.04.11 by����
            if (isHit)
                return;

            //Hit�� �ƴϸ� ���� ���� ��Ű�� ��� 22.04.11 by����
            isHit = true;

            //player�� life ��� ScrollGameManager�� UpdateLifeIcon�� �޾Ƽ� ����(UI�κ�) 22.04.11 by����
            life--;
            scrollGameManager.UpdateLifeIcon(life);
            scrollGameManager.CallExplosion(transform.position, "P");

            //���࿡ life�� zero��� GameOver ���� ��Ű�� ��� 22.04.11 by����
            if (life == 0)
            {
                scrollGameManager.GameOver();
            }
            //life�� zero�� �ƴϰ� life�� �����ִٸ� RespawnPlayer�� ���� ��Ű�� ���  22.04.11 by����
            else
            {
                //player�ı� ���� �ʰ� player ��� �Ұ� �ٽ� ������ ��� 22.04.08 by����
                //player ���� ��Ű�� ����� ScroolGameManager���� 22.04.08 by����

                scrollGameManager.RespawnPlayer();

            }


            gameObject.SetActive(false);


        }

        //item ��� 22.04.11 by����
        else if (collision.gameObject.tag == "Item")
        {
            ScrollGameItem item = collision.gameObject.GetComponent<ScrollGameItem>();

            switch (item.type)
            {
                case "Coin":
                    score += 1000;
                    break;

                case "Power":
                    //power�� maxPower�� ���ٸ� ������ �÷��ִ� ��� 22.04.11 by����
                    //maxPower�� ������ power�� �������� ���� �����ִ� ��� 22.04.11 by����
                    if (power == maxPower)
                        score += 500;
                    //maxPower�� �ƴ� ��� power�� up �����ִ� ��� 22.04.11 by����
                    else
                    {
                        power++;
                        AddFollower();
                    }
                    break;

                //�ʻ�� ��� 22.04.11 by����
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

            //item ������ item ���� ��Ű�� ��� 22.04.11 by����
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
        //player�� power�� 4��� ù���� follower�� Ȱ��ȭ ��Ű�� ��� 22.04.14 by����
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
