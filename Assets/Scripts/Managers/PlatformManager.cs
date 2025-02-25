using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    private const int MAX_PLATFROM_COUNT = 15;



    [SerializeField] private List<GameObject> _prefabList = new List<GameObject>();
    [SerializeField] private Queue<GameObject> _activePlatforms = new Queue<GameObject>();
    [SerializeField] private float _lastPlatformYPosition = -1f;
    [SerializeField] private float _spawnPlatformOffset = 6f;
    [SerializeField] private Vector2 _startPoint = Vector2.zero;
    [SerializeField] private float _spawnXMin = -2.5f;
    [SerializeField] private float _spawnXMax = 2.5f;
    [SerializeField] private float _spawnYMin = 0.4f;
    [SerializeField] private float _spawnYMax = 1f;

    [SerializeField] private int _totalPlatformCounter = 0;

    private void Awake()
    {
        for (int i = 0; i < MAX_PLATFROM_COUNT; i++)
        {
            if (i == 7 || i == 5 || i == 10 || i==12||i==14)
            {
                GameObject obj = Instantiate(_prefabList[1], transform.position, Quaternion.identity);
                obj.transform.position = _totalPlatformCounter == 0 ? _startPoint : GenerateRandomPosition() + _startPoint;
                SetLastPlatformYPosition(obj.transform.position);
                _activePlatforms.Enqueue(obj);
                _totalPlatformCounter++;
            }
            else
            {
                SpawnPlatform();

            }
        }
    }

    void Start()
    {

    }

    void Update()
    {

        if (CheckPlayerPos() && CheckPlatformAtMaxCount())
        {
            SpawnPlatform();
            DeletePlatform();
        }

        Debug.Log(_activePlatforms.Count);
    }

    private void SpawnPlatform()
    {
        GameObject obj = Instantiate(_prefabList[0], transform.position, Quaternion.identity);
        obj.transform.position = _totalPlatformCounter == 0 ? _startPoint : GenerateRandomPosition() + _startPoint;
        SetLastPlatformYPosition(obj.transform.position);
        _activePlatforms.Enqueue(obj);
        _totalPlatformCounter++;
    }

    private bool CheckPlatformAtMaxCount()
    {
        return _activePlatforms.Count <= MAX_PLATFROM_COUNT;
    }

    private bool CheckPlayerPos()
    {
        return _lastPlatformYPosition - PlayerController.Instance.transform.position.y < _spawnPlatformOffset;
    }

    private void SetLastPlatformYPosition(Vector3 platformPos)
    {
        if (platformPos.y > _lastPlatformYPosition)
        {
            _lastPlatformYPosition = platformPos.y;
        }
    }

    private void DeletePlatform()
    {
        Destroy(_activePlatforms.Dequeue());
    }

    private Vector2 GenerateRandomPosition()
    {
        float randomX = Random.Range(_spawnXMin, _spawnXMax);
        //float randomY = Random.Range(_spawnYMin, _spawnYMax);
        return new Vector2(randomX, _totalPlatformCounter * 1f);
    }


}
