using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpd = 5f;

    private Rigidbody2D _rigid;
    private InputController _input;

    private float _moveLockTimer = 0f;
    private float _speedBonus = 0f;

    public float MoveSpeed
    {
        get
        {
            return _moveSpd;
        }
    }

    private void Awake()
    {
        _input = GetComponent<InputController>();
        _rigid = GetComponent<Rigidbody2D>();

        _input.OnMoveEvent += HandleMove;
    }

    private void Update()
    {
        if (_moveLockTimer > 0f)
        {
            _moveLockTimer -= Time.deltaTime;
        }
    }

    public void LockMove(float time)
    {
        _moveLockTimer = time;
    }

    public void SetSpeedBonus(float bonus)
    {
        _speedBonus = bonus;
    }

    private void HandleMove(float x)
    {
        if (_moveLockTimer > 0f)
        {
            return;
        }

        float finalSpeed = _moveSpd + _speedBonus;

        _rigid.velocity = new Vector2(x * finalSpeed, _rigid.velocity.y);
    }

    private void OnDestroy()
    {
        if (_input != null)
        {
            _input.OnMoveEvent -= HandleMove;
        }
    }
}