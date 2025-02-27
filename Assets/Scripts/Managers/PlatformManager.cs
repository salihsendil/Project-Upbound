using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformType { SimplePlatform, BreakablePlatform, OneJumpPlatform, MovingOnXPlatform }

public class PlatformManager : MonoBehaviour
{
    [Header("Lists")]
    [SerializeField] private Queue<GameObject> _activePlatforms = new Queue<GameObject>();

    [Header("References")]
    private PlayerController _player;
    private PlatformObjectPooling _pool;
    private PlatformSpawner _platformSpawner;
    private DifficultyManager _difficultyManager;

    [Header("Platform Properties")]
    [SerializeField] private float _spawnPlatformOffset = 7.5f;

    [Header("Difficulty")]
    [SerializeField] private PlatformType _platformType = PlatformType.SimplePlatform;
    [SerializeField] private float _perlinNoiseValue;

    private void Start()
    {
        _player = PlayerController.Instance;
        _pool = FindFirstObjectByType<PlatformObjectPooling>();
        _platformSpawner = FindFirstObjectByType<PlatformSpawner>();
        _difficultyManager = new DifficultyManager();

        if (_platformSpawner != null && _pool != null)
        {
            for (int i = 0; i < _pool.PoolSize; i++)
            {
                _activePlatforms.Enqueue(_platformSpawner.SpawnPlatform(_platformType));
            }
        }
    }

    void Update()
    {
        HandlePlatformSpawning();
    }

    private void HandlePlatformSpawning()
    {
        if (IsPlayerCloseEnoughToHighestPlatform() /*&& CheckPlatformAtMaxCount()*/)
        {
            DeletePlatform();
            SetPlatformType();
            _activePlatforms.Enqueue(_platformSpawner.SpawnPlatform(_platformType));
        }
    }

    /// <summary>
    /// Perlin Noise deðerine göre platform türünü belirler. (Güncellenecek!)
    /// </summary>
    private void SetPlatformType()
    {
        _perlinNoiseValue = _difficultyManager.PlatformTypePerlinNoise(Time.time, _platformSpawner.HighestPlatformYPosition);

        if (_perlinNoiseValue > 0 && _perlinNoiseValue < 25)
        {
            _platformType = PlatformType.OneJumpPlatform;
        }

        else if (_perlinNoiseValue > 25 && _perlinNoiseValue < 60)
        {
            _platformType = PlatformType.SimplePlatform;
        }

        else if (_perlinNoiseValue > 60 && _perlinNoiseValue < 70)
        {
            _platformType = PlatformType.BreakablePlatform;
        }

        else if (_perlinNoiseValue > 70 && _perlinNoiseValue < 100)
        {
            _platformType = PlatformType.MovingOnXPlatform;
        }
    }

    /// <summary>
    /// En alttaki platformu kuyruktan çýkarýr ve siler.
    /// </summary>
    private void DeletePlatform()
    {
        GameObject obj = _activePlatforms.Dequeue();
        if (obj.GetComponent<SimplePlatform>())
        {
            _pool.ReturnPlatformToPool(obj);
        }
        else
        {
            Destroy(obj);
        }
    }

    /// <summary>
    /// En üstteki platform ile karakter arasýndaki mesafeyi ölçer.
    /// </summary>
    /// <returns>En yukardaki platform ile karakter arasýndaki mesafe yeteri kadar kýsaysa true, yoksa false döndürür. </returns>
    private bool IsPlayerCloseEnoughToHighestPlatform()
    {
        return _platformSpawner.HighestPlatformYPosition - _player.transform.position.y < _spawnPlatformOffset;
    }
}