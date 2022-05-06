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

    // IsFullmyEntities �� Entitiy�� �� ã���� �˷��ִ� ��� 22.05.04 ����
    //myEntitiesCount�� MAX_ENTITY_COUNT �̻��̰� ���� ExistMyEmptyEntity�� �������� ������ 
    public bool IsFullmyEntities => myEntities.Count >= MAX_ENTITY_COUNT && !ExistMyEmptyEntity;

    //other���� ���� mouse Drag ����� �����Ƿ� �ٷ� ���� �Ǵϱ� otherEntities.Count�� MAX_ENTITY_COUNT �̻��̸�  IsFullOtherEntities�� �Ǵ� ��� 22.05.04 ����
    bool IsFullOtherEntities => otherEntities.Count >= MAX_ENTITY_COUNT;

    //MyEmptyEntity�� �����ϴ� ���� myEntities.Exists�ؼ� x�� myEmptyEntity�� ���縦 �ϴ� �� �� �ϴ� ��  ExistMyEmptyEntity�� �Ǵ��ϴ� ��� 22.05.04 ����
    bool ExistMyEmptyEntity => myEntities.Exists(x => x == myEmptyEntity);

    int MyEmptyEntityIndex => myEntities.FindIndex(x => x == myEmptyEntity);

    //myTurn�̸鼭 isLoading�� false ��� CanMouseInput�� ���� �� �ִ� ��� 22.05.06 ����
    bool CanMouseInput => CardTrunManager.Inst.myTurn && !CardTrunManager.Inst.isLoading;

    //���� �� ���� ���ϴ� ��� 22.05.06 ����
    Enemy selectEntity;

    //mouse�� ����ٰ� ����� ���� �ϰ� �Ǹ� �װ� targetPickEntity ��� 22.05.06 ����
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
        //myTurn�� �Ѱ����� ��Turn�̶�� StartCoroutine
        if (!myTurn)
            StartCoroutine(AICo());
    }

    //�� card ���� ��� 22.05.04 ����
    IEnumerator AICo()
    {
        CardManager.Inst.TryPutCard(false);
        yield return delay1;

        //���� ��� 22.05.04 ����
        CardTrunManager.Inst.EndTurn();
        
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
            targetEntity.originPos = new Vector3(targetX-6, targetY, 0);
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

        selectEntity = null;
        targetPickEntity = null;
    }

    public void EntityMouseDrag()
    {
        if (!CanMouseInput || selectEntity == null)
            return;

        //other targetEntity ã�� ��� 22.05.06 ����
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
