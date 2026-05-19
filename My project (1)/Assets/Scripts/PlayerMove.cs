using UnityEngine;

/// <summary>
/// 플레이어 이동을 관리하는 클래스
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpd;

    private Rigidbody2D _rigid;
    private InputController _input;

    private void Awake()
    {
        _input = GetComponent<InputController>();
        _rigid = GetComponent<Rigidbody2D>();

        _input.OnMoveEvent += HandleMove;
    }

    private void HandleMove(float x)
    {
        var vel = new Vector2(x * _moveSpd, _rigid.velocity.y);
        _rigid.velocity = vel;
    }

    private void OnDestroy()
    {
        if (_input != null)
        {
            _input.OnMoveEvent -= HandleMove;
        }
    }
}