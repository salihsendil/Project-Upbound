using System;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private Rigidbody2D _rb;
    private CapsuleCollider2D _jumpCollider;
    [Inject] private InputManager _inputManager;

    [Header("Camera Variables")]
    private Camera _mainCam;
    private float _mainCamSize;
    private float _screenBound;

    [Header("Movement Variables")]
    private Vector3 _worldPos;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _maxSpeed = 10f;
    [SerializeField] private float _highestYPosition = 0f;
    [SerializeField] private bool _canJump = true;

    public float HighestYPosition { get => _highestYPosition; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _jumpCollider = GetComponent<CapsuleCollider2D>();
    }


    private void Start()
    {
        _mainCam = Camera.main;
        _mainCamSize = Camera.main.orthographicSize;
        _screenBound = (_mainCamSize / 2) - (transform.localScale.x / 2);
    }

    void Update()
    {
        //input girdisi lerp ile yumu�at�larak hem oyun zorla�t�r�ls�n hem farkl� bi tat kazand�r�ls�n
        //LimitTheLinearVelocity();
        ControllColliderForPlayerJump();

        if (IsPressedScreen())
        {
            MovePlayerOnX();
        }
        SetHighestYPosition();    
    }

    /// <summary>
    /// Karakterin h�z�n� s�n�rlar.
    /// </summary>
    private void LimitTheLinearVelocity()
    {
        _rb.linearVelocity = Vector2.ClampMagnitude(_rb.linearVelocity, _maxSpeed);
    }

    private void ControllColliderForPlayerJump()
    {
        _jumpCollider.enabled = _rb.linearVelocityY < Mathf.Epsilon;
    }

    /// <summary>
    /// Kullan�c�n�n ekrana dokunma durumunu geri d�nd�r�r.
    /// </summary>
    /// <returns>Ekrana dokunulduysa true, yoksa false d�nd�r�r.</returns>
    private bool IsPressedScreen()
    {
        return _inputManager.TouchInput != Vector2.zero;
    }

    private void MovePlayerOnX()
    {
        _worldPos = _mainCam.ScreenToWorldPoint(new Vector3(
                _inputManager.TouchInput.x,
                _inputManager.TouchInput.y,
                Camera.main.nearClipPlane));

        _worldPos.x = Mathf.Clamp(_worldPos.x, -_screenBound, _screenBound);

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, _worldPos.x, _speed * Time.deltaTime * _speed), transform.position.y, transform.position.z);
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
            _highestYPosition = transform.position.y;
        }
    }

}
