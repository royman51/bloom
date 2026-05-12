using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    /// <summary>
    /// 점프파워
    /// </summary>
    [SerializeField] private float _jumpPower;

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

        //Input에 점프 이벤트 구독
        _input.OnJumpEvent += HandleJump;
    }

    /// <summary>
    /// 점프 이벤트 처리를 위한 함수
    /// </summary>
    private void HandleJump()
    {
        _rigid.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
    }

    private void OnDestroy()
    {
        //Input이 아직 존재한다면?
        if (_input != null)
        {
            //이벤트 구독을 해제
            _input.OnJumpEvent -= HandleJump;
        }
    }
}