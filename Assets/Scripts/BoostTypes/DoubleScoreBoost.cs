using UnityEngine;
using Zenject;

public class DoubleScoreBoost : BaseBoost
{
    [Inject] private BoostTimer _boostTimer;
    [Inject] private GameManager _gameManager;
    private float _boostDuration = 3f;
    private float _boostedScoreMultiplier = 4.7f;

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        _gameManager.ScoreMultiplier = _boostedScoreMultiplier;
        _boostTimer.SetTimer(_boostDuration);
        base.OnTriggerEnter2D(collider);
    }

}
