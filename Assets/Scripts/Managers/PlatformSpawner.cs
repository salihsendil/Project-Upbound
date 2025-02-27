using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlatformObjectPooling _pool;

    [Header("Platform List")]
    [SerializeField] private List<GameObject> _prefabs = new List<GameObject>();

    [Header("Platform Boundaries")]
    [SerializeField] private Vector2 _spawnStartPoint = Vector2.zero;
    [SerializeField] private float _platformXMin = -2.75f;
    [SerializeField] private float _platformXMax = 2.75f;
    [SerializeField] private float _highestPlatformYPosition;
    [SerializeField] private int _totalPlatformCounter = 0;

    [Header("Getters - Setters")]
    public float HighestPlatformYPosition { get => _highestPlatformYPosition; }

    private void Awake()
    {
        _pool = FindFirstObjectByType<PlatformObjectPooling>();
    }

    /// <summary>
    /// Türüne göre havuzdan platform çeker, havuz gerekli türü karþýlamýyorsa platform oluþturur.
    /// </summary>
    /// <param name="index">Platform Türünü belirlemek için kullanýlýr. (0 = Simple Platform, 1 = Breakable Platform)</param>
    public GameObject SpawnPlatform(PlatformType index)
    {
        GameObject obj;
        if (index == PlatformType.SimplePlatform)
        {
            obj = _pool.GetPlatformFromPool();
        }
        else
        {
            obj = Instantiate(_prefabs[(int)index], transform.position, Quaternion.identity);

        }
        SetPlatformProperties(obj);
        _totalPlatformCounter++;
        return obj;
    }

    /// <summary>
    /// Platformun pozisyonunu rastgele ayarlar.
    /// </summary>
    /// <param name="obj">Pozisyonu ayarlanacak platform.</param>
    public void SetPlatformProperties(GameObject obj)
    {
        obj.transform.position = _totalPlatformCounter == 0 ? _spawnStartPoint : GenerateRandomPosition(obj) + _spawnStartPoint;
        SetHighestPlatformYPosition(obj.transform.position.y);
    }

    /// <summary>
    /// Obje türüne göre belirli aralýkta rastgele pozisyon üretir.
    /// </summary>
    /// <param name="obj">Gönderilen obje türünün sýnýrlarý Y max - min sýnýrlarýný belirler.</param>
    /// <returns></returns>
    private Vector2 GenerateRandomPosition(GameObject obj)
    {
        BasePlatform platform = obj.GetComponent<BasePlatform>();
        float randomX = Random.Range(_platformXMin, _platformXMax);
        float randomY = Random.Range(platform.platformMarginMin, platform.platformMarginMax);
        return new Vector2(randomX, _highestPlatformYPosition + randomY);
    }

    private void SetHighestPlatformYPosition(float yPos)
    {
        if (yPos > _highestPlatformYPosition)
        {
            _highestPlatformYPosition = yPos;
        }
    }

}
