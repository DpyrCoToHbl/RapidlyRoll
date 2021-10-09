using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private BallMover _ball;
	[SerializeField] private float _damping = 10f;
	[SerializeField] private Vector2 _offset = new Vector2(1f, 6f);

	private void Start()
	{
		FindBall();
	}

	private void FindBall()
	{
		transform.position = new Vector3(transform.position.x, _ball.transform.position.y + _offset.y, transform.position.z);
	}

	private void FixedUpdate()
	{
		if (_ball)
		{
			var target = new Vector3(_ball.transform.position.x - _offset.x, _ball.transform.position.y + _offset.y, transform.position.z);
			Vector3 currentPosition = Vector3.Lerp(transform.position, target, _damping * Time.deltaTime);
			transform.position = currentPosition;
		}
	}

	private void OnEnable()
	{
		_ball.VelocityChanged += OnVelocityChanged;
	}

	private void OnDisable()
	{
		_ball.VelocityChanged -= OnVelocityChanged;
	}

	private void OnVelocityChanged()
	{
		_offset.x -= 0.01f;
	}
}