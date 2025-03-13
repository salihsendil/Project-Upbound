using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlatformSpawner : MonoBehaviour
{
    public event EventHandler<PlatformEventArgs> OnPlatformSpawned;
    public class PlatformEventArgs : EventArgs
    {
        public Vector3 _pos;
        public PlatformEventArgs(Vector3 pos)
        {
            _pos = pos;
        }
    }

    [Header("References")]
    [Inject] private PlatformObjectPooling _pool;

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
    public Vector2 SpawnStartPoint { get => _spawnStartPoint; }

    /// <summary>
    /// Türüne göre havuzdan platform çeker, havuz gerekli türü karþýlamýyorsa platform oluþturur.
    /// </summary>
    /// <param name="index">Platform Türünü belirlemek için kullanýlýr. (0 = Simple Platform, 1 = Breakable Platform)</param>
    /// <param name="offsetValue">Oyun ilerledikçe platformlarýn arasýndaki uzaklýðý artýrmak için ek offset deðeri.</param>
    public GameObject SpawnPlatform(PlatformType index, float offsetValue)
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
        SetPlatformProperties(obj, offsetValue);
        OnPlatformSpawned?.Invoke(this, new PlatformEventArgs(obj.transform.position));
        _totalPlatformCounter++;
        return obj;
    }

    /// <summary>
    /// Platformun pozisyonunu rastgele ayarlar.
    /// </summary>
    /// <param name="obj">Pozisyonu ayarlanacak platform.</param>
    public void SetPlatformProperties(GameObject obj, float offsetValue)
    {
        obj.transform.position = _totalPlatformCounter == 0 ? _spawnStartPoint : GenerateRandomPosition(obj, offsetValue) + _spawnStartPoint;
        SetHighestPlatformYPosition(obj.transform.position.y);
    }

    /// <summary>
    /// Obje türüne göre belirli aralýkta rastgele pozisyon üretir.
    /// </summary>
    /// <param name="obj">Gönderilen obje türünün sýnýrlarý Y max - min sýnýrlarýný belirler.</param>
    /// <returns></returns>
    private Vector2 GenerateRandomPosition(GameObject obj, float offsetValue)
    {
        BasePlatform platform = obj.GetComponent<BasePlatform>();
        float randomX = UnityEngine.Random.Range(_platformXMin, _platformXMax);
        float randomY = UnityEngine.Random.Range(platform.platformMarginMin, platform.platformMarginMax);
        return new Vector2(randomX, _highestPlatformYPosition + randomY + offsetValue);
    }

    /// <summary>
    /// En yüksekteki platformun y pozisyonunu tutuyor.
    /// </summary>
    /// <param name="yPos">Yeni oluþturulan platformun y pozisyonu parametre olarak alýnýr.</param>
    private void SetHighestPlatformYPosition(float yPos)
    {
        if (yPos > _highestPlatformYPosition)
        {
            _highestPlatformYPosition = yPos;
        }
    }

}
