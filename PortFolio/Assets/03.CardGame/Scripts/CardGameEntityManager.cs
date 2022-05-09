using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardGameEntityManager : MonoBehaviour
{
    public static CardGameEntityManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] GameObject entityPrefab;
    [SerializeField] GameObject damagePrefab;
    [SerializeField] List<CardGameEntity> myEntities;
    [SerializeField] List<CardGameEntity> otherEntities;

    [SerializeField] GameObject TargetPicker;

    [SerializeField] CardGameEntity myEmptyEntity;
    [SerializeField] CardGameEntity myBossEntity;
    [SerializeField] CardGameEntity otherBossEntity;

    const int MAX_ENTITY_COUNT = 4;

    // IsFullmyEntities 내 Entitiy가 꽉 찾음을 알려주는 기능 22.05.04 승주
    //myEntitiesCount가 MAX_ENTITY_COUNT 이상이고 또한 ExistMyEmptyEntity가 존재하지 않으면 
    public bool IsFullmyEntities => myEntities.Count >= MAX_ENTITY_COUNT && !ExistMyEmptyEntity;

    //other같은 경우는 mouse Drag 기능이 없으므로 바로 내면 되니까 otherEntities.Count가 MAX_ENTITY_COUNT 이상이면  IsFullOtherEntities이 되는 기능 22.05.04 승주
    bool IsFullOtherEntities => otherEntities.Count >= MAX_ENTITY_COUNT;

    bool ExistTargetPickEntity => targetPickEntity != null;

    //MyEmptyEntity가 존재하는 것은 myEntities.Exists해서 x가 myEmptyEntity가 존재를 하는 지 안 하는 지  ExistMyEmptyEntity로 판단하는 기능 22.05.04 승주
    bool ExistMyEmptyEntity => myEntities.Exists(x => x == myEmptyEntity);

    int MyEmptyEntityIndex => myEntities.FindIndex(x => x == myEmptyEntity);

    //CanMouseInput만드는데 myTurn이면서 isLoading이 false이면 mouse에 입력을 받을 수 있게 해주는 기능 22.05.06 승주
    bool CanMouseInput => CardGameTrunManager.Inst.myTurn && !CardGameTrunManager.Inst.isLoading;

    //공격할 것을 선택하는 기능 22.05.06 승주
    CardGameEntity selectEntity;

    //mouse를 끌어다가 대상을 선택하게 되는 기능 22.05.06 승주
    CardGameEntity targetPickEntity;

    WaitForSeconds delay1 = new WaitForSeconds(1);

    //boss 죽음 처리 판단 기능 22.05.09 승주
    WaitForSeconds delay2 = new WaitForSeconds(2);


    void Start()
    {
        CardGameTrunManager.OnTurnStarted += OnTurnStarted;
    }

    private void OnDestroy()
    {
        CardGameTrunManager.OnTurnStarted -= OnTurnStarted;
    }

    void OnTurnStarted(bool myTurn)
    {
        AttackableReset(myTurn);

        //myTurn이 넘겨지면 적Turn이라면 StartCoroutine
        if (!myTurn)
            StartCoroutine(AICo());
    }

    void Update()
    {
        ShowTargetPicker(ExistTargetPickEntity);
    }



    //적 card 내기 기능 22.05.04 승주
    IEnumerator AICo()
    {
        CardManager.Inst.TryPutCard(false);
        yield return delay1;

        //공격 로직 기능 22.05.04 승주

        //attackable이 true인 모든 otherEntites를 가져와 순서를 섞어주는 기능 22.05.09 승주
        //new List를 한 이유는 otherEntities 순서를 바꾸면 안되기 때문 22.05.09 승주
        var attackers = new List<CardGameEntity>(otherEntities.FindAll(x => x.attackable == true));
        
        //list의 순서 섞는 기능 22.05.09승주
        for (int i = 0; i < attackers.Count; i++)
        {
            int rand = Random.Range(i, attackers.Count);
            CardGameEntity temp = attackers[i];
            attackers[i] = attackers[rand];
            attackers[rand] = temp;
        }

        //보스를 포함한 myEntities를 random하게 시간차 공격 하는 기능 22.05.09 승주
        foreach(var attacker in attackers)
        {
            //myEntities를 새로운 공간을 할당하는 이유는 myBossEntity까지 추가 할 것 이기 때문 22.05.09승주
            var defenders = new List<CardGameEntity>(myEntities);
            defenders.Add(myBossEntity);
            int rand = Random.Range(0, defenders.Count);
            Attack(attacker, defenders[rand]);

            if (CardGameTrunManager.Inst.isLoading)
                yield break;

            yield return new WaitForSeconds(2);
        }

        CardGameTrunManager.Inst.EndTurn();

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
            targetEntity.originPos = new Vector3(targetX - 6, targetY, 0);
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

        // selectiEntity, targetPickEntity 둘 다 존재하면 공격한다. 바로 null, null로 만드는 기능 22.05.09 승주
        if (selectEntity && targetPickEntity && selectEntity.attackable)
            Attack(selectEntity, targetPickEntity);

        selectEntity = null;
        targetPickEntity = null;
    }

    public void EntityMouseDrag()
    {
        if (!CanMouseInput || selectEntity == null)
            return;

        //other targetEntity 찾기 기능 22.05.06 승주
        bool existTarget = false;

        // Physics2D.RaycastAll 쏴서 mousedrag중일 때 Utils.MousePos부터 camera앞쪽을 발사하게 되며 거기에 충돌 된  모든 RaycastAll 가져오는 기능 22.05.07 승주
        foreach (var hit in Physics2D.RaycastAll(Utils.MousePos, Vector3.forward))
        {
            //var hit로 들어가고 그 떄  foreach문을 돌 때 hit에 collider가 존재를 한다면 GetComponent<CardGameEntity> 가져오는 기능 22.05.07 승주
            CardGameEntity entity = hit.collider?.GetComponent<CardGameEntity>();

            //entity가 null이 아니고 entity가 isMine도 아니고 selectEntity.attackable가 true라면 공격할 수 있는 상태 기능 22.05.07 승주
            if (entity != null && !entity.isMine && selectEntity.attackable)
            {

                targetPickEntity = entity;
                existTarget = true;
                break;
            }
        }

        if (!existTarget)
            targetPickEntity = null;
    }

    void Attack(CardGameEntity attacker, CardGameEntity defender)
    {
        //_attacker가 _defender의 위치로 이동하다 원래 위치로 오는 기능(이 떄 order가 높다(z축 제일 앞쪽)) 22.05.09 승주 
        attacker.attackable = false;
        attacker.GetComponent<Order>().SetMostFrontOrder(true);

        Sequence sequence = DOTween.Sequence()
                .Append(attacker.transform.DOMove(defender.originPos, 0.4f)).SetEase(Ease.InSine)
                .AppendCallback(() =>
                {
                    //damage 주고 받기 기능 22.05.09 승주

                    //방어자의 공격력 만큼 damage를 주는 기능 22.05.09 승주
                    attacker.Damaged(defender.attack);

                    //공격자의 공격력 만큼 damage를 주는 기능 22.05.09 승주
                    defender.Damaged(attacker.attack);

                    //damage 주고 받는 기능 22.05.09 승주
                    SpawnDamge(defender.attack, attacker.transform);
                    SpawnDamge(attacker.attack, defender.transform);

                })
                .Append(attacker.transform.DOMove(attacker.originPos, 0.4f)).SetEase(Ease.OutSine)

                //죽음 처리 기능 22.05.09 승주
                .OnComplete(() => AttackCallback(attacker, defender));
    }

    //params 개수의 제한 없이 매개변수를 넘길 수 있는 기능 22.05.09 승주
    void AttackCallback(params CardGameEntity[] entities)
    {
        //죽을 Entity 골라서 죽음 처리 기능 22.05.09 승주
        entities[0].GetComponent<Order>().SetMostFrontOrder(false);


        foreach (var entity in entities)
        {
            if (!entity.isDie || entity.isBossOrEmpty)
                continue;

            if (entity.isMine)
                myEntities.Remove(entity);
            else
                otherEntities.Remove(entity);

            //죽는 모션 기능 22.05.09 승주
            Sequence sequence = DOTween.Sequence()
                .Append(entity.transform.DOShakePosition(1.3f))
                .Append(entity.transform.DOScale(Vector3.zero, 0.3f)).SetEase(Ease.OutCirc)
                .OnComplete(() =>
                {
                    EntityAlignment(entity.isMine);
                    Destroy(entity.gameObject);
                });


        }

        //공격을 할 때마다 누가 죽었는 지 확인 하는 기능 22.05.09 승주
        StartCoroutine(CheckBossDie());
    }

    IEnumerator CheckBossDie()
    {
        //2초뒤에
        yield return delay2;

        //myBossEntity가 죽으면 CardGameManager있는 GameOver를 false를 하면 적이 이겼다는 기능 22.05.09 승주
        if (myBossEntity.isDie)
            StartCoroutine(CardGameManager.Inst.GameOver(false));

        //otherBossEntity가 죽으면 CardGameManager있는 GameOver를 true를 하면 내가 이겼다라는 기능 22.05.09 승주
        if (otherBossEntity.isDie)
            StartCoroutine(CardGameManager.Inst.GameOver(true));
    }

    //debuging을 위한 기능 22.05.09 승주
    public void DamageBoss(bool isMine, int damage)
    {
        var targetBossEntity = isMine ? myBossEntity : otherBossEntity;
        targetBossEntity.Damaged(damage);
        StartCoroutine(CheckBossDie());
    }

    void ShowTargetPicker(bool ishow)
    {
        TargetPicker.SetActive(ishow);

        //targetPickEntity.transform.position을 TargetPicker.transform.position에 그대로 대입하는 기능 22.05.09 승주
        if (ExistTargetPickEntity)
            TargetPicker.transform.position = targetPickEntity.transform.position;
    }

    //damage sprite를 damage 받은 Entity 위치에 뜰 수 있게 해주는 기능 22.05.09 승주
    void SpawnDamge(int damage, Transform tr)
    {
        if (damage <= 0)
            return;

        var damageComponent = Instantiate(damagePrefab).GetComponent<CardGameDamage>();
        damageComponent.SetupTransform(tr);
        damageComponent.Damaged(damage);
    }

    public void AttackableReset(bool isMine)
    {
        var targetEntites = isMine ? myEntities : otherEntities;
        targetEntites.ForEach(x => x.attackable = true);
    }
}
