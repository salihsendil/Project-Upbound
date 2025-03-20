using UnityEngine;
using Zenject;

public class JumpForceBoost : BaseBoost
{
    [Inject] private BoostTimer _boostTimer;
    [Inject] private GameManager _gameManager;
    [Inject] private UIManager _uiManager;
    private float _boostDuration = 3f;
    private float _boostedJumpForce = 10f;

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        _boostTimer.SetTimer(_boostDuration);
        _gameManager.BasePlatformJumpForce = _boostedJumpForce;
        _uiManager.SetTimerUIElement(_boostDuration);
        base.OnTriggerEnter2D(collider);
    }
}
