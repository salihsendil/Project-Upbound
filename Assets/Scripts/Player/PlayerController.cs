using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private Rigidbody2D _rb;
    private BoxCollider2D _jumpCollider;

    [Header("Camera Variables")]
    private Camera _mainCam;
    private float _mainCamSize;
    private float _screenBound;

    [Header("Movement Variables")]
    private Vector3 _worldPos;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _maxSpeed = 10f;


    public static PlayerController Instance { get; private set; }

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
        _jumpCollider = GetComponent<BoxCollider2D>();

    }


    private void Start()
    {
        _mainCam = Camera.main;
        _mainCamSize = Camera.main.orthographicSize;
        _screenBound = (_mainCamSize / 2) - (transform.localScale.x / 2);
    }

    void Update()
    {

        LimitTheLinearVelocity();
        BlockMultipleJump();

        if (IsPressedScreen())
        {
            MovePlayerOnX();
        }
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
        return InputManager.Instance.TouchInput != Vector2.zero;
    }

    private void MovePlayerOnX()
    {
        _worldPos = _mainCam.ScreenToWorldPoint(new Vector3(
                InputManager.Instance.TouchInput.x,
                InputManager.Instance.TouchInput.y,
                Camera.main.nearClipPlane));

        _worldPos.x = Mathf.Clamp(_worldPos.x, -_screenBound, _screenBound);

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, _worldPos.x, _speed * Time.deltaTime * _speed), transform.position.y, transform.position.z);
    }

}
