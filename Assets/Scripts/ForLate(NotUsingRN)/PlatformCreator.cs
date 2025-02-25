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


        //önce bi object pooling yazalým ama türüne karar vermesin sadece objeleri oluþtursun


        //karakter yukarý yönlü hareket ediyor
        //en son oluþturulan platform ile karakterin anlýk konumu kýyaslanýr aradaki fark örneðin 3f'düþünce 5 tane daha spawnlanýr.
        //en mantýklý yaklaþým bu ayný zamanda background scrollerýnda bizimle birlikte yukarý yönlü yükselmesi gerekli


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
        //x ve y için sýnýrlara göre random pozisyon üret bunun için boundariesler lazým onu da gameDatadan çekicez

        return new Vector2(randomX, randomY);
    }

    private Vector3 GetPlayerPos()
    {
        return PlayerController.Instance.transform.position;
    }

}
