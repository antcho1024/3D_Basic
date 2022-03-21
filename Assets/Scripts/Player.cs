using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]   // 이 스크립트를 가진 게임오브젝트는 무조건 Animator가 있다(없으면 만든다)
public class Player : MonoBehaviour
{
    public float moveSpeed = 10.0f;     // 플레이어 이동 속도 (기본값 1초에 10)
    public float spinSpeed = 360.0f;    // 플레이어 회전 속도 (기본값 1초에 한바퀴)
    private float spinInput = 0.0f;     // 회전 입력 여부 (-1.0 ~ 2.0)
    private float moveInput = 0.0f;     // 앞뒤 입력 여부 (-1.0 ~ 2.0)

    private Animator anim = null;       // 에니메이터 컴포넌트
    private PlayerAction pc = null;   // 입력 처리용 클래스
    private Rigidbody rigid = null;
    
    private void Awake()                
    {
        anim = GetComponent<Animator>();// 에니메이터 컴포넌트 찾아서 보관 (못찾으면 null 값 들어감)
        rigid = GetComponent<Rigidbody>();
        pc = new PlayerAction();      // Input Action Asset을 이용해 자동 생성한 클래스
        //Player라는 액션 맵에 있는 UseItem 액션이 start일 떄 UseItem 함수 실행하도록 바인딩
        pc.Player.UseItem.started += UseItem;
    }
    private void OnEnable()
    {
        pc.Player.Enable(); //액션 맵도 함께 활성화
    }
    private void OnDisable()
    {
        pc.Player.Disable();    // 애
    }
    /*private void Update()
    {
        // 이동 처리 (1초에 moveSpeed 만큼 moveInput쪽 방향(앞or뒤)으로 이동)
        transform.Translate(Vector3.forward * moveInput * moveSpeed * Time.deltaTime); 
        // 회전 처리 (1초에 spinSpeed 만큼 spinInput쪽 방향(죄우)으로 이동)
        transform.Rotate(Vector3.up * spinInput * spinSpeed * Time.deltaTime); 
    }
    */
    private void FixedUpdate() 
    {
        rigid.MovePosition(rigid.position + transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime); //  현재위치 + 움직이고픈 정도 
        rigid.MoveRotation(rigid.rotation * Quaternion.AngleAxis(spinInput*spinSpeed*Time.fixedDeltaTime, Vector3.up));
    }

    //WSAD를 눌렀을 때 실행될 함수
    public void MovePlayer(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();   // 입력값을 받아서 회전 정도랑 이동 정도를 받아옴
        spinInput = input.x;    //A(1) D(-1)
        moveInput = input.y;    //W(1) S(-1)

        if(context.started)
        {
            anim.SetBool("isMove",true);    // 누른 직후에 이동 에니메이션 시작
        }
        if(context.canceled)
        {
            anim.SetBool("isMove",false);   // 키를 땠을 떄 Idle 애니메이션 시작
        }
    }
    public void UseItem(InputAction.CallbackContext context)
    {
        anim.SetTrigger("OnUseItem");
    }
    
}
