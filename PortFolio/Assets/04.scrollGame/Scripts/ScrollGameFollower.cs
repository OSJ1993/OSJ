using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollGameFollower : MonoBehaviour
{
    //bullet이 발사 되는 속도 딜레이를 위한 기능 max(최대 실제 딜레이), cur(현재 한발 발사한 후 충전되는 딜레이) 22.04.14 by승주
    public float maxShotDealy;
    public float curShotDelay;

    public ScrollGameObjectManager scrollObjectManager;

    //follow 로직 작성 22.04.14 by승주
    public Vector3 followPos;
    public int followDelay;
    public Transform parent;

    //Queue<> 자료구조 중 하나이며 List<>나 배열과 달리 데이터를 집어넣거나(Enequeue), 빼내는(Dequeue) 두개의 작업으로 데이터 관리하는 기능 22.04.14 by승주
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

    //따라갈 위치를 계속 생신해주는 함수 기능 22.04.14 by승주
    void Watch()
    {
        //Input Pos
        //Queue  == FIFO: First  Input  First  Out 먼저 들어간 것이 먼저 나오는 기능 22.04.14 by승주
        //Queue<> 자료구조 중 하나이며 List<>나 배열과 달리 데이터를 집어넣거나(Enequeue), 빼내는(Dequeue) 두개의 작업으로 데이터 관리하는 기능 22.04.14 by승주
     
        //부모 위치가 가만히 있으면 저장하지 않는 기능 22.04.14 by승주
        if(!parentPos.Contains(parent.position))
        parentPos.Enqueue(parent.position);


        //Output Pos
        //Queue에 일정 데이터 갯수를 채워지면 그 때부터 반환하도록 작성한 기능 22.04.14 by승주
        if (parentPos.Count > followDelay)
            followPos = parentPos.Dequeue();
       //Queue가 채워지기 전까진 부모 위치 적용하는 기능 22.04.14 by승주
        else if (parentPos.Count < followDelay)
            followPos = parent.position;
    }

    void Follow()
    {
        transform.position = followPos;
    }

    void Fire()
    {
        //Button을 누르지 않았을 경우 Bullet이 나가지 않게 하는 기능 22.04.07 by승주
        if (!Input.GetButton("Fire1"))
            return;

        //curShotDelay(현재) Shot 딜레이가 maxShotDelay를 넘지 않았다면 장전이 안된 걸 알 수 있게 해주는 기능 22.04.07 by승주
        if (curShotDelay < maxShotDealy)
            return;

        //bullet의 위치를 지정 해주는 기능 22.04.07 by승주
        GameObject bullet = scrollObjectManager.MakeObj("BulletFollower");
        bullet.transform.position = transform.position;

        //Rigidbody2D를 가져와 Addforce()로 총알 발사를 시켜주는 기능 22.04.07 by승주
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        //bullet을 다 발사 헀을 경우 다시 재장전을 위해 딜레이 변수를 0으로 초기화 시키는 기능 22.04.07 by승주
        curShotDelay = 0;
    }

    //Bullet 발사 후 장전 하는 기능 22.04.07 by승주
    void Reload()
    {

        curShotDelay += Time.deltaTime;
    }

}
