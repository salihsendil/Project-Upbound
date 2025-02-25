using System.Collections.Generic;
using UnityEngine;

public class PlatformObjectPooling : MonoBehaviour
{
    private const int POOL_SIZE = 20;

    [Header("Prefabs")]
    [SerializeField] private GameObject _simplePlatformPrefab;
    [SerializeField] private GameObject _breakablePlatformPrefab;

    [Header("Spawn Lucks")]
    [SerializeField] private float _breakablePlatformLuck = 20f;

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

    public void ReFillThePool()
    {
        if (GenerateRandomNumber() < _breakablePlatformLuck)
        {
            GameObject obj = GetPlatformFromPool();
            Destroy(obj);
            GameObject newObject = Instantiate(_breakablePlatformPrefab, transform.position, Quaternion.identity);
            ReturnPlatformToPool(newObject);
            
        }
    }

    private float GenerateRandomNumber()
    {
        return UnityEngine.Random.Range(0, 100);
    }

}
