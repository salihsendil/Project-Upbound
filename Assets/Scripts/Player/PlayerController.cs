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

    public static PlayerController Instance { get; private set; }
    public float HighestYPosition { get => _highestYPosition; }

    private void Awake()
    {
        #region SingletonPattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        #endregion

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

        //input girdisi lerp ile yumuþatýlarak hem oyun zorlaþtýrýlsýn hem farklý bi tat kazandýrýlsýn
        LimitTheLinearVelocity();
        BlockMultipleJump();

        if (IsPressedScreen())
        {
            MovePlayerOnX();
        }
        SetHighestYPosition();
    }

    private void LimitTheLinearVelocity()
    {
        _rb.linearVelocity = Vector2.ClampMagnitude(_rb.linearVelocity, _maxSpeed);
    }

    private void BlockMultipleJump()
    {
        _jumpCollider.enabled = _rb.linearVelocity.y > 0.5f ? false : true;
    }

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

    public void PlayerJump(float jumpForce)
    {
        _rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
    }

    private void SetHighestYPosition()
    {
        if (transform.position.y > _highestYPosition)
        {
            _highestYPosition = transform.position.y;
        }
    }

}
