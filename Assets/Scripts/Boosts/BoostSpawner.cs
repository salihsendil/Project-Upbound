using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BoostSpawner : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    [Inject] private PlatformSpawner _platformSpawner;
    [SerializeField] private float _boostSpawnOffset = 0.3f;
    [SerializeField] private List<GameObject> _boostList = new List<GameObject>();

    void OnEnable()
    {
        _platformSpawner.OnPlatformSpawned += SpawnBoost;
    }

    void OnDisable()
    {
        _platformSpawner.OnPlatformSpawned -= SpawnBoost;
    }

    private void SpawnBoost(object sender, PlatformSpawner.PlatformEventArgs e)
    {
        GameObject obj = ChooseBoostType();
        if (obj == null) { return; }
        _diContainer.InstantiatePrefab(obj, e._transform.position + new Vector3(0f, _boostSpawnOffset, 0f), Quaternion.identity, e._transform);
    }

    private GameObject ChooseBoostType()
    {
        int number = GenerateBoostSpawnChanceNumber();

        if (number >= 4 && number <= 98) { return null; }
        if (number < 4 && number >= 2) { return _boostList[0]; }
        if (number < 2) { return _boostList[1]; }
        return _boostList[2];
    }

    private int GenerateBoostSpawnChanceNumber()
    {
        return UnityEngine.Random.Range(0, 100);
    }
}
