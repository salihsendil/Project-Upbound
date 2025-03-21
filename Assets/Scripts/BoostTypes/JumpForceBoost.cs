using UnityEngine;

public class JumpForceBoost : BaseBoost
{
    public override float BoostDuration => 4f;
    private float _boostJumpForce = 12f;

    protected override void ApplyBoostEffect()
    {
        BoostEventManager.OnJumpBoost?.Invoke(_boostJumpForce);
    }
}
