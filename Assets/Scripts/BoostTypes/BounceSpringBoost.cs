using UnityEngine;

public class BounceSpringBoost : MonoBehaviour
{
    private float _jumpForce = 15f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            collision.gameObject.GetComponent<PlayerController>().PlayerJump(_jumpForce);
        }
    }
}
