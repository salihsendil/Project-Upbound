using System;
using UnityEngine;
using Zenject;

public class BoostSpawner : MonoBehaviour
{
    [Inject] private PlatformSpawner _platformSpawner;
    [SerializeField] private float _boostSpawnOffset = 0.2f;
    [SerializeField] private GameObject _boostPrefab;
    [SerializeField] private int _boostSpawnChange = 50;
    private int number;

    void Start()
    {
        _platformSpawner.OnPlatformSpawned += SpawnBoost;
    }

    void Update()
    {
        
    }

    private bool CanSpawnBoost()
    {
        number = UnityEngine.Random.Range(0, 101);
        return number < _boostSpawnChange;
    }

    private void SpawnBoost(object sender, PlatformSpawner.PlatformEventArgs e)
    {
        if (CanSpawnBoost())
        {
            GameObject boost = Instantiate(_boostPrefab, transform.position, Quaternion.identity);
            boost.transform.position = new Vector3(e._pos.x, e._pos.y + _boostSpawnOffset, e._pos.z);
        }
        //Debug.Log("Platform Spawned, I invoked from PlatformSpawner class, I am BoostSpawner class, here is the platform position: " + e._pos);
    }



}
