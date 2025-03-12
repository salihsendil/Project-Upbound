using UnityEngine;

public class BreakablePlatform : BasePlatform
{
    public override float platformMarginMin { get => 0.2f; }
    public override float platformMarginMax { get => 0.3f; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }
}
