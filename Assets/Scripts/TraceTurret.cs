using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceTurret : MonoBehaviour
{
    public GameObject bullet = null;        // 발사할 총알 오브젝트
    public Transform shotTransform = null;  // 총알이 발사될 위치

    public float interval = 1.0f;           // 총알을 발사하는 간격(방아쇠를 당기는 간격)
    public int shots = 5;                   // 한번 발사할때 몇연사를 할 것인가
    public float rateOfFire = 0.1f;         // 연사를 할 때 발사되는 간격

    IEnumerator shotSave;                   // 코루틴용 IEnumerator 저장

    Transform target = null;

    // 첫번째 업데이트가 실행되기 직전
    private void Start()
    {
        shotSave = Shot();          // IEnumerator 저장
        StartCoroutine(shotSave);   // 저장한 IEnumerator로 코루틴 실행
    }

    private void Update()
    {
        if(target!=null) // 무언가 트리거 안에 들어
        {
            // 1. LookAt
            //transform.LookAt(target);

            // 2. LookRotation
            //Vector3 dir = target.position - transform.position;
            //transform.rotation = Quaternion.LookRotation(dir);

            // 3. Quaternion.Lerp, Quaternion.Slerp
            // 보간을 응용해서 부드럽게 움직이는 연/
            Vector3 dir = target.position - transform.position; // 방향벡터 계산
            transform.rotation =
                Quaternion.Slerp(
                    transform.rotation,             // 시작할 때의 회전 상	
                    Quaternion.LookRotation(dir),   // 끝났을 때의 회전 상태
                    0.1f * Time.deltaTime);         // 시작과 끝 사이 지점 (0~1) 1초에 3/
        }
    }

    IEnumerator Shot()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval - shots * rateOfFire); // 1초-0.1초*5 대기
            // 총알 연사 시작
            for (int i = 0; i < shots; i++)
            {
                //GameObject bulletInstance = Instantiate(bullet, shotTransform);  // 총알 생성
                //bulletInstance.transform.parent = null;
                Instantiate(bullet, shotTransform.position, shotTransform.rotation);

                yield return new WaitForSeconds(rateOfFire);    // 0.1초 대기
            }
        }
    }

    // 일정 범위 안에 들어왔는지를 확인하는 방법
    //  1. 플레이어와의 거리를 계산한다.
    //     단점 : 매 프레임 계산해야 한다.
    //  2. 트리거를 사용한다.

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = null;
        }
    }
}
