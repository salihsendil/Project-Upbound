using UnityEngine;

public class BreakablePlatform : PlatformBase
{
    public override float jumpForce { get => 4f; }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("jump force overrided: " + jumpForce);
        base.OnCollisionEnter2D(collision);
        Destroy(gameObject);
    }

}
