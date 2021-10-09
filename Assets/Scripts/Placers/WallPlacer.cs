using System.Collections.Generic;
using UnityEngine;

public class WallPlacer : ObjectPool
{
    [SerializeField] private Ball _ball;
    [SerializeField] private GameObject[] _wallsTemplates;
    [SerializeField] private float _secondsBetweenSpawn;

    private readonly List<GameObject> _spawnedWalls = new List<GameObject>();
    private const int DistanceToBall = 55;
    private const int MaximumSpawnWallsQuantity = 3;
    private const float WallPositionY = 3.5f;
    private const float WallPositionZ = -3.8f;
    private float _elapsedTime;
    
    private void Start()
    {
        Initialize(_wallsTemplates);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        TryAddWall();
        TryRemoveWall();
    }
    
    private void SetWall(GameObject wall, Vector3 spawnPosition)
    {
        wall.SetActive(true);
        wall.transform.position = spawnPosition;
    }
    
    private void OnEnable()
    {
        _ball.Fail += OnBallFail;
    }

    private void OnDisable()
    {
        _ball.Fail -= OnBallFail;
    }

    private void OnBallFail()
    {
        if (_spawnedWalls != null)
        {
            for (int i = _spawnedWalls.Count-1 ; i >= 0; i--)
            {
                _spawnedWalls[i].gameObject.SetActive(false);
                _spawnedWalls.RemoveAt(i);
            }
        }
    }

    private void TryAddWall()
    {
        if (_elapsedTime >= _secondsBetweenSpawn && _spawnedWalls.Count < MaximumSpawnWallsQuantity && _ball.TryGetComponent(out BallMover ballMover) == ballMover.IsMoving)
        {
            _elapsedTime = 0;
            
            if (TryGetObject(out GameObject wall))
            {
                SetWall(wall, new Vector3(_ball.transform.position.x + DistanceToBall, WallPositionY, WallPositionZ));
                _spawnedWalls.Add(wall);
            }
        }
    }

    private void TryRemoveWall()
    {
        if (_spawnedWalls.Count != 0 && _spawnedWalls[0].transform.position.x < _ball.transform.position.x - (DistanceToBall / 2))
        {
            _spawnedWalls[0].gameObject.SetActive(false);
            _spawnedWalls.RemoveAt(0);
        }
    }
}
