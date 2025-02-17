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



        //önce bi object pooling yazalým ama türüne karar vermesin sadece objeleri oluþtursun


        //karakter yukarý yönlü hareket ediyor
        //en son oluþturulan platform ile karakterin anlýk konumu kýyaslanýr aradaki fark örneðin 3f'düþünce 5 tane daha spawnlanýr.
        //en mantýklý yaklaþým bu ayný zamanda background scrollerýnda bizimle birlikte yukarý yönlü yükselmesi gerekli
        //ayný zamanda karakter ile en alttaki platformun uzaklýðý ölçülüp ona göre tekrar havuza gönderilmeli


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
        //x ve y için sýnýrlara göre random pozisyon üret bunun için boundariesler lazým onu da gameDatadan çekicez
        return Vector2.up;
    }
}
