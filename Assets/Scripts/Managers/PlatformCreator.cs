using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour
{
    private const int MAX_PLATFROM_COUNT = 10;

    [Header("Position Variables")]
    [SerializeField] private Vector2 _startPoint = new Vector2(0f, -5f);
    [SerializeField] private Vector2 _blocksDistance = new Vector2(0, 2f);
    [SerializeField] private Vector2 _lastPlatfromPos;

    [SerializeField] private List<GameObject> _activeObjects = new List<GameObject>();


    void Start()
    {
        for (int i = 0; i < MAX_PLATFROM_COUNT; i++)
        {
            GameObject obj = PlatformObjectPooling.Instance.GetPlatformFromPool();
            obj.transform.position = _startPoint + i * _blocksDistance;
            //Debug.Log($"start point: {_startPoint}");
            //Debug.Log($"obje pos: {obj.transform.position}");
        }



        //�nce bi object pooling yazal�m ama t�r�ne karar vermesin sadece objeleri olu�tursun


        //karakter yukar� y�nl� hareket ediyor
        //en son olu�turulan platform ile karakterin anl�k konumu k�yaslan�r aradaki fark �rne�in 3f'd���nce 5 tane daha spawnlan�r.
        //en mant�kl� yakla��m bu ayn� zamanda background scroller�nda bizimle birlikte yukar� y�nl� y�kselmesi gerekli
        //ayn� zamanda karakter ile en alttaki platformun uzakl��� �l��l�p ona g�re tekrar havuza g�nderilmeli


    }

    void Update()
    {
        CheckPlayerPos();
    }

    private void CheckPlayerPos()
    {
        //throw new NotImplementedException();
    }

    private Vector2 PlatformRandomPosition()
    {
        //x ve y i�in s�n�rlara g�re random pozisyon �ret bunun i�in boundariesler laz�m onu da gameDatadan �ekicez
        return Vector2.up;
    }
}
