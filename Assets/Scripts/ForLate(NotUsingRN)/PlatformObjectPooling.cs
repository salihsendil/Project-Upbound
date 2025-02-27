using System.Collections.Generic;
using UnityEngine;

public class PlatformObjectPooling : MonoBehaviour
{
    private const int POOL_SIZE = 25;

    [Header("Prefabs")]
    [SerializeField] private GameObject _simplePlatformPrefab;

    [Header("List")]
    [SerializeField] private Queue<GameObject> _platformList = new Queue<GameObject>();

    public int PoolSize { get => POOL_SIZE; }
    public static PlatformObjectPooling Instance { get; private set; }

    private void Awake()
    {
        FillThePool();
        #region SingletonPattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        #endregion
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
        obj.SetActive(false);
        _platformList.Enqueue(obj);
    }

    private void FillThePool()
    {
        if (_simplePlatformPrefab != null)
        {
            for (int i = 0; i < POOL_SIZE; i++)
            {
                GameObject obj = Instantiate(_simplePlatformPrefab, transform.position, Quaternion.identity);
                _platformList.Enqueue(obj);
                obj.SetActive(false);
            }
        }
    }
}
