using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BoostSpawner : MonoBehaviour
{
    [Inject] private PlatformSpawner _platformSpawner;
    [SerializeField] private float _boostSpawnOffset = 0.2f;
    [SerializeField] private GameObject _boostPrefab;
    [SerializeField] private int _boostSpawnChange = 50;
    [SerializeField] private List<GameObject> _boostList = new List<GameObject>();
    private int number;

    void Start()
    {
        _platformSpawner.OnPlatformSpawned += SpawnBoost;
    }

    void Update()
    {

    }

    private void SpawnBoost(object sender, PlatformSpawner.PlatformEventArgs e)
    {
        GameObject obj = ChooseBoostType();
        if (obj == null) { return; }
        GameObject boost = Instantiate(obj, transform.position, Quaternion.identity);
        boost.transform.position = new Vector3(e._pos.x, e._pos.y + _boostSpawnOffset, e._pos.z);

        //Debug.Log("Platform Spawned, I invoked from PlatformSpawner class, I am BoostSpawner class, here is the platform position: " + e._pos);
    }

    private GameObject ChooseBoostType()
    {
        int number = GenerateBoostSpawnChanceNumber();
        if (number > 20 && number < 90)
        {
            return null;
        }

        else if (number < 20 && number > 10)
        {
            return _boostList[0];
        }

        else if (number < 10)
        {
            return _boostList[1];
        }

        else
        {
            return _boostList[2];
        }
    }

    private int GenerateBoostSpawnChanceNumber()
    {
        return UnityEngine.Random.Range(0, 100);
    }


}
