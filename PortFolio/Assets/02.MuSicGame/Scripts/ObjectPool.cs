using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjcctInfo
{
    //정보. goPrefab 생성시키기.
    public GameObject goPrefab;

    //goPrefab생성시킬 갯수.
    public int count;

    //goPrefab어느 위치에 생성시킬지 설정.
    public Transform tfPoolParent;
}


public class ObjectPool : MonoBehaviour
{

    //ObjectPool은 언제 어디에서나 가져다 쓰고 반납할 수 있게 만들어야함.
    public static ObjectPool instnace;

    [SerializeField] ObjcctInfo[] objectInfo = null;

    //Queue : 선입선출 자료형(가장 먼저 들어간 데이터가 가장 먼저 빠져나옴)
    public Queue<GameObject> noteQueue = new Queue<GameObject>();



    void Start()
    {
        instnace = this;

        //리턴시킨 값을 InsertQueue 넣어주기. 번호순서는 0번쨰.
        noteQueue = InsertQueue(objectInfo[0]);
    }


    // Queue 가져다 쓰기.
    Queue<GameObject> InsertQueue(ObjcctInfo p_objectInfo)
    {
        Queue<GameObject> t_queue = new Queue<GameObject>();

        //카운트
        for (int i = 0; i < p_objectInfo.count; i++)
        {
            //적당한 위치에 생성 어차피 비활성화로 인해 게임에서 보이지 않는다. 필요할 때 위치정보 주는걸로 가져다 쓸것이다.
            GameObject t_clone = Instantiate(p_objectInfo.goPrefab, transform.position, Quaternion.identity);

            //객체 생성 시켰기 때문에 바로 비활성화.
            t_clone.SetActive(false);

            //부모 설정.
            //기존에 설정했던 노트같은 경우에는 노트매니져 스크립트가 붙어있는 객체가 부모. 부모 객체가 존재한다면 그 객체를 부모로 삼아주기.
            if (p_objectInfo.tfPoolParent != null)
                t_clone.transform.SetParent(p_objectInfo.tfPoolParent);

            //부모 설정이 null값이라면 t_clone.transform.SetParent(this.transform); 부모로.
            else
                t_clone.transform.SetParent(this.transform);

            //반복문이 다 돌고 나면 Queue 카운트 갯수만큼 객체가 들어가있다.
            t_queue.Enqueue(t_clone);


        }

        return t_queue;
    }
}
