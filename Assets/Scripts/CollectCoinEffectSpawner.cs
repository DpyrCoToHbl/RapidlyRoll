using UnityEngine;

public class CollectCoinEffectSpawner : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private GameObject _particlePrefab;

    private void OnEnable()
    {
        _ball.CoinCollected += OnCoinCollected;
    }

    private void OnDisable()
    {
        _ball.CoinCollected += OnCoinCollected;
    }

    private void OnCoinCollected()
    {
        Instantiate(_particlePrefab, _ball.transform.position, transform.rotation);
    }
}
