using System;
using UnityEngine;

public class PlatformCreator : MonoBehaviour
{
    [SerializeField] private GameObject _platfromPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        CheckPlayerPos();
    }

    private void CheckPlayerPos()
    {
        throw new NotImplementedException();
    }
}
