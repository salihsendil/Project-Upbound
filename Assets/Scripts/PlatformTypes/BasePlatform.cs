using UnityEngine;
using Zenject;

public abstract class BasePlatform : MonoBehaviour
{
    [Inject] private GameManager _gameManager;
    protected virtual float jumpForce { get => _gameManager.BasePlatformJumpForce; set => _gameManager.BasePlatformJumpForce = value; }
    public virtual float platformMarginMin { get; } = 0.4f;
    public virtual float platformMarginMax { get; } = 1.2f;

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            Debug.Log("jump force base platform class: " + jumpForce);
            player.PlayerJump(jumpForce);
        }
    }

    public float GenerateRandomYPosition()
    {
        return Random.Range(platformMarginMin, platformMarginMax);
    }
}
