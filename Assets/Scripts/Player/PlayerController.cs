using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    public event Action<float> OnPlayerGoUp;
    public event Action OnPlayerDeath;

    [Header("References")]
    private Rigidbody2D _rb;
    private BoxCollider2D _jumpCollider;
    [Inject] private InputManager _inputManager;
    [Inject] private PlatformManager _platformManager;

    [Header("Movement Variables")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _highestYPosition = 0f;
    [SerializeField] private float _lowestYPosition = -5f;
    [SerializeField] private bool _canJump = true;

    [Header("X Limits")]
    private float _rightLimit = 3f;
    private float _leftLimit = -3f;

    [Header("Health")]
    private bool _isPlayerDead;
    private float _deathDelay = 0.5f;

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
            HandlePlayerDeath();
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
        AudioManager.Instance.PlaySound(SoundType.Jump);
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

    private void SetLowestYPosition()
    {
        _lowestYPosition = _platformManager.GetFirstSpawnedPlatform().transform.position.y;
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

    private void HandlePlayerDeath()
    {
        SetLowestYPosition();
        if (transform.position.y <= _lowestYPosition)
        {
            StartCoroutine(OnPlayerDeathMethod());
        }
    }

    public IEnumerator OnPlayerDeathMethod()
    {
        _isPlayerDead = true;
        _inputManager.enabled = false;
        yield return new WaitForSecondsRealtime(_deathDelay);
        AudioManager.Instance.PlaySound(SoundType.GameOver);
        OnPlayerDeath?.Invoke();
    }
}
