using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BoostSpawner : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    [SerializeField] private float _boostSpawnOffset = 0.5f;
    [SerializeField] private List<GameObject> _boostList = new List<GameObject>();

    void OnEnable()
    {
        PlatformSpawner.OnBoostPlatformSpawned += SpawnBoost;
    }

    void OnDisable()
    {
        PlatformSpawner.OnBoostPlatformSpawned -= SpawnBoost;
    }

    private void SpawnBoost(GameObject obj)
    {
        GameObject gameObject = ChooseBoostType();
        if (gameObject == null) { return; }
        _diContainer.InstantiatePrefab(gameObject, obj.transform.position + new Vector3(0f, _boostSpawnOffset, 0f), Quaternion.identity, obj.transform);
    }

    private GameObject ChooseBoostType()
    {
        int number = GenerateBoostSpawnChanceNumber();

        if (number >= 30 && number <= 100) { return null; }
        if (number < 30 && number >= 20) { return _boostList[0]; }
        if (number < 20 && number >= 10) { return _boostList[1]; }
        return _boostList[2];
    }

    private int GenerateBoostSpawnChanceNumber()
    {
        return UnityEngine.Random.Range(0, 100);
    }
}
