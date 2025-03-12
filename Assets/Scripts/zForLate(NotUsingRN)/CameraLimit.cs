using UnityEngine;
using Cinemachine;

public class CameraLimit : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    private Transform playerTransform;
    private float minYPosition;  // Kamera i�in alt limit

    private void Start()
    {
        if (virtualCamera == null)
        {
            Debug.LogError("Cinemachine Virtual Camera atanmam��!");
            return;
        }

        playerTransform = virtualCamera.Follow;
        minYPosition = playerTransform.position.y;  // Oyunun ba��ndaki Y konumu alt limit olarak al�n�yor.
    }

    private void LateUpdate()
    {
        if (playerTransform == null) return;

        float cameraY = virtualCamera.transform.position.y;
        float targetY = Mathf.Max(cameraY, playerTransform.position.y); // Kamera a�a�� gitmesin

        virtualCamera.transform.position = new Vector3(
            virtualCamera.transform.position.x,
            targetY,
            virtualCamera.transform.position.z
        );
    }
}
