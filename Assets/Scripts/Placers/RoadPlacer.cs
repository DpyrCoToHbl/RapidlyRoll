using System.Collections.Generic;
using UnityEngine;

public class RoadPlacer : ObjectPool
{
    [SerializeField] private GameObject _roadPrefab;
    [SerializeField] private Ball _ball;
    [SerializeField] private GameObject firstRoad;

    private readonly List<GameObject> _spawnedRoads = new List<GameObject>();
    private const int MaximumSpawnedRoads = 14;
    private const int PositionAdjustment = 55;

    private void Awake()
    {
        _spawnedRoads.Add(firstRoad);
        Initialize(_roadPrefab);
    }

    private void Update()
    {
        if (_ball.transform.position.x > _spawnedRoads[_spawnedRoads.Count - 1].transform.position.x - PositionAdjustment)
        {
            if (TryGetObject(out GameObject road))
            {
                SetRoad(road,  new Vector3(_spawnedRoads[_spawnedRoads.Count - 1].transform.position.x + GetLastRoadBlockLenght(),0,0));
                _spawnedRoads.Add(road);
            }
        }
        
        if (_spawnedRoads.Count >= MaximumSpawnedRoads)
        {
            _spawnedRoads[0].gameObject.SetActive(false);
            _spawnedRoads.RemoveAt(0);
        }
    }

    private void SetRoad(GameObject road, Vector3 spawnPosition)
    {
        road.SetActive(true);
        road.transform.position = spawnPosition;
    }

    private float GetLastRoadBlockLenght()
    {
        return _spawnedRoads[_spawnedRoads.Count - 1].transform.lossyScale.x;
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
        for (int i = 0; i < _spawnedRoads.Count; i++)
        {
            _spawnedRoads[i].gameObject.SetActive(false);
            _spawnedRoads.RemoveAt(i);
        }
        
        if (TryGetObject(out GameObject road))
        {
            SetRoad(road,  new Vector3(_ball.transform.position.x,0,0));
            _spawnedRoads.Add(road);
        }
    }
}
