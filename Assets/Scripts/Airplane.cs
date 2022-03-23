using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    public bool propellerOn = false; // true면 프로펠러 돌아가고 fasle면 안돌아감
    //private GameObject propObj = null;
    private Transform propTransform = null; // 프로펠러의 트랜스// 프로펠러의 트랜스폼을 돌리기
    private float propSpeed = 720.0f; //1초에 2바퀴 돌리기가 기본
    public float moveSpeed = 3.0f;
    public Transform[] waypoints = null;

    private int waypointIndex = 0;

    private void Awake()
    {
        //propObj = transform.Find("Propeller").gameObject;
        propTransform = transform.Find("Propeller"); //transform 자식 중에서 이름이 프로펠러 인 트랜스폼 찾ㄱ
    }
    private void Start()
    {
        propellerOn = true;
        if (waypoints.Length > 0)
        {
            waypointIndex = 0;
            transform.LookAt(waypoints[waypointIndex]);
        }
        else
            Debug.Log("waypoint가 없음");
    }
    private void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);        
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
        if (CheckArrive())
        {
            GoNextWaypoint();
        }
        if (propellerOn)
        {
            propTransform.Rotate(0, propSpeed * Time.deltaTime, 0);// 프로펠러의 트랜스폼을 돌리기
        }

    }

    private bool CheckArrive()
    {
        //waypoints[waypointIndex].position; 도착 지점
        //transform.position 출발 지점
        Vector3 distance = waypoints[waypointIndex].position - transform.position; //도착지점 - 출발 지
        return distance.sqrMagnitude < 0.1f;
    }

    void GoNextWaypoint()
    {
        waypointIndex++;
        waypointIndex %= waypoints.Length;
        transform.LookAt(waypoints[waypointIndex]);
    }


}
