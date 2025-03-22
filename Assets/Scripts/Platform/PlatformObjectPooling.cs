using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlatformObjectPooling : MonoBehaviour
{
    private const int POOL_SIZE = 20;
    [Inject] private DiContainer _diContainer;

    [Header("Prefabs")]
    [SerializeField] private GameObject _simplePlatformPrefab;

    [Header("List")]
    [SerializeField] private Queue<GameObject> _platformList = new Queue<GameObject>();


    public int PoolSize { get => POOL_SIZE; }

    private void Awake()
    {
        FillThePool();
    }

    public GameObject GetPlatformFromPool()
    {
        if (_platformList.Count > 0)
        {
            GameObject obj = _platformList.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        return null;
    }

    public void ReturnPlatformToPool(GameObject obj)
    {
        if (obj.transform.childCount > 0)
        {
            Destroy(obj.transform.GetChild(0).gameObject);
        }
        obj.SetActive(false);
        _platformList.Enqueue(obj);
    }

    private void FillThePool()
    {
        if (_simplePlatformPrefab != null)
        {
            for (int i = 0; i < POOL_SIZE; i++)
            {
                GameObject obj = _diContainer.InstantiatePrefab(_simplePlatformPrefab, transform.position, Quaternion.identity, null);
                obj.SetActive(false);
                _platformList.Enqueue(obj);
            }
        }
    }
}
