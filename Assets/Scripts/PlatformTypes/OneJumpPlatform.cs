using UnityEngine;

public class OneJumpPlatform : BasePlatform
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        gameObject.SetActive(false);
    }
}
