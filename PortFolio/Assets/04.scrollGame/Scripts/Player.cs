using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


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
    public ScrollGameObjectManager scrollobjectManager;

    //�ǰ� �ߺ� ���� ���� ��� 22.04.11 by����
    public bool isHit;

    public bool isBoomTime;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Fire();
        Boom();
        Reload();
    }

    //Player ������ ��� 22.04.07 by����
    void Move()
    {
        //GetAxisRaw()�� ���� ���� �� ���� 22.04.07 by����
        float h = Input.GetAxisRaw("Horizontal");

        //�÷��� ������ ����Ͽ� ��� �̻� ���� ���ϵ��� ���� ���� �ϴ� ��� 22.04.07 by����
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1)) h = 0;

        float v = Input.GetAxisRaw("Vertical");

        //�÷��� ������ ����Ͽ� ��� �̻� ���� ���ϵ��� ���� ���� �ϴ� ��� 22.04.07 by����
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1)) v = 0;

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

    //Bullet �߻� ��� 22.04.07 by����
    void Fire()
    {
        //Button�� ������ �ʾ��� ��� Bullet�� ������ �ʰ� �ϴ� ��� 22.04.07 by����
        if (!Input.GetButton("Fire1"))
            return;

        //curShotDelay(����) Shot �����̰� maxShotDelay�� ���� �ʾҴٸ� ������ �ȵ� �� �� �� �ְ� ���ִ� ��� 22.04.07 by����
        if (curShotDelay < maxShotDealy)
            return;

        switch (power)
        {

            //power one �ѹ� ¥�� �Ŀ�(�⺻ �ѹ� bullet) 22.04.07 by����
            case 1:


                //bullet�� ��ġ�� ���� ���ִ� ��� 22.04.07 by����
                GameObject bullet = scrollobjectManager.MakeObj("BulletPlayerA");
                bullet.transform.position = transform.position;

                //Rigidbody2D�� ������ Addforce()�� �Ѿ� �߻縦 �����ִ� ��� 22.04.07 by����
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                break;


            //power two �ι� ¥�� �Ŀ�(�⺻ �ι� bullet) 22.04.07 by����
            case 2:


                //bullet�� ��ġ�� ���� ���ִ� ��� �׳� ��� ��ġ�� ��ġ�� ������ Vector3.right * 0.1f, Vector3.left * 0.1f�� �Ѿ� ��ġ�� ������, �������� �̵����Ѽ� �߻� �����ִ� ���  22.04.07 by����
                GameObject bulletR = scrollobjectManager.MakeObj("BulletPlayerA");
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;


                GameObject bulletL = scrollobjectManager.MakeObj("BulletPlayerA");
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;


                //Rigidbody2D�� ������ Addforce()�� �Ѿ� �߻縦 �����ִ� ��� 22.04.07 by����
                //bullet�� ��ġ�� ���� ���ִ� ��� �׳� ��� ��ġ�� ��ġ�� ������ Vector3.right * 0.1f, Vector3.left * 0.1f�� �Ѿ� ��ġ�� ������, �������� �̵����Ѽ� �߻� �����ִ� ���  22.04.07 by����
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);


                break;

            //power three ���� ¥�� �Ŀ�(�⺻ �ι� + ���� �ѹ� bullet) 22.04.07 by����
            case 3:

                //bullet�� ��ġ�� ���� ���ִ� ��� �׳� ��� ��ġ�� ��ġ�� ������ Vector3.right * 0.1f, Vector3.left * 0.1f�� �Ѿ� ��ġ�� ������, �������� �̵����Ѽ� �߻� �����ִ� ���  22.04.07 by����
                GameObject bulletRR = scrollobjectManager.MakeObj("BulletPlayerA");
                bulletRR.transform.position = transform.position + Vector3.right * 0.35f;


                GameObject bulletCC = scrollobjectManager.MakeObj("BulletPlayerB");
                bulletCC.transform.position = transform.position;


                GameObject bulletLL = scrollobjectManager.MakeObj("BulletPlayerA");
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
        if (!Input.GetButton("Fire2"))
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
        GameObject[] enemiesL = scrollobjectManager.GetPool("EnemyL");
        GameObject[] enemiesM = scrollobjectManager.GetPool("EnemyM");
        GameObject[] enemiesS = scrollobjectManager.GetPool("EnemyS");

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
        GameObject[] bulletsA = scrollobjectManager.GetPool("BulletEnemyA");
        GameObject[] bulletsB = scrollobjectManager.GetPool("BulletEnemyB");
        
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
            //�ѹ��� �ι� �´� �� �� �ϰ� �ϱ� ���� ��� 22.04.11 by����
            //Hit�� ���� ���� �� �ϰ� �ϴ� ��� 22.04.11 by����
            if (isHit) return;

            //Hit�� �ƴϸ� ���� ���� ��Ű�� ��� 22.04.11 by����
            isHit = true;

            //player�� life ��� ScrollGameManager�� UpdateLifeIcon�� �޾Ƽ� ����(UI�κ�) 22.04.11 by����
            life--;
            scrollGameManager.UpdateLifeIcon(life);

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
                        power++;
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
