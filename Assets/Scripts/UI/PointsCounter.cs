using UnityEngine;
using UnityEngine.Events;

public class PointsCounter : MonoBehaviour
{
    [SerializeField] private Ball _ball;

    private int _scoreValue;

    public event UnityAction<int> ScoreChanged; 

    private void AddScore()
    {
        _scoreValue++;
        ScoreChanged?.Invoke(_scoreValue);
    }

    private void ResetScore()
    {
        _scoreValue = 0;
        ScoreChanged?.Invoke(_scoreValue);
    }

    private void OnEnable()
    {
        _ball.CoinCollected += OnCoinCollected;
        _ball.Fail += OnFail;
    }

    private void OnDisable()
    {
        _ball.CoinCollected += OnCoinCollected;
        _ball.Fail -= OnFail;
    }

    private void OnCoinCollected()
    {
        AddScore();
    }

    private void OnFail()
    {
        ResetScore();
    }
}
