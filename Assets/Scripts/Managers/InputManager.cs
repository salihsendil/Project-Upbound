using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("References")]
    private PlayerInput _playerInput;

    [Header("Movement Input")]
    private Vector2 _touchInput;

    [Header("Getters - Setters")]
    public Vector2 TouchInput { get => _touchInput; }
    public static InputManager Instance { get; private set; }

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

        _playerInput = new PlayerInput();
        _playerInput.Enable();
    }


    private void Move(InputAction.CallbackContext callback)
    {
        _touchInput = callback.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        _playerInput.Player.Movement.started += Move;
        _playerInput.Player.Movement.performed += Move;
        _playerInput.Player.Movement.canceled += Move;
    }

    private void OnDisable()
    {
        _playerInput.Player.Movement.started -= Move;
        _playerInput.Player.Movement.performed -= Move;
        _playerInput.Player.Movement.canceled -= Move;
    }
}
