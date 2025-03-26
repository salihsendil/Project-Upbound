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

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
    }

    private void Move(InputAction.CallbackContext callback)
    {
        _touchInput = callback.ReadValue<Vector2>();
        _touchInput = Camera.main.ScreenToViewportPoint(_touchInput);
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
