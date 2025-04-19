using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BackgroundScroller : MonoBehaviour
{
    [Inject] private PlayerController _player;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _smoothFactor = 0.1f;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        if (_player)
        {
            _player.OnPlayerGoUp += ScrollBackground;
        }
    }

    private void ScrollBackground(float travaledValue)
    {
        Vector2 velocity = Vector2.zero;
        Vector2 currentOffset = _meshRenderer.material.mainTextureOffset;
        Vector2 targetOffset = currentOffset + new Vector2(0f, travaledValue * _speed * Time.deltaTime);
        _meshRenderer.material.mainTextureOffset = Vector2.SmoothDamp(currentOffset, targetOffset, ref velocity, _smoothFactor);

        //fixlenecek

    }
}
