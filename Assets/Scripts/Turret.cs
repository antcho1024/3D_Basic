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

    IEnumerator shotSave;                   // 코루틴용 IEnumerator 저장

    // 첫번째 업데이트가 실행되기 직전
    private void Start()
    {
        shotSave = Shot();          // IEnumerator 저장
        StartCoroutine(shotSave);   // 저장한 IEnumerator로 코루틴 실행
    }

    IEnumerator Shot()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval - shots * rateOfFire); // 1초-0.1초*5 대기
            // 총알 연사 시작
            for (int i = 0; i < shots; i++)
            {
                Instantiate(bullet, shotTransform);             // 총알 생성
                yield return new WaitForSeconds(rateOfFire);    // 0.1초 대기
            }
        }
    }
}
