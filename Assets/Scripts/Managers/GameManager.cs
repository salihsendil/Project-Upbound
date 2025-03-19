using System;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] private BoostTimer _boostTimer;

    [SerializeField] private float _playerScore = 0;
    [SerializeField] private float _basePlatformJumpForce = 7.5f;
    [SerializeField] private float _scoreMultiplier = 2.1f;

    public float BasePlatformJumpForce { get => _basePlatformJumpForce; set => _basePlatformJumpForce = value; }
    public float ScoreMultiplier { get => _scoreMultiplier; set => _scoreMultiplier = value; }
    public float PlayerScore { get => _playerScore; set => _playerScore = value; }

    private void Start()
    {
        _boostTimer.OnTimerFinish += ResetBoostedValues;
    }

    public void AddScore(float score)
    {
        _playerScore += score;
    }

    private void ResetBoostedValues(object sender, EventArgs e)
    {
        _basePlatformJumpForce = 7.5f;
        _scoreMultiplier = 2.1f;
    }

}
