using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //카메라가 어떤 대상을 따라갈 지 타겟 설정. /22.03.23 by승주
    [SerializeField] Transform thePlayer = null;

    //카메라 이동 속도 /22.03.23 by승주
    [SerializeField] float followSpeed = 15;

    //카메라와 플레이어의 거리 차  /22.03.23 by승주
    Vector3 playerDistance = new Vector3();

    //Note를 맞출 때 마다 zoom 효과 /22.03.23 by승주
    float hitDistance = 0;
    //시야를 -1.25f만큼 멀어지게 하는 효과 /22.03.23 by승주
    [SerializeField] float zoomDistance = -1.25f;

    void Start()
    {
        //거리차: 카메라 위치 - 플레이어 위치 /22.03.23 by승주
        playerDistance = transform.position - thePlayer.position;
    }


    void Update()
    {
        //매 프레임 마다 플레이어 추적 이동할 좌표값을 구한 뒤, 카메라 이동 /22.03.23 by승주
        //transform.forward 대상의 정면 방향(파란색 z축 화살표)
        Vector3 t_destPos = thePlayer.position + playerDistance + (transform.forward * hitDistance);

        //Vecter3.Lerp(A,B,C):A와 B사이의 값에서 C비율의 값을 추출 
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
