using UnityEngine;

public class BreakablePlatform : BasePlatform
{
    public override float platformMarginMax { get => 0.6f; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }
}
