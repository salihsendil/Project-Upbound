using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlatformSpawner : MonoBehaviour
{
    public static Action<GameObject> OnJumperPlatformSpawned;
    public static Action<GameObject> OnBoostPlatformSpawned;

    [Header("References")]
    [Inject] private PlatformObjectPooling _pool;
    [Inject] private DiContainer _diContainer;

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
    /// T�r�ne g�re havuzdan platform �eker, havuz gerekli t�r� kar��lam�yorsa platform olu�turur.
    /// </summary>
    /// <param name="index">Platform T�r�n� belirlemek i�in kullan�l�r. (0 = Simple Platform, 1 = Breakable Platform)</param>
    /// <param name="offsetValue">Oyun ilerledik�e platformlar�n aras�ndaki uzakl��� art�rmak i�in ek offset de�eri.</param>
    public GameObject SpawnPlatform(PlatformType index, float offsetValue)
    {
        GameObject obj;
        if (index == PlatformType.SimplePlatform)
        {
            obj = _pool.GetPlatformFromPool();
        }
        else
        {
            obj = _diContainer.InstantiatePrefab(_prefabs[(int)index], transform.position, Quaternion.identity, null);

        }
        SetPlatformProperties(obj, offsetValue);
        if (!obj.GetComponent<BreakablePlatform>())
        {
            IsPlatformHaveAnyObject(obj);
        }
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
    /// Obje t�r�ne g�re belirli aral�kta rastgele pozisyon �retir.
    /// </summary>
    /// <param name="obj">G�nderilen obje t�r�n�n s�n�rlar� Y max - min s�n�rlar�n� belirler.</param>
    /// <returns></returns>
    private Vector2 GenerateRandomPosition(GameObject obj, float offsetValue)
    {
        BasePlatform platform = obj.GetComponent<BasePlatform>();
        float randomX = UnityEngine.Random.Range(_platformXMin, _platformXMax);
        float randomY = UnityEngine.Random.Range(platform.platformMarginMin, platform.platformMarginMax);
        return new Vector2(randomX, _highestPlatformYPosition + randomY + offsetValue);
    }

    /// <summary>
    /// En y�ksekteki platformun y pozisyonunu tutuyor.
    /// </summary>
    /// <param name="yPos">Yeni olu�turulan platformun y pozisyonu parametre olarak al�n�r.</param>
    private void SetHighestPlatformYPosition(float yPos)
    {
        if (yPos > _highestPlatformYPosition)
        {
            _highestPlatformYPosition = yPos;
        }
    }

    /// <summary>
    /// Platformun hangi objeye sahip olup olmayaca��n� belirler.
    /// </summary>
    /// <param name="obj">Platform gameobject</param>
    private void IsPlatformHaveAnyObject(GameObject obj)
    {
        int number = UnityEngine.Random.Range(0, 100);
        if (number >= 15 && number <= 100) { return; }
        if (number < 10 && number >= 5){ OnJumperPlatformSpawned?.Invoke(obj); }
        if (number < 5 && number >= 0) { OnBoostPlatformSpawned?.Invoke(obj); }
    }
}
