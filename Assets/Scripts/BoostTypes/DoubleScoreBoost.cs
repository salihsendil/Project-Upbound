using UnityEngine;
using Zenject;

public class DoubleScoreBoost : BaseBoost
{
    [Inject] private BoostTimer _boostTimer;
    [Inject] private GameManager _gameManager;
    [Inject] private UIManager _uiManager;
    private float _boostDuration = 3f;
    private float _boostedScoreMultiplier = 4.7f;

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        _boostTimer.SetTimer(_boostDuration);
        _gameManager.ScoreMultiplier = _boostedScoreMultiplier;
        _uiManager.SetTimerUIElement(_boostDuration);
        base.OnTriggerEnter2D(collider);
    }
}
