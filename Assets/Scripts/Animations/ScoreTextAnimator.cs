using UnityEngine;

public class ScoreTextAnimator : MonoBehaviour
{
   [SerializeField] private PointsCounter pointsCounter;
   [SerializeField] private Animator _animator;

   private const string ScoreChangeAnimationTriggerName = "ChangeScoreAnimation";

   private void OnEnable()
   {
      pointsCounter.ScoreChanged += OnPointsCounterChanged;
   }

   private void OnDisable()
   {
      pointsCounter.ScoreChanged -= OnPointsCounterChanged;
   }

   private void OnPointsCounterChanged(int score)
   {
      _animator.SetTrigger(ScoreChangeAnimationTriggerName);
   }
}
