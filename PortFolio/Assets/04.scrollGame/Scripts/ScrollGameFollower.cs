using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollGameFollower : MonoBehaviour
{
    //bullet�� �߻� �Ǵ� �ӵ� �����̸� ���� ��� max(�ִ� ���� ������), cur(���� �ѹ� �߻��� �� �����Ǵ� ������) 22.04.14 by����
    public float maxShotDealy;
    public float curShotDelay;

    public ScrollGameObjectManager scrollObjectManager;

    //follow ���� �ۼ� 22.04.14 by����
    public Vector3 followPos;
    public int followDelay;
    public Transform parent;

    //Queue<> �ڷᱸ�� �� �ϳ��̸� List<>�� �迭�� �޸� �����͸� ����ְų�(Enequeue), ������(Dequeue) �ΰ��� �۾����� ������ �����ϴ� ��� 22.04.14 by����
    public Queue<Vector3> parentPos;

    private void Awake()
    {
        parentPos = new Queue<Vector3>();
    }

    void Update()
    {
        Watch();
        Follow();
        Fire();
        Reload();
    }

    //���� ��ġ�� ��� �������ִ� �Լ� ��� 22.04.14 by����
    void Watch()
    {
        //Input Pos
        //Queue  == FIFO: First  Input  First  Out ���� �� ���� ���� ������ ��� 22.04.14 by����
        //Queue<> �ڷᱸ�� �� �ϳ��̸� List<>�� �迭�� �޸� �����͸� ����ְų�(Enequeue), ������(Dequeue) �ΰ��� �۾����� ������ �����ϴ� ��� 22.04.14 by����
     
        //�θ� ��ġ�� ������ ������ �������� �ʴ� ��� 22.04.14 by����
        if(!parentPos.Contains(parent.position))
        parentPos.Enqueue(parent.position);


        //Output Pos
        //Queue�� ���� ������ ������ ä������ �� ������ ��ȯ�ϵ��� �ۼ��� ��� 22.04.14 by����
        if (parentPos.Count > followDelay)
            followPos = parentPos.Dequeue();
       //Queue�� ä������ ������ �θ� ��ġ �����ϴ� ��� 22.04.14 by����
        else if (parentPos.Count < followDelay)
            followPos = parent.position;
    }

    void Follow()
    {
        transform.position = followPos;
    }

    void Fire()
    {
        //Button�� ������ �ʾ��� ��� Bullet�� ������ �ʰ� �ϴ� ��� 22.04.07 by����
        if (!Input.GetButton("Fire1"))
            return;

        //curShotDelay(����) Shot �����̰� maxShotDelay�� ���� �ʾҴٸ� ������ �ȵ� �� �� �� �ְ� ���ִ� ��� 22.04.07 by����
        if (curShotDelay < maxShotDealy)
            return;

        //bullet�� ��ġ�� ���� ���ִ� ��� 22.04.07 by����
        GameObject bullet = scrollObjectManager.MakeObj("BulletFollower");
        bullet.transform.position = transform.position;

        //Rigidbody2D�� ������ Addforce()�� �Ѿ� �߻縦 �����ִ� ��� 22.04.07 by����
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        //bullet�� �� �߻� ���� ��� �ٽ� �������� ���� ������ ������ 0���� �ʱ�ȭ ��Ű�� ��� 22.04.07 by����
        curShotDelay = 0;
    }

    //Bullet �߻� �� ���� �ϴ� ��� 22.04.07 by����
    void Reload()
    {

        curShotDelay += Time.deltaTime;
    }

}
