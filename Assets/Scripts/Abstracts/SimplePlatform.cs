using UnityEngine;

public class SimplePlatform : PlatformBase
{

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        Debug.Log("jump force: " + jumpForce);
    }
}
