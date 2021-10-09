using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class BackgroundMover : MonoBehaviour
{
    [SerializeField] private GameObject _ball;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _defaultSpeed;

    private RawImage _image;
    private float _imagePositionX;
    private const float SpeedModifier = 0.001f;
    
    private void Awake()
    {
        _currentSpeed = _defaultSpeed;
        _image = GetComponent<RawImage>();
    }

    private void Update()
    {
        if(_ball.GetComponent<BallMover>().IsMoving)
            _imagePositionX += _currentSpeed * Time.deltaTime;

        if (_imagePositionX > 1)
            _imagePositionX = 0;

        _image.uvRect = new Rect(_imagePositionX, 0, _image.uvRect.width, _image.uvRect.height);
    }

    private void OnEnable()
    {
        _ball.GetComponent<BallMover>().VelocityChanged += OnBallVelocityChanged;
        _ball.GetComponent<Ball>().Fail += OnFail;
    }

    private void OnDisable()
    {
        _ball.GetComponent<BallMover>().VelocityChanged -= OnBallVelocityChanged;
        _ball.GetComponent<Ball>().Fail -= OnFail;
    }

    private void OnBallVelocityChanged()
    {
        _currentSpeed += SpeedModifier;
    }

    private void OnFail()
    {
        _currentSpeed = _defaultSpeed;
    }
}
