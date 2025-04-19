using System;
using UnityEngine;

public class BoostTimer : MonoBehaviour
{
    public event Action<Sprite> OnTimerStarted;
    public event Action<float> OnTimerChanged;
    public event Action OnTimerFinish;
    [SerializeField] private float _timerDuration = 0;

    private void Update()
    {
        if (_timerDuration > 0)
        {
            OnTimerChanged?.Invoke(_timerDuration);
            ReduceTime(Time.deltaTime);
        }
    }

    public void SetTimer(float duration)
    {
        OnTimerFinish?.Invoke();
        _timerDuration = duration;
    }

    private void ReduceTime(float reduce)
    {
        _timerDuration -= reduce;

        if (_timerDuration <= 0)
        {
            _timerDuration = 0;
            OnTimerFinish?.Invoke();
        }
    }
}
