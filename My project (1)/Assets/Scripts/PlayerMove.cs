using UnityEngine;

/// <summary>
/// 플레이어 이동을 관리하는 클래스
/// </summary>
public class PlayerMove : MonoBehaviour
{

    /// <summary>
    /// 이동 스피드
    /// </summary>
    [SerializeField] private float _moveSpd;

    /// <summary>
    /// 물리 사용을 위하여 Rigidbody를 받기
    /// </summary>
    private Rigidbody2D _rigid;

    /// <summary>
    /// Input사용을 위하여 InputController를 받기
    /// </summary>
    private InputController _input;

    private void Awake()
    {
        //GetComponent로 찾아오기
        _input = GetComponent<InputController>();
        _rigid = GetComponent<Rigidbody2D>();

        //Input에 이동 이벤트 구독
        _input.OnMoveEvent += HandleMove;
    }

    /// <summary>
    /// 이동 이벤트를 처리하는 함수
    /// </summary>
    /// <param name="x">입력정보</param>
    private void HandleMove(float x)
    {
        //Rigidbody.velocity에 넣을 값 설정
        //x는 입력정보를 받아 Speed를 곱하여 설정
        //y는 점프 처리를 위하여 현재의 velocity로 설정
        var vel = new Vector2(x * _moveSpd, _rigid.velocity.y);

        //velocity설정
        _rigid.velocity = vel;
    }

    private void OnDestroy()
    {
        //Input이 아직 존재한다면?
        if (_input != null)
        {
            //이벤트 구독을 해제
            _input.OnMoveEvent -= HandleMove;
        }
    }
}
