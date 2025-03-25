using UnityEngine;
using Zenject;

public abstract class BaseBoost : MonoBehaviour
{
    [Inject] protected BoostTimer _boostTimer;

    [SerializeField] public virtual float BoostDuration { get; private set; }
    [SerializeField] public SpriteRenderer SpriteRenderer;

    protected virtual void ApplyBoostEffect() { }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        _boostTimer.SetTimer(BoostDuration);
        BoostEventManager.OnBoostTimerUI?.Invoke(BoostDuration, SpriteRenderer.sprite);
        ApplyBoostEffect();
        Destroy(gameObject);
    }
}
