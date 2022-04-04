using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //ī�޶� � ����� ���� �� Ÿ�� ����. /22.03.23 by����
    [SerializeField] Transform thePlayer = null;

    //ī�޶� �̵� �ӵ� /22.03.23 by����
    [SerializeField] float followSpeed = 15;

    //ī�޶�� �÷��̾��� �Ÿ� ��  /22.03.23 by����
    Vector3 playerDistance = new Vector3();

    //Note�� ���� �� ���� zoom ȿ�� /22.03.23 by����
    float hitDistance = 0;
    //�þ߸� -1.25f��ŭ �־����� �ϴ� ȿ�� /22.03.23 by����
    [SerializeField] float zoomDistance = -1.25f;

    void Start()
    {
        //�Ÿ���: ī�޶� ��ġ - �÷��̾� ��ġ /22.03.23 by����
        playerDistance = transform.position - thePlayer.position;
    }


    void Update()
    {
        //�� ������ ���� �÷��̾� ���� �̵��� ��ǥ���� ���� ��, ī�޶� �̵� /22.03.23 by����
        //transform.forward ����� ���� ����(�Ķ��� z�� ȭ��ǥ)
        Vector3 t_destPos = thePlayer.position + playerDistance + (transform.forward * hitDistance);

        //Vecter3.Lerp(A,B,C):A�� B������ ������ C������ ���� ���� 
        //ex:Mathf.Lerp(0,10,0.5)=>5
        transform.position = Vector3.Lerp(transform.position, t_destPos, followSpeed * Time.deltaTime);

    }

    public IEnumerator ZoomCam()
    {
        hitDistance = zoomDistance;

        yield return new WaitForSeconds(0.15f);

        hitDistance = 0;
    }
}
