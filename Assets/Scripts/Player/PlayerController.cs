using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Camera Variables")]
    private Camera _mainCam;
    private float _mainCamSize;
    private float _screenBound;

    [Header("Movement Variables")]
    private Vector3 _worldPos;
    [SerializeField] private float _speed = 10f;


    private void Start()
    {
        _mainCam = Camera.main;
        _mainCamSize = Camera.main.orthographicSize;
        _screenBound = (_mainCamSize / 2) - (transform.localScale.x / 2);
        /*
         Debug.Log($"size {_mainCamSize}");
        Debug.Log($"width {Screen.width}");
        Debug.Log($"calculated {(_mainCamSize / 2) - transform.localScale.x / 2}");
        */
    }

    void Update()
    {
        if (IsPressedScreen())
        {
            MovePlayerOnX();
        }
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

        Debug.Log(_worldPos.x);
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, _worldPos.x, _speed * Time.deltaTime * _speed), transform.position.y, transform.position.z);
    }

}
