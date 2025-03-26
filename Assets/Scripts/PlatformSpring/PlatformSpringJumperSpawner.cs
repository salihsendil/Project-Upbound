using UnityEngine;

public class PlatformSpringJumperSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _jumperSpringPrefab;
    private Vector3 _spawnOffset = new Vector3(0f, 0.15f, 0f);


    private void OnEnable()
    {
        PlatformSpawner.OnJumperPlatformSpawned += SpawnSpringJumper;
    }
    private void OnDisable()
    {
        PlatformSpawner.OnJumperPlatformSpawned -= SpawnSpringJumper;

    }
    private void SpawnSpringJumper(GameObject obj)
    {
        if (_jumperSpringPrefab != null)
        {
            Instantiate(_jumperSpringPrefab, obj.transform.position + _spawnOffset, Quaternion.identity, obj.transform);
        }
    }
}
