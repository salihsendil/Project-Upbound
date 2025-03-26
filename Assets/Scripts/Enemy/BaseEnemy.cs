using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    private float _jumpForce = 10f;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController player = collider.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.IsPlayerDead = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.PlayerJump(_jumpForce);
            Destroy(gameObject);
        }
    }
}
