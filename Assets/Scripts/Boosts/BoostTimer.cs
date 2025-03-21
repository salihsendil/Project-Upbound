using System;
using UnityEngine;

public class BoostTimer : MonoBehaviour
{
    public event EventHandler OnTimerFinish;
    [SerializeField] private float _timerDuration = 0;

    public float TimerDuration { get => _timerDuration; }

    private void Update()
    {
        if (_timerDuration > 0)
        {
            ReduceTime(Time.deltaTime);
        }
    }

    public void SetTimer(float duration)
    {
        OnTimerFinish?.Invoke(this, EventArgs.Empty);
        _timerDuration = duration;
    }

    private void ReduceTime(float reduce)
    {
        _timerDuration -= reduce;

        if (_timerDuration <= 0)
        {
            _timerDuration = 0;
            OnTimerFinish?.Invoke(this, EventArgs.Empty);
        }
    }
}
