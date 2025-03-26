using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnOffset = 0.6f;
    [SerializeField] private List<GameObject> _enemiesPrefabs = new List<GameObject>();

    private void OnEnable()
    {
        PlatformSpawner.OnEnemyPlatformSpawned += SpawnEnemy;
    }

    private void OnDisable()
    {
        PlatformSpawner.OnEnemyPlatformSpawned -= SpawnEnemy;
    }

    private void SpawnEnemy(GameObject obj)
    {
        int index = RandomIndexNumber();
        Instantiate(_enemiesPrefabs[index], obj.transform.position + new Vector3(0f, _spawnOffset, 0f), Quaternion.identity, obj.transform);
    }

    private int RandomIndexNumber()
    {
        return Random.Range(0, _enemiesPrefabs.Count);
    }

}
