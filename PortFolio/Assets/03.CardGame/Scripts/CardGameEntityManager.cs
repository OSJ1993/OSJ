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

    // IsFullmyEntities �� Entitiy�� �� ã���� �˷��ִ� ��� 22.05.04 ����
    //myEntitiesCount�� MAX_ENTITY_COUNT �̻��̰� ���� ExistMyEmptyEntity�� �������� ������ 
    public bool IsFullmyEntities => myEntities.Count >= MAX_ENTITY_COUNT && !ExistMyEmptyEntity;

    //other���� ���� mouse Drag ����� �����Ƿ� �ٷ� ���� �Ǵϱ� otherEntities.Count�� MAX_ENTITY_COUNT �̻��̸�  IsFullOtherEntities�� �Ǵ� ��� 22.05.04 ����
    bool IsFullOtherEntities => otherEntities.Count >= MAX_ENTITY_COUNT;

    bool ExistTargetPickEntity => targetPickEntity != null;

    //MyEmptyEntity�� �����ϴ� ���� myEntities.Exists�ؼ� x�� myEmptyEntity�� ���縦 �ϴ� �� �� �ϴ� ��  ExistMyEmptyEntity�� �Ǵ��ϴ� ��� 22.05.04 ����
    bool ExistMyEmptyEntity => myEntities.Exists(x => x == myEmptyEntity);

    int MyEmptyEntityIndex => myEntities.FindIndex(x => x == myEmptyEntity);

    //CanMouseInput����µ� myTurn�̸鼭 isLoading�� false�̸� mouse�� �Է��� ���� �� �ְ� ���ִ� ��� 22.05.06 ����
    bool CanMouseInput => CardGameTrunManager.Inst.myTurn && !CardGameTrunManager.Inst.isLoading;

    //������ ���� �����ϴ� ��� 22.05.06 ����
    CardGameEntity selectEntity;

    //mouse�� ����ٰ� ����� �����ϰ� �Ǵ� ��� 22.05.06 ����
    CardGameEntity targetPickEntity;

    WaitForSeconds delay1 = new WaitForSeconds(1);

    //boss ���� ó�� �Ǵ� ��� 22.05.09 ����
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

        //myTurn�� �Ѱ����� ��Turn�̶�� StartCoroutine
        if (!myTurn)
            StartCoroutine(AICo());
    }

    void Update()
    {
        ShowTargetPicker(ExistTargetPickEntity);
    }



    //�� card ���� ��� 22.05.04 ����
    IEnumerator AICo()
    {
        CardManager.Inst.TryPutCard(false);
        yield return delay1;

        //���� ���� ��� 22.05.04 ����

        //attackable�� true�� ��� otherEntites�� ������ ������ �����ִ� ��� 22.05.09 ����
        //new List�� �� ������ otherEntities ������ �ٲٸ� �ȵǱ� ���� 22.05.09 ����
        var attackers = new List<CardGameEntity>(otherEntities.FindAll(x => x.attackable == true));
        
        //list�� ���� ���� ��� 22.05.09����
        for (int i = 0; i < attackers.Count; i++)
        {
            int rand = Random.Range(i, attackers.Count);
            CardGameEntity temp = attackers[i];
            attackers[i] = attackers[rand];
            attackers[rand] = temp;
        }

        //������ ������ myEntities�� random�ϰ� �ð��� ���� �ϴ� ��� 22.05.09 ����
        foreach(var attacker in attackers)
        {
            //myEntities�� ���ο� ������ �Ҵ��ϴ� ������ myBossEntity���� �߰� �� �� �̱� ���� 22.05.09����
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

    //Entity��  ���� spawn�ϸ� �� �ʵ忡 Entity�� ��밡 spawn �ϸ� ��� �ʵ忡 ��ġ �ϰ� �ϴ� ��� 22.05.04 ����
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

    //mouse�� �ʵ忡 drag���� �� �� gameObject�� ������ ������ �ؼ� x��ǥ�� ���� List�� ������ �ٲ��ִ� ��� 22.05.04 ����
    public void InsertMyEmptyEntity(float xpos)
    {
        //IsFullmyEntities �� true��� return�� ���ش� �ֳ��ϸ� �� �̻� �ʵ尡 Full�̱� �����̴� 22.05.04 ����
        if (IsFullmyEntities)
            return;

        //���࿡ EmptyEntity�� ���� ���� ������ myEntities�� �߰��� �ϴ� ��� (�ּ��� 1���� �����ϰ� �ȴ�.) 22.05.04 ����
        if (!ExistMyEmptyEntity)
            myEntities.Add(myEmptyEntity);

        Vector3 emptyEntityPos = myEmptyEntity.transform.position;
        emptyEntityPos.x = xpos;
        myEmptyEntity.transform.position = emptyEntityPos;

        //������ �� �� Index�� �ٲ��� �Ǵ��ؼ� �ٲ������ Entity������ �ϴ� ��� 22.05.04 ����
        int _emptyEntityIndex = MyEmptyEntityIndex;
        myEntities.Sort((entity1, entiy2) => entity1.transform.position.x.CompareTo(entiy2.transform.position.x));
        if (MyEmptyEntityIndex != _emptyEntityIndex)
            EntityAlignment(true);
    }

    //�� gameObject ���� ��� 22.05.04 ����
    public void RemoveMyEmptyEntity()
    {
        if (!ExistMyEmptyEntity)
            return;

        myEntities.RemoveAt(MyEmptyEntityIndex);

        //������ �� �� �ֵ��� �ϴ� ��� 22.05.04 ����
        EntityAlignment(true);
    }

    //Entity spawn��� 22.05.04 ����
    //bool Spawn�� �����ߴ� �� �˾ƺ��� ���� ��� 22.05.04 ����
    public bool SpawnEntity(bool isMine, Item item, Vector3 spawnPos)
    {
        if (isMine)
        {
            //FullmyEntities(�� ���ִ� �� ) �� EnptyEntity�� ���ٸ� return false �ϴ� ��� 22.05.04 ����
            if (IsFullmyEntities || !ExistMyEmptyEntity)
                return false;
        }
        else
        {
            //�� �ʵ尡 �� á�ٸ� retrun false ��Ű�� ��� 22.05.04 ����
            if (IsFullOtherEntities)
                return false;
        }

        //Instantiate �ؼ� entityPrefab ���� ��ġ�� spawnPos ��ġ��  �Ű� ������ �Ѱ��� ��ġ�� Quaternion identity�� ������ �ϸ� entitiyObject(gameObject)�� ��ȯ �ϰ�
        var entitiyObject = Instantiate(entityPrefab, spawnPos, Utils.QI);
        //entity���� �����ؼ� entity�� �ϰ� 
        var entity = entitiyObject.GetComponent<CardGameEntity>();

        //���࿡ isMine�̶�� myEntities�� MyEmptyEntityIndex(�� gameObjectIndex)���ٰ� entity�� �ִ� ��� 22.05.04 ����
        if (isMine)
            myEntities[MyEmptyEntityIndex] = entity;

        //isMine�� �ƴ϶�� otherEntities�� Insert�� otherEntities.Count��ŭ  Random.Range�� ������ �ƹ� ���̳� Insert�� ���� �ִ� ��� 22.05.04 ����
        else
            otherEntities.Insert(Random.Range(0, otherEntities.Count), entity);

        entity.isMine = isMine;
        entity.Setup(item);

        //���� �߰� �ϸ� ���� ���� �ϰ� ������ �߰��ϸ� ������ ���� �ϴ� ��� 22.05.04 ����
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

        // selectiEntity, targetPickEntity �� �� �����ϸ� �����Ѵ�. �ٷ� null, null�� ����� ��� 22.05.09 ����
        if (selectEntity && targetPickEntity && selectEntity.attackable)
            Attack(selectEntity, targetPickEntity);

        selectEntity = null;
        targetPickEntity = null;
    }

    public void EntityMouseDrag()
    {
        if (!CanMouseInput || selectEntity == null)
            return;

        //other targetEntity ã�� ��� 22.05.06 ����
        bool existTarget = false;

        // Physics2D.RaycastAll ���� mousedrag���� �� Utils.MousePos���� camera������ �߻��ϰ� �Ǹ� �ű⿡ �浹 ��  ��� RaycastAll �������� ��� 22.05.07 ����
        foreach (var hit in Physics2D.RaycastAll(Utils.MousePos, Vector3.forward))
        {
            //var hit�� ���� �� ��  foreach���� �� �� hit�� collider�� ���縦 �Ѵٸ� GetComponent<CardGameEntity> �������� ��� 22.05.07 ����
            CardGameEntity entity = hit.collider?.GetComponent<CardGameEntity>();

            //entity�� null�� �ƴϰ� entity�� isMine�� �ƴϰ� selectEntity.attackable�� true��� ������ �� �ִ� ���� ��� 22.05.07 ����
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
        //_attacker�� _defender�� ��ġ�� �̵��ϴ� ���� ��ġ�� ���� ���(�� �� order�� ����(z�� ���� ����)) 22.05.09 ���� 
        attacker.attackable = false;
        attacker.GetComponent<Order>().SetMostFrontOrder(true);

        Sequence sequence = DOTween.Sequence()
                .Append(attacker.transform.DOMove(defender.originPos, 0.4f)).SetEase(Ease.InSine)
                .AppendCallback(() =>
                {
                    //damage �ְ� �ޱ� ��� 22.05.09 ����

                    //������� ���ݷ� ��ŭ damage�� �ִ� ��� 22.05.09 ����
                    attacker.Damaged(defender.attack);

                    //�������� ���ݷ� ��ŭ damage�� �ִ� ��� 22.05.09 ����
                    defender.Damaged(attacker.attack);

                    //damage �ְ� �޴� ��� 22.05.09 ����
                    SpawnDamge(defender.attack, attacker.transform);
                    SpawnDamge(attacker.attack, defender.transform);

                })
                .Append(attacker.transform.DOMove(attacker.originPos, 0.4f)).SetEase(Ease.OutSine)

                //���� ó�� ��� 22.05.09 ����
                .OnComplete(() => AttackCallback(attacker, defender));
    }

    //params ������ ���� ���� �Ű������� �ѱ� �� �ִ� ��� 22.05.09 ����
    void AttackCallback(params CardGameEntity[] entities)
    {
        //���� Entity ��� ���� ó�� ��� 22.05.09 ����
        entities[0].GetComponent<Order>().SetMostFrontOrder(false);


        foreach (var entity in entities)
        {
            if (!entity.isDie || entity.isBossOrEmpty)
                continue;

            if (entity.isMine)
                myEntities.Remove(entity);
            else
                otherEntities.Remove(entity);

            //�״� ��� ��� 22.05.09 ����
            Sequence sequence = DOTween.Sequence()
                .Append(entity.transform.DOShakePosition(1.3f))
                .Append(entity.transform.DOScale(Vector3.zero, 0.3f)).SetEase(Ease.OutCirc)
                .OnComplete(() =>
                {
                    EntityAlignment(entity.isMine);
                    Destroy(entity.gameObject);
                });


        }

        //������ �� ������ ���� �׾��� �� Ȯ�� �ϴ� ��� 22.05.09 ����
        StartCoroutine(CheckBossDie());
    }

    IEnumerator CheckBossDie()
    {
        //2�ʵڿ�
        yield return delay2;

        //myBossEntity�� ������ CardGameManager�ִ� GameOver�� false�� �ϸ� ���� �̰�ٴ� ��� 22.05.09 ����
        if (myBossEntity.isDie)
            StartCoroutine(CardGameManager.Inst.GameOver(false));

        //otherBossEntity�� ������ CardGameManager�ִ� GameOver�� true�� �ϸ� ���� �̰�ٶ�� ��� 22.05.09 ����
        if (otherBossEntity.isDie)
            StartCoroutine(CardGameManager.Inst.GameOver(true));
    }

    //debuging�� ���� ��� 22.05.09 ����
    public void DamageBoss(bool isMine, int damage)
    {
        var targetBossEntity = isMine ? myBossEntity : otherBossEntity;
        targetBossEntity.Damaged(damage);
        StartCoroutine(CheckBossDie());
    }

    void ShowTargetPicker(bool ishow)
    {
        TargetPicker.SetActive(ishow);

        //targetPickEntity.transform.position�� TargetPicker.transform.position�� �״�� �����ϴ� ��� 22.05.09 ����
        if (ExistTargetPickEntity)
            TargetPicker.transform.position = targetPickEntity.transform.position;
    }

    //damage sprite�� damage ���� Entity ��ġ�� �� �� �ְ� ���ִ� ��� 22.05.09 ����
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
