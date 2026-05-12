using System;
using UnityEngine;

/// <summary>
/// 전반적인 Input을 관리하는 클래스
/// </summary>
public class InputController : MonoBehaviour
{

    /// <summary>
    /// 플레이어의 좌우 입력 이벤트
    /// </summary>
    public event Action<float> OnMoveEvent;

    /// <summary>
    /// 플레이어 점프 이벤트
    /// </summary>
    public event Action OnJumpEvent;

    private void Update()
    {
        MovePlayer();
        JumpPlayer();
    }

    /// <summary>
    /// 플레이어 점프 입력을 감지하는 함수
    /// </summary>
    private void JumpPlayer()
    {
        //만약 스페이스바를 눌렀다면?
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //점프 이벤트를 발행
            OnJumpEvent?.Invoke();
        }
    }

    /// <summary>
    /// 플레이어 이동 입력을 감지하는 함수
    /// </summary>
    private void MovePlayer()
    {
        //Horizontal입력 받기(A,D,<-,->)
        float x = Input.GetAxisRaw("Horizontal");

        //이동 이벤트 발행
        OnMoveEvent?.Invoke(x);
    }
}