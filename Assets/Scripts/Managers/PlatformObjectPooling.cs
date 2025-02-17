using System.Collections.Generic;
using UnityEngine;

public class PlatformObjectPooling : MonoBehaviour
{
    private const int POOL_SIZE = 20;

    [SerializeField] private GameObject _platfromPrefab;
    [SerializeField] private Queue<GameObject> _platformList = new Queue<GameObject>();

    public static PlatformObjectPooling Instance { get; private set; }

    private void Awake()
    {
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

    void Start()
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
        obj.SetActive(false);
        _platformList.Enqueue(obj);
    }

    private void FillThePool()
    {
        if (_platfromPrefab != null)
        {
            for (int i = 0; i < POOL_SIZE; i++)
            {
                GameObject obj = Instantiate(_platfromPrefab, transform.position, Quaternion.identity);
                _platformList.Enqueue(obj);
                obj.SetActive(false);
            }
        }
    }

}
