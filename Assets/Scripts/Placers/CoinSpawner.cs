using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinSpawner : ObjectPool
{
   [SerializeField] private GameObject _coinPrefab;
   [SerializeField] private float _secondsBetweenSpawn;
   [SerializeField] private Ball _ball;
   [SerializeField] private LayerMask wrongSpawnPlaceLayer;

   private readonly List<GameObject> _spawnedCoins = new List<GameObject>();
   private const int DistanceToBall = 50;
   private const int MaximumSpawnCoinsQuantity = 5;  
   private const int PositionAdjustment = 5;
   private const int SphereRadius = 10;
   private const int RayDistance = 10;
   private float _elapsedTime = 0;
   private Vector3 _spawnPosition;


   private void Start()
   {
      Initialize(_coinPrefab.gameObject);
   }

   private void Update()
   {
      _elapsedTime += Time.deltaTime;
      TryAddCoin();
      TryRemoveCoin();
   }

   private void SetCoin(GameObject coin, Vector3 spawnPosition)
   {
      coin.SetActive(true);
      coin.transform.position = spawnPosition;
   }

   private float GetRandomYPosition()
   {
      float[] yPositions = {1.5f, 3};
      return yPositions[Random.Range(0, yPositions.Length)];
   }

   private float GetRandomZPosition()
   {
      float[] zPositions = {-2.5f, 0, 2.5f};
      return zPositions[Random.Range(0, zPositions.Length)];
   }

   private void TryAddCoin()
   {
      if (_elapsedTime >= _secondsBetweenSpawn && _spawnedCoins.Count < MaximumSpawnCoinsQuantity && _ball.TryGetComponent(out BallMover ballMover) == ballMover.IsMoving)
      {
         _elapsedTime = 0;
         var _spawnPosition = new Vector3(_ball.transform.position.x + DistanceToBall, GetRandomYPosition(), GetRandomZPosition());
         var _adjustedSpawnPosition = new Vector3(_spawnPosition.x + PositionAdjustment, _spawnPosition.y, _spawnPosition.z);

         for (int i = 0; i < MaximumSpawnCoinsQuantity; i++)
         {
            _spawnPosition.x += i;
            
            if (TryGetObject(out GameObject coin))
            {
               if (Physics.Raycast(_spawnPosition, Vector3.down, out var hit, RayDistance))
               {
                  Collider[] collidersInsideOverlapSphere = new Collider[1];
                  int numberOfCollidersFound = Physics.OverlapSphereNonAlloc(hit.point, SphereRadius, collidersInsideOverlapSphere, wrongSpawnPlaceLayer);
                  SetCoin(coin, numberOfCollidersFound == 0 ? _spawnPosition : _adjustedSpawnPosition);
               }

               _spawnedCoins.Add(coin);
            }
         }
      }
   }

   private void TryRemoveCoin()
   {
      if (_spawnedCoins.Count != 0 && _spawnedCoins[0].transform.position.x < _ball.transform.position.x - DistanceToBall / 2)
      {
         _spawnedCoins[0].gameObject.SetActive(false);
         _spawnedCoins.RemoveAt(0);
      }
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
      if (_spawnedCoins != null)
      {
         for (int i = _spawnedCoins.Count-1 ; i >= 0; i--)
         {
            _spawnedCoins[i].gameObject.SetActive(false);
            _spawnedCoins.RemoveAt(i);
         }
      }
   }
}
