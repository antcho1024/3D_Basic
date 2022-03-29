using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bullet = null;        // 발사할 총알 오브젝트
    public Transform shotTransform = null;  // 총알이 발사될 위치

    public float interval = 1.0f;           // 총알을 발사하는 간격(방아쇠를 당기는 간격)
    public int shots = 5;                   // 한번 발사할때 몇연사를 할 것인가
    public float rateOfFire = 0.1f;         // 연사를 할 때 발사되는 간격

    public float halfAngle = 10.0f;
    public float rotateSpeed = 2.0f;

    private float rotateDirection = 1.0f;
    private float targetAngle = 0.0f;



    private Transform pillar = null;

    IEnumerator shotSave;                   // 코루틴용 IEnumerator 저장

    private void Awake()
    {
        pillar = transform.Find("Pillar");
    }
    // 첫번째 업데이트가 실행되기 직전
    private void Start()
    {
        shotSave = Shot();          // IEnumerator 저장
        StartCoroutine(shotSave);   // 저장한 IEnumerator로 코루틴 실행
    }
    private void Update()
    {
        if (pillar)
        {
            // targetAngle은 매프레임 증가(증가 방향은 rotateDirection에 의해 결정된다)
            targetAngle += rotateDirection * rotateSpeed * Time.deltaTime;
            // targetAngle의 절대값이 halfAngle을 넘어섰는지 확인
            if (Mathf.Abs(targetAngle) > halfAngle)
            {
                rotateDirection *= -1.0f;   // 방향 뒤집
            }
            //회전 y축으로 targetAngle만큼 회전하는 쿼터니언 생성
            //생성한 쿼터니언을 기둥의 회전으로 적용
            pillar.transform.rotation = Quaternion.Euler(0, targetAngle, 0);

            //오일러 앵글로 y값 확인한 다음 범위를 벗어나면 방향 거꾸로 진
            //if (pillar.eulerAngles.y > halfAngle && pillar.eulerAngles.y < (360 - halfAngle)) // 각도가 항상 이 사이에 있도록
            //{
            //    rotateDirection *= -1;
            //}
            //pillar.Rotate(0, rotateSpeed * Time.deltaTime, 0);// == (pillar.transform.up * rotateSpeed * Time.deltaTime)

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
                //Instantiate(bullet, shotTransform);             // 총알 생성
                //부모 자식 연결 되어 있어서 이렇게 하면 총알이 총 따라 움직임
                Instantiate(bullet, shotTransform.position, shotTransform.rotation);    // 부모의 위치랑 회전 값만 받아오 
                yield return new WaitForSeconds(rateOfFire);    // 0.1초 대기
            }
        }
    }



}
