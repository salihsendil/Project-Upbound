using UnityEngine;

public class ScoreMultiplierBoost : BaseBoost
{
    public override float BoostDuration => 6f;
    private float _boostedScoreMultiplier = 142.4f;

    protected override void ApplyBoostEffect()
    {
        BoostEventManager.OnScoreMultiplierBoost?.Invoke(_boostedScoreMultiplier);
    }
}
