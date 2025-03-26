using System;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    public event Action<float> OnPlayerGoUp;

    [Header("References")]
    private Rigidbody2D _rb;
    private BoxCollider2D _jumpCollider;
    [Inject] private InputManager _inputManager;

    [Header("Movement Variables")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _highestYPosition = 0f;
    [SerializeField] private bool _canJump = true;

    [Header("X Limits")]
    private float _rightLimit = 3f;
    private float _leftLimit = -3f;

    [Header("Health")]
    private bool _isPlayerDead;

    public float HighestYPosition { get => _highestYPosition; }
    public bool IsPlayerDead { get => _isPlayerDead; set => _isPlayerDead = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _jumpCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (!_isPlayerDead)
        {
            MovePlayerOnX();
            SetPlayerXLimits();
        }
    }

    private void FixedUpdate()
    {
        if (!_isPlayerDead)
        {
            ControllColliderForPlayerJump();
            SetHighestYPosition();
        }
    }

    /// <summary>
    /// Karakterin h�z�na ba�l� olarak collider'ini a��p kapatarak z�plama durumunu y�netir.
    /// </summary>
    private void ControllColliderForPlayerJump()
    {
        _jumpCollider.enabled = _rb.linearVelocityY < Mathf.Epsilon;
    }

    /// <summary>
    /// Karakterin Input Deltaya ba�l� X eksen hareketini sa�lar.
    /// </summary>
    private void MovePlayerOnX()
    {
        Vector3 targetPos = new Vector3(_inputManager.TouchInput.x, 0f, 0f);
        transform.position += targetPos * _speed;
    }

    /// <summary>
    /// Karakteri Rigidbody2D ile z�plat�r.
    /// </summary>
    /// <param name="jumpForce">Z�plama kuvveti</param>
    public void PlayerJump(float jumpForce)
    {
        _rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
    }


    /// <summary>
    /// Karakterin ��kabildi�i en y�ksek Y pozisyonunu kaydeder.
    /// </summary>
    private void SetHighestYPosition()
    {
        if (transform.position.y > _highestYPosition)
        {
            float travaledRoad = transform.position.y - _highestYPosition;
            OnPlayerGoUp?.Invoke(travaledRoad);
            _highestYPosition = transform.position.y;
        }
    }

    /// <summary>
    /// Karakterin ekran�n sa��ndan ve solundan ��kma durumuna g�re pozisyon de�i�ikli�ini y�netir.
    /// </summary>
    private void SetPlayerXLimits()
    {
        if (transform.position.x > _rightLimit)
        {
            transform.position = new Vector3(_leftLimit, transform.position.y, transform.position.z);
        }

        if (transform.position.x < _leftLimit)
        {
            transform.position = new Vector3(_rightLimit, transform.position.y, transform.position.z); ;
        }
    }

}
