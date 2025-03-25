using System;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] private BoostTimer _boostTimer;
    [Inject] private PlayerController _player;

    public static float DefaultTimeScale = 2F;

    private const float DefaultJumpForce = 7.5f;
    private const float DefaultScoreMultiplier = 45.8f;

    [SerializeField] private float _playerScore = 0;
    [SerializeField] private float _platformJumpForce = DefaultJumpForce;
    [SerializeField] private float _scoreMultiplier = DefaultScoreMultiplier;

    public float BasePlatformJumpForce { get => _platformJumpForce; }
    public float ScoreMultiplier { get => _scoreMultiplier; }
    public float PlayerScore { get => _playerScore; }

    private void OnEnable()
    {
        BoostEventManager.OnJumpBoost += ApplyJumpBoost;
        BoostEventManager.OnScoreMultiplierBoost += ApplyScoreMultiplierBoost;
        BoostEventManager.OnTimeSpeedUpBoost += ApplyTimeSpeedUpBoost;
        _boostTimer.OnTimerFinish += ResetBoostedValues;
        _player.OnPlayerGoUp += AddScore;
    }

    private void OnDisable()
    {
        BoostEventManager.OnJumpBoost -= ApplyJumpBoost;
        BoostEventManager.OnScoreMultiplierBoost -= ApplyScoreMultiplierBoost;
        BoostEventManager.OnTimeSpeedUpBoost -= ApplyTimeSpeedUpBoost;
        _boostTimer.OnTimerFinish -= ResetBoostedValues;
        _player.OnPlayerGoUp -= AddScore;
    }

    public void AddScore(float value)
    {
        _playerScore += value * _scoreMultiplier;
    }

    private void ResetBoostedValues(object sender, EventArgs e)
    {
        _platformJumpForce = DefaultJumpForce;
        _scoreMultiplier = DefaultScoreMultiplier;
        Time.timeScale = DefaultTimeScale;
    }

    private void ApplyJumpBoost(float jumpForce)
    {
        _platformJumpForce = jumpForce;
    }

    private void ApplyScoreMultiplierBoost(float multiplier)
    {
        _scoreMultiplier = multiplier;
    }

    private void ApplyTimeSpeedUpBoost(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}
