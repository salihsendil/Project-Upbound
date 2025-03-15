using UnityEngine;

public class BoostTimer : MonoBehaviour
{
    private float _timerDuration = 0;
    private bool _hasTimerFinished = true;

    private void Update()
    {
        if (_hasTimerFinished != true)
        {
            ReduceTime(Time.deltaTime);
        }
    }

    public void SetTimer(float duration)
    {
        _timerDuration = duration;
        _hasTimerFinished = true;
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
        }
    }
}
