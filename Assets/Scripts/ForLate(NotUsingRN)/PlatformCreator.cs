using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour
{
    private const int MAX_PLATFROM_COUNT = 15;

    [Header("Position Variables")]
    [SerializeField] private Vector2 _startPoint = new Vector2(0f, -0.5f);
    [SerializeField] private Vector2 _blocksDistance = new Vector2(0, 1f);
    [SerializeField] private float _randomXPosBoundary = 2.6f;
    [SerializeField] private float _randomYPosMinBoundary = 0.5f;
    [SerializeField] private float _randomYPosMaxBoundary = 1.5f;
    [SerializeField] private Vector2 _lastPlatfromPos;
    [SerializeField] private float _deleteOffset = 10f;
    [SerializeField] private int _deleteIndex = 0;
    [SerializeField] private int _platformCounter = 0;

    [SerializeField] private List<GameObject> _activeObjects = new List<GameObject>();


    void Start()
    {
        for (int i = 0; i < MAX_PLATFROM_COUNT; i++)
        {
            GeneratePlatform(i);

            //Debug.Log($"start point: {_startPoint}");
            //Debug.Log($"obje pos: {obj.transform.position}");
        }


        //�nce bi object pooling yazal�m ama t�r�ne karar vermesin sadece objeleri olu�tursun


        //karakter yukar� y�nl� hareket ediyor
        //en son olu�turulan platform ile karakterin anl�k konumu k�yaslan�r aradaki fark �rne�in 3f'd���nce 5 tane daha spawnlan�r.
        //en mant�kl� yakla��m bu ayn� zamanda background scroller�nda bizimle birlikte yukar� y�nl� y�kselmesi gerekli


    }

    void Update()
    {
        DeletePlatform();

    }

    private void GeneratePlatform(int index)
    {
        GameObject obj = PlatformObjectPooling.Instance.GetPlatformFromPool();
        obj.transform.position = _startPoint + (index == 0 ? Vector3.zero : index * _blocksDistance + GenerateRandomPlatformPosition());
        _activeObjects.Add(obj);
        PlatformObjectPooling.Instance.ReFillThePool();
        _platformCounter++;
    }


    private void DeletePlatform()
    {
        if (GetPlayerPos().y - _activeObjects[_deleteIndex].transform.position.y > _deleteOffset)
        {
            PlatformObjectPooling.Instance.ReturnPlatformToPool(_activeObjects[_deleteIndex].gameObject);
            GeneratePlatform(_platformCounter);
            _deleteIndex++;
        }
    }

    private Vector2 GenerateRandomPlatformPosition()
    {
        float randomX = UnityEngine.Random.Range(-_randomXPosBoundary, _randomXPosBoundary);
        float randomY = UnityEngine.Random.Range(_randomYPosMinBoundary, _randomYPosMaxBoundary);
        //x ve y i�in s�n�rlara g�re random pozisyon �ret bunun i�in boundariesler laz�m onu da gameDatadan �ekicez

        return new Vector2(randomX, randomY);
    }

    private Vector3 GetPlayerPos()
    {
        return PlayerController.Instance.transform.position;
    }

}
