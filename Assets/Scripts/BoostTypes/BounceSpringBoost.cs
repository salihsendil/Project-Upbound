using UnityEngine;

public class BounceSpringBoost : BaseBoost
{
    private float _jumpForce = 15f;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            collision.gameObject.GetComponent<PlayerController>().PlayerJump(_jumpForce);
        }
        base.OnCollisionEnter2D(collision);
    }
}
