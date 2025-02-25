using UnityEngine;
using Cinemachine;

public class CameraLimit : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    private Transform playerTransform;
    private float minYPosition;  // Kamera için alt limit

    private void Start()
    {
        if (virtualCamera == null)
        {
            Debug.LogError("Cinemachine Virtual Camera atanmamýþ!");
            return;
        }

        playerTransform = virtualCamera.Follow;
        minYPosition = playerTransform.position.y;  // Oyunun baþýndaki Y konumu alt limit olarak alýnýyor.
    }

    private void LateUpdate()
    {
        if (playerTransform == null) return;

        float cameraY = virtualCamera.transform.position.y;
        float targetY = Mathf.Max(cameraY, playerTransform.position.y); // Kamera aþaðý gitmesin

        virtualCamera.transform.position = new Vector3(
            virtualCamera.transform.position.x,
            targetY,
            virtualCamera.transform.position.z
        );
    }
}
