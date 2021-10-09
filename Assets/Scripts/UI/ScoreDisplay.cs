using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private PointsCounter pointsCounter;

    private void OnEnable()
    {
        pointsCounter.ScoreChanged += OnPointsCounterChanged;
    }

    private void OnDisable()
    {
        pointsCounter.ScoreChanged -= OnPointsCounterChanged;
    }

    private void OnPointsCounterChanged(int currentScore)
    {
        _scoreText.text = currentScore.ToString();
    }
}
