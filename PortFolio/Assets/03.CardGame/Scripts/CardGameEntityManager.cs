using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameEntityManager : MonoBehaviour
{
    public static CardGameEntityManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] GameObject entityPrefab;
    [SerializeField] List<CardGameEntity> myEntities;
    [SerializeField] List<CardGameEntity> otherEntities;

    [SerializeField] CardGameEntity myEmptyEntity;
    [SerializeField] CardGameEntity myBossEntity;
    [SerializeField] CardGameEntity otherBossEntity;

    const int MAX_ENTITY_COUNT = 4;

    // IsFullmyEntities 내 Entitiy가 꽉 찾음을 알려주는 기능 22.05.04 승주
    //myEntitiesCount가 MAX_ENTITY_COUNT 이상이고 또한 ExistMyEmptyEntity가 존재하지 않으면 
    public bool IsFullmyEntities => myEntities.Count >= MAX_ENTITY_COUNT && !ExistMyEmptyEntity;

    //other같은 경우는 mouse Drag 기능이 없으므로 바로 내면 되니까 otherEntities.Count가 MAX_ENTITY_COUNT 이상이면  IsFullOtherEntities이 되는 기능 22.05.04 승주
    bool IsFullOtherEntities => otherEntities.Count >= MAX_ENTITY_COUNT;

    //MyEmptyEntity가 존재하는 것은 myEntities.Exists해서 x가 myEmptyEntity가 존재를 하는 지 안 하는 지  ExistMyEmptyEntity로 판단하는 기능 22.05.04 승주
    bool ExistMyEmptyEntity => myEntities.Exists(x => x == myEmptyEntity);

    int MyEmptyEntityIndex => myEntities.FindIndex(x => x == myEmptyEntity);

    //myTurn이면서 isLoading이 false 라면 CanMouseInput를 받을 수 있다 기능 22.05.06 승주
    bool CanMouseInput => CardTrunManager.Inst.myTurn && !CardTrunManager.Inst.isLoading;

    //공격 할 것을 정하는 기능 22.05.06 승주
    Enemy selectEntity;

    //mouse를 끌어다가 대상을 선택 하게 되면 그게 targetPickEntity 기능 22.05.06 승주
    Enemy targetPickEntity;

    WaitForSeconds delay1 = new WaitForSeconds(1);

    
     void Start()
    {
        CardTrunManager.OnTurnStarted += OnTurnStarted;
    }

    private void OnDestroy()
    {
        CardTrunManager.OnTurnStarted -= OnTurnStarted;
    }
    
    void OnTurnStarted(bool myTurn)
    {
        //myTurn이 넘겨지면 적Turn이라면 StartCoroutine
        if (!myTurn)
            StartCoroutine(AICo());
    }

    //적 card 내기 기능 22.05.04 승주
    IEnumerator AICo()
    {
        CardManager.Inst.TryPutCard(false);
        yield return delay1;

        //공격 기능 22.05.04 승주
        CardTrunManager.Inst.EndTurn();
        
    }

    //Entity를  내가 spawn하면 내 필드에 Entity를 상대가 spawn 하면 상대 필드에 위치 하게 하는 기능 22.05.04 승주
    void EntityAlignment(bool isMine)
    {
        float targetY = isMine ? -2.5f : 4.5f;
        var targetEntities = isMine ? myEntities : otherEntities;

        for (int i = 0; i < targetEntities.Count; i++)
        {
            float targetX = (targetEntities.Count - 1) * 0.05f + i * 4f;

            var targetEntity = targetEntities[i];
            targetEntity.originPos = new Vector3(targetX-6, targetY, 0);
            targetEntity.MoveTransform(targetEntity.originPos, true, 0.5f);
            targetEntity.GetComponent<Order>()?.SetOriginOrder(i);
        }
    }

    //mouse를 필드에 drag했을 때 빈 gameObject가 없으면 생성을 해서 x좌표에 따라 List의 순서를 바꿔주는 기능 22.05.04 승주
    public void InsertMyEmptyEntity(float xpos)
    {
        //IsFullmyEntities 가 true라면 return을 해준다 왜냐하면 더 이상 필드가 Full이기 때문이다 22.05.04 승주
        if (IsFullmyEntities)
            return;

        //만약에 EmptyEntity가 존재 하지 않으면 myEntities에 추가를 하는 기능 (최소한 1개가 존재하게 된다.) 22.05.04 승주
        if (!ExistMyEmptyEntity)
            myEntities.Add(myEmptyEntity);

        Vector3 emptyEntityPos = myEmptyEntity.transform.position;
        emptyEntityPos.x = xpos;
        myEmptyEntity.transform.position = emptyEntityPos;

        //정렬을 할 때 Index가 바뀜을 판단해서 바뀌었으면 Entity정렬을 하는 기능 22.05.04 승주
        int _emptyEntityIndex = MyEmptyEntityIndex;
        myEntities.Sort((entity1, entiy2) => entity1.transform.position.x.CompareTo(entiy2.transform.position.x));
        if (MyEmptyEntityIndex != _emptyEntityIndex)
            EntityAlignment(true);
    }

    //빈 gameObject 빼기 기능 22.05.04 승주
    public void RemoveMyEmptyEntity()
    {
        if (!ExistMyEmptyEntity)
            return;

        myEntities.RemoveAt(MyEmptyEntityIndex);

        //정렬이 될 수 있도록 하는 기능 22.05.04 승주
        EntityAlignment(true);
    }

    //Entity spawn기능 22.05.04 승주
    //bool Spawn이 성공했는 지 알아보기 위한 기능 22.05.04 승주
    public bool SpawnEntity(bool isMine, Item item, Vector3 spawnPos)
    {
        if (isMine)
        {
            //FullmyEntities(꽉 차있는 지 ) 과 EnptyEntity가 없다면 return false 하는 기능 22.05.04 승주
            if (IsFullmyEntities || !ExistMyEmptyEntity)
                return false;
        }
        else
        {
            //적 필드가 꽉 찼다면 retrun false 시키는 기능 22.05.04 승주
            if (IsFullOtherEntities)
                return false;
        }

        //Instantiate 해서 entityPrefab 생성 위치는 spawnPos 위치에  매개 변수로 넘겨준 위치에 Quaternion identity로 생성을 하면 entitiyObject(gameObject)를 반환 하고
        var entitiyObject = Instantiate(entityPrefab, spawnPos, Utils.QI);
        //entity까지 참고해서 entity를 하고 
        var entity = entitiyObject.GetComponent<CardGameEntity>();

        //만약에 isMine이라면 myEntities에 MyEmptyEntityIndex(빈 gameObjectIndex)에다가 entity를 넣는 기능 22.05.04 승주
        if (isMine)
            myEntities[MyEmptyEntityIndex] = entity;

        //isMine이 아니라면 otherEntities에 Insert를 otherEntities.Count만큼  Random.Range를 돌려서 아무 곳이나 Insert로 끼워 넣는 기능 22.05.04 승주
        else
            otherEntities.Insert(Random.Range(0, otherEntities.Count), entity);

        entity.isMine = isMine;
        entity.Setup(item);

        //내가 추가 하면 나를 정렬 하고 상대방이 추가하면 상대방이 정렬 하는 기능 22.05.04 승주
        EntityAlignment(isMine);

        return true;

    }

    public void EntityMouseDown(CardGameEntity entity)
    {
        if (!CanMouseInput)
            return;

        selectEntity = entity;
    }

    public void EntityMouseUp()
    {
        if (!CanMouseInput)
            return;

        selectEntity = null;
        targetPickEntity = null;
    }

    public void EntityMouseDrag()
    {
        if (!CanMouseInput || selectEntity == null)
            return;

        //other targetEntity 찾기 기능 22.05.06 승주
        bool existTarget = false;
        foreach(var hit in Physics2D.RaycastAll(Utils.MousePos, Vector3.forward))
        {

            CardGameEntity entity = hit.collider?.GetComponent<CardGameEntity>();
            if( entity != null && !entity.isMine&& selectEntity.attackable)
            {

                targetPickEntity = entity;
                existTarget = true;
                break;
            }
            if (!existTarget)
                targetPickEntity = null;
        }
    }
}
