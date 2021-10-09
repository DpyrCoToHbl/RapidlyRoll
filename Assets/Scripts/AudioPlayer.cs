using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _collectCoinAudioClip;
    [SerializeField] private Ball _ball;

    private void Start()
    {
        _audioSource.Play();
    }

    private void OnEnable()
    {
        _ball.CoinCollected += OnCoinCollected;
    }

    private void OnDisable()
    {
        _ball.CoinCollected -= OnCoinCollected;
    }

    private void OnCoinCollected()
    {
        _audioSource.PlayOneShot(_collectCoinAudioClip);
    }
}
