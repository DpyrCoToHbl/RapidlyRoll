using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BallMover))]
public class Ball : MonoBehaviour
{    
    [SerializeField] private LayerMask _wallLayer;

    private float _stayInWallColliderTime;
    
    public event UnityAction CoinCollected;
    public event UnityAction Fail;
    public event UnityAction Stuck;
    public event UnityAction Freed;

    private void FixedUpdate()
    {
        CheckWall();
        
        if (transform.position.y <= 0.5f)
        {
            Fail?.Invoke();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Coin coin))
        {
            CoinCollected?.Invoke();
        }
    }
    
    private void CheckWall()
    {
        const float SphereRadius = 0.7f;
        const int SecondsBeforeFail = 2;

        if (Physics.CheckSphere(transform.position, SphereRadius, _wallLayer))
        {
            Stuck?.Invoke();
            _stayInWallColliderTime += Time.deltaTime;
            
            if (_stayInWallColliderTime >= SecondsBeforeFail)
            {
                Fail?.Invoke();
            }
        }
        else
        {
            Freed?.Invoke();
            _stayInWallColliderTime = 0;
        }
    }
}
