using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    private readonly List<GameObject> _pool = new List<GameObject>();

    protected void Initialize(GameObject prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawnedObject = Instantiate(prefab, _container.transform);
            DeactivateSpawnedObject(spawnedObject);
            AddSpawnedObject(spawnedObject);
        }
    }

    protected void Initialize(GameObject[] prefabs)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawnedObject = Instantiate(prefabs[i], _container.transform);
            DeactivateSpawnedObject(spawnedObject);
            AddSpawnedObject(spawnedObject);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        var inactiveObjectsInPool = _pool.Where(objectInPool => objectInPool.activeSelf == false).ToArray();
        result = inactiveObjectsInPool[Random.Range(0, inactiveObjectsInPool.Length)];
        return result != null;
    }

    private void DeactivateSpawnedObject(GameObject spawnedObject)
    {
        spawnedObject.SetActive(false);
    }

    private void AddSpawnedObject(GameObject spawnedObject)
    {
        _pool.Add(spawnedObject);
    }
}
