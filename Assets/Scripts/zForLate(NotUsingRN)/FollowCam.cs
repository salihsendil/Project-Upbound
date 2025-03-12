using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private Transform _followTo;
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private Vector3 targetPosition = new Vector3(0f, 0f, -10f);

    private Vector3 _topLimit;
    private Vector3 _bottomLimit;

    private void Awake()
    {
        var player = FindFirstObjectByType<PlayerController>();
        if (player != null)
        {
            _followTo = player.transform;
        }
        else
        {
            Debug.LogError("PlayerController bulunamadý!");
        }
    }

    private void Start()
    {
        UpdateLimits();
        SetCameraPositionInstantly(); // Baþlangýçta anlýk konumlanmasý için
    }

    private void Update()
    {
        if (IsNotPlayerInSafeZone())
        {
            SetTargetPosition();
            UpdateLimits();
        }
        MoveCameraSmoothly();
    }

    private void UpdateLimits()
    {
        _topLimit = _followTo.position + new Vector3(0f, 3f, 0f);
        _bottomLimit = _followTo.position + new Vector3(0f, -2.5f, 0f);
    }

    private void SetTargetPosition()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, _followTo.position.y, -10f);
    }

    private void MoveCameraSmoothly()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    private void SetCameraPositionInstantly()
    {
        transform.position = new Vector3(transform.position.x, _followTo.position.y, -10f);
    }

    private bool IsNotPlayerInSafeZone()
    {
        return _followTo.position.y > _topLimit.y || _followTo.position.y < _bottomLimit.y;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_topLimit + Vector3.right, _topLimit - Vector3.right);
        Gizmos.DrawLine(_bottomLimit + Vector3.right, _bottomLimit - Vector3.right);
    }
}


/*
 
     [Header("Offsets")]
    [SerializeField] private Vector3 _followOffset = new Vector3(0f, 2f, -10f);
    
    [SerializeField] private Vector3 _deadZoneTopOffset = new Vector3(0f, 3.5f, 0f);
    [SerializeField] private Vector3 _deadZoneBottomOffset = new Vector3(0f, -2.5f, 0f);

    [SerializeField] private Vector3 _deadZoneTop;
    [SerializeField] private Vector3 _deadZoneBottom;

    [SerializeField] private float _followSpeed = 10f;
    [SerializeField] private float _bodyOffset = 0.6f;



    void Start()
    {
        _targetVector = _followTo.position + _followOffset;
        _targetVector.z = transform.position.z;
        _targetVector.x = transform.position.x;
        transform.position = _targetVector;
    }

    void Update()
    {
        SetSafeZoneLimits();
        CheckPlayerOnSafeZone();
        UpdatePosition();

        Debug.Log("alt: " + (_followTo.transform.position.y - _deadZoneBottom.y));
        Debug.Log("üst: " + (_deadZoneTop.y - _followTo.transform.position.y));

    }

    private void SetSafeZoneLimits()
    {
        _deadZoneBottom = _followTo.position + _deadZoneBottomOffset;
        _deadZoneTop = _followTo.position + _deadZoneTopOffset;

        //_deadZoneBottom = 
    }

    private void UpdatePosition()
    {
        transform.position = Vector3.Lerp(transform.position, _targetVector, Time.deltaTime * _followSpeed);
    }

    private void CheckPlayerOnSafeZone()
    {
        if (_followTo.transform.position.y - _deadZoneBottom.y < -0.5f ||
            _followTo.transform.position.y - _deadZoneTop.y > 0.5f)
        {
            _targetVector = _followTo.position + _followOffset;
            _targetVector.z = transform.position.z;
            _targetVector.x = transform.position.x;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 bottomLine = new Vector3(0f, transform.position.y - _deadZoneBottom.y, 0f);
        Vector3 topLine = new Vector3(0f, transform.position.y + _deadZoneTop.y, 0f);
        Gizmos.DrawLine(bottomLine - transform.right, transform.right + bottomLine);
        Gizmos.DrawLine(topLine - transform.right, transform.right + topLine);
        Gizmos.DrawLine(_followTo.transform.position.y * Vector3.up - transform.right, transform.right + Vector3.up * _followTo.transform.position.y);
        Gizmos.DrawLine((_followTo.transform.position.y + _bodyOffset) * Vector3.up - transform.right, transform.right + Vector3.up * (_followTo.transform.position.y + _bodyOffset));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(0f, _deadZoneBottom.y, 0f), Vector3.one * 0.5f);
        Gizmos.DrawWireCube(new Vector3(0f, _deadZoneTop.y, 0f), Vector3.one * 0.5f);
    }

 
 */
