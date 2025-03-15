using UnityEngine;
using Zenject;

public class JumpForceBoost : BaseBoost
{
    [Inject] private BoostTimer _boostTimer;
    [Inject] private GameManager _gameManager;
    private float _boostDuration = 3f;
    private float _boostedJumpForce = 10f;

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        //_gameManager.BasePlatformJumpForce = _boostedJumpForce;
        //_boostTimer.SetTimer(_boostDuration);
        //_gameManager.TestDebug();
        base.OnTriggerEnter2D(collider);
    }
}
