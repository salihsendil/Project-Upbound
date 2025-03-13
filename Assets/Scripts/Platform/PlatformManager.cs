using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum PlatformType { SimplePlatform, BreakablePlatform, OneJumpPlatform, MovingOnXPlatform }

public class PlatformManager : MonoBehaviour
{
    [Header("Lists")]
    private Queue<GameObject> _activePlatforms = new Queue<GameObject>();

    [Header("References")]
    private PlayerController _player;
    private PlatformObjectPooling _pool;
    private PlatformSpawner _platformSpawner;
    private DifficultyManager _difficultyManager;

    [Header("Platform Properties")]
    [SerializeField] private float _spawnPlatformOffset = 6.5f;
    [SerializeField] private bool _hasBreakableSpawned = false;

    [Header("Difficulty")]
    [SerializeField] private PlatformType _platformType = PlatformType.SimplePlatform;
    [SerializeField] private float _perlinNoiseValue;

    [Inject]
    private void ZenjectSetup(PlayerController player, PlatformObjectPooling pool, PlatformSpawner platformSpawner, DifficultyManager difficultyManager)
    {
        _player = player;
        _pool = pool;
        _platformSpawner = platformSpawner;
        _difficultyManager = difficultyManager;
    }

    private void Start()
    {
        if (_platformSpawner != null && _pool != null)
        {
            for (int i = 0; i < _pool.PoolSize; i++)
            {
                _activePlatforms.Enqueue(_platformSpawner.SpawnPlatform(_platformType, _difficultyManager.PlatformOffsetIncreaser()));
            }
        }
    }

    void Update()
    {
        HandlePlatformSpawning();
    }

    private void HandlePlatformSpawning()
    {
        if (IsPlayerFarAwayFromLowestPlatform())
        {
            DeletePlatform();
            SetPlatformType();
            _activePlatforms.Enqueue(_platformSpawner.SpawnPlatform(_platformType, _difficultyManager.PlatformOffsetIncreaser()));
        }
    }

    /// <summary>
    /// Perlin Noise deðerine göre platform türünü belirler. (Güncellenecek!)
    /// </summary>
    private void SetPlatformType()
    {
        _perlinNoiseValue = _difficultyManager.PlatformTypePerlinNoise(Time.time, _platformSpawner.HighestPlatformYPosition);

        if (_perlinNoiseValue > 0 && _perlinNoiseValue < 20)
        {
            if (!_hasBreakableSpawned)
            {
                _platformType = PlatformType.BreakablePlatform;
                _hasBreakableSpawned = true;
            }
            else
            {
                _platformType = PlatformType.SimplePlatform;
            }
        }
        else if (_perlinNoiseValue >= 20 && _perlinNoiseValue < 55)
        {
            _platformType = PlatformType.SimplePlatform;
        }
        else if (_perlinNoiseValue >= 55 && _perlinNoiseValue < 75)
        {
            _platformType = PlatformType.OneJumpPlatform;
        }
        else if (_perlinNoiseValue >= 75 && _perlinNoiseValue <= 100)
        {
            _platformType = PlatformType.MovingOnXPlatform;
        }

        if (_platformType != PlatformType.BreakablePlatform)
        {
            _hasBreakableSpawned = false;
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
    /// En alttaki platform ile karakter arasýndaki mesafeyi ölçer.
    /// </summary>
    /// <returns>En alttaki platform ile karakter arasýndaki mesafe yeteri kadar uzunsa true, yoksa false döndürür. </returns>
    private bool IsPlayerFarAwayFromLowestPlatform()
    {
        return _player.transform.position.y - _activePlatforms.Peek().transform.position.y > _spawnPlatformOffset;
    }

}