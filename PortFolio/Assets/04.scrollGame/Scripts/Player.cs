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

    //Player�� bulletObjB�� �߻��ϱ� ���� power�� ��� 22.04.07 by����
    public int maxPower;
    public int power;

    //bullet�� �߻� �Ǵ� �ӵ� �����̸� ���� ��� max(�ִ� ���� ������), cur(���� �ѹ� �߻��� �� �����Ǵ� ������) 22.04.07 by����
    public float maxShotDealy;
    public float curShotDelay;

    //bullet Prefab�� ������ �� �ִ� ��� 22.04.07 by����
    public GameObject bulletObjA;
    public GameObject bulletObjB;

    public ScrollGameManager scrollGameManager;

    //�ǰ� �ߺ� ���� ���� ��� 22.04.11 by����
    public bool isHit;

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

                //Instantiate() �Ű����� ������Ʈ�� �����ϴ� �Լ� 22.04.07 by����
                //bullet�� ��ġ�� ���� ���ִ� ��� 22.04.07 by����
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);

                //Rigidbody2D�� ������ Addforce()�� �Ѿ� �߻縦 �����ִ� ��� 22.04.07 by����
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                break;


            //power two �ι� ¥�� �Ŀ�(�⺻ �ι� bullet) 22.04.07 by����
            case 2:

                //Instantiate() �Ű����� ������Ʈ�� �����ϴ� �Լ� 22.04.07 by����
                //bullet�� ��ġ�� ���� ���ִ� ��� �׳� ��� ��ġ�� ��ġ�� ������ Vector3.right * 0.1f, Vector3.left * 0.1f�� �Ѿ� ��ġ�� ������, �������� �̵����Ѽ� �߻� �����ִ� ���  22.04.07 by����
                GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.1f, transform.rotation);
                GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.1f, transform.rotation);

                //Rigidbody2D�� ������ Addforce()�� �Ѿ� �߻縦 �����ִ� ��� 22.04.07 by����
                //bullet�� ��ġ�� ���� ���ִ� ��� �׳� ��� ��ġ�� ��ġ�� ������ Vector3.right * 0.1f, Vector3.left * 0.1f�� �Ѿ� ��ġ�� ������, �������� �̵����Ѽ� �߻� �����ִ� ���  22.04.07 by����
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);


                break;

            //power three ���� ¥�� �Ŀ�(�⺻ �ι� + ���� �ѹ� bullet) 22.04.07 by����
            case 3:

                //Instantiate() �Ű����� ������Ʈ�� �����ϴ� �Լ� 22.04.07 by����
                //bullet�� ��ġ�� ���� ���ִ� ��� �׳� ��� ��ġ�� ��ġ�� ������ Vector3.right * 0.1f, Vector3.left * 0.1f�� �Ѿ� ��ġ�� ������, �������� �̵����Ѽ� �߻� �����ִ� ���  22.04.07 by����
                GameObject bulletRR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.35f, transform.rotation);
                GameObject bulletCC = Instantiate(bulletObjB, transform.position, transform.rotation);
                GameObject bulletLL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.35f, transform.rotation);


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
            Destroy(collision.gameObject);

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
