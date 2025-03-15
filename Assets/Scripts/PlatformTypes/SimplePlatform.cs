using UnityEngine;

public class SimplePlatform : BasePlatform
{
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(base.jumpForce);
        base.OnCollisionEnter2D(collision);
    }
}
