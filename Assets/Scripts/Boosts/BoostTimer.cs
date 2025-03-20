using System;
using UnityEngine;

public class BoostTimer : MonoBehaviour
{
    public event EventHandler OnTimerFinish;
    [SerializeField] private float _timerDuration = 0;
    [SerializeField] private bool _hasTimerFinished = true;

    public float TimerDuration { get => _timerDuration; }
    public bool HasTimerFinished { get => _hasTimerFinished; }

    private void Update()
    {
        if (_hasTimerFinished != true)
        {
            ReduceTime(Time.deltaTime);
        }
    }

    public void SetTimer(float duration)
    {
        OnTimerFinish?.Invoke(this, EventArgs.Empty);
        _timerDuration = duration;
        _hasTimerFinished = false;
    }

    private void ReduceTime(float reduce)
    {
        if (_timerDuration >= 0)
        {
            _timerDuration -= reduce;
        }
        else
        {
            _timerDuration = 0;
            _hasTimerFinished = true;
            OnTimerFinish?.Invoke(this, EventArgs.Empty);
        }
    }
}
