using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//실슴 
//비행기 게임 오브젝트 만들기(프리펩)
//이름 : Airplane
//방식 : 프리미티브 사용 or 프로빌더
//true면 프로펠러 돌아가고 fasle면 안돌아감 
public class Airplane : MonoBehaviour
{
    public bool propellerOn = false; // true면 프로펠러 돌아가고 fasle면 안돌아감
    //private GameObject propObj = null;
    private Transform propTransform = null;
    private float propSpeed = 720.0f;

    private void Awake()
    {
        //propObj = transform.Find("Propeller").gameObject;
        propTransform = transform.Find("Propeller"); //transform 자식 중에서 이름이 프로펠러 인 트랜스폼 찾ㄱ
    }
    private void Update()
    {
        if(propellerOn)
        {
            propTransform.Rotate(0, propSpeed * Time.deltaTime, 0);// 초당 두바퀴
        }
    }


}
