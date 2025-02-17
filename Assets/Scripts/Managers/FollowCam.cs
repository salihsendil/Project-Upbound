using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private Transform _followTo;

    [Header("Offsets")]
    [SerializeField] private Vector3 _followOffset = new Vector3(0f, 2f, -10f);
    [SerializeField] private Vector3 _deadZoneTop = new Vector3(0f, 2.5f, 0f);
    [SerializeField] private Vector3 _deadZoneBottom = new Vector3(0f, 2.5f, 0f);
    [SerializeField] private float _followSpeed = 10f;
    [SerializeField] private float _bodyOffset = 0.6f;

    [SerializeField] private Vector3 _targetVector;

    void Start()
    {
        _targetVector = _followTo.position + _followOffset;
        _targetVector.z = transform.position.z;
        _targetVector.x = transform.position.x;
        transform.position = _targetVector;
    }

    void Update()
    {
        CheckPlayerOnSafeZone();
        UpdatePosition();

    }

    private void UpdatePosition()
    {
        transform.position = Vector3.Slerp(transform.position, _targetVector, Time.deltaTime * _followSpeed);
    }

    private void CheckPlayerOnSafeZone()
    {
        if (Mathf.Abs(_followTo.transform.position.y - transform.position.y + _deadZoneBottom.y) < 0.5f ||
            Mathf.Abs((transform.position.y + _deadZoneTop.y) - (_followTo.transform.position.y + _bodyOffset)) < 0.5f)
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
        Gizmos.DrawLine((_followTo.transform.position.y+_bodyOffset) * Vector3.up - transform.right, transform.right + Vector3.up * (_followTo.transform.position.y + _bodyOffset));
    }

}
