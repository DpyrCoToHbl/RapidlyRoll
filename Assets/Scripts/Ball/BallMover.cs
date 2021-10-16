using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class BallMover : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Ball _ball;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private float _strafeForce;
    [SerializeField] private float _jumpForce;
    
    private const float DefaultVelocity  = 10;
    private const float VelocityModifier = 0.1f;
    private const float MaximumVelocity = 25;
    private float _currentVelocity;
    private Rigidbody _rigidbody;
    private bool _isGrounded;

    public bool IsMoving { get; private set; }

    public event UnityAction VelocityChanged;

    private void Start()
    {
        transform.position = _startPosition;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = Vector3.zero;
    }
    
    private void FixedUpdate()
    {
        CheckGround();
        
        if (Input.GetKey(KeyCode.D) && _currentVelocity == 0)
        {
            _currentVelocity = DefaultVelocity;
            IsMoving = true;
        }
        
        Move();
        Jump();
    }

    private void Move()
    {
        Vector3 velocity = _rigidbody.velocity;
        velocity.x = _currentVelocity;
        _rigidbody.velocity = velocity;

        if (Input.GetKey((KeyCode.W)))
            _rigidbody.AddForce(Vector3.forward * _strafeForce);

        if (Input.GetKey((KeyCode.S)))
            _rigidbody.AddForce(Vector3.back * _strafeForce);
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && _isGrounded)
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private void CheckGround()
    {
        const int SphereRadius = 1;
        _isGrounded = Physics.CheckSphere(transform.position, SphereRadius,  _groundLayer);
    }

    private void OnEnable()
    {
        _ball.Fail += OnFail;
        _ball.CoinCollected += OnCoinCollected;
        _ball.Stuck += OnStuck;
        _ball.Freed += OnFreed;
    }

    private void OnDisable()
    {
        _ball.Fail -= OnFail;
        _ball.CoinCollected -= OnCoinCollected;
        _ball.Stuck -= OnStuck;
        _ball.Freed -= OnFreed;
    }

    private void OnFail()
    {
        ResetBall();
    }

    private void OnCoinCollected()
    {
        if (_currentVelocity < MaximumVelocity)
        {
            _currentVelocity += VelocityModifier;
            VelocityChanged?.Invoke();
        }
    }

    private void ResetBall()
    {
        transform.position = _startPosition;
        _currentVelocity = 0;
        IsMoving = false;
    }

    private void OnStuck()
    {
        IsMoving = false;
    }

    private void OnFreed()
    {
        if (_currentVelocity > 0)
            IsMoving = true;
    }
}
