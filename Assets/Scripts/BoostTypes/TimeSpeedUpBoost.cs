using UnityEngine;

public class TimeSpeedUpBoost : BaseBoost
{
    public override float BoostDuration => 12f;
    private const float _boostTimeScale = 3F;

    protected override void ApplyBoostEffect()
    {
        BoostEventManager.OnTimeSpeedUpBoost?.Invoke(_boostTimeScale);
    }
}
