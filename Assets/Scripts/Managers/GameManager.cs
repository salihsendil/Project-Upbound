using System;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    public event Action<float> OnScoreChanged;

    [Inject] private BoostTimer _boostTimer;
    [Inject] private PlayerController _player;
    [Inject] private InputManager _inputManager;

    public static float DefaultTimeScale = 2F;
    private float _currentTimeScale = DefaultTimeScale;

    private const float DefaultJumpForce = 7.5f;
    private const float DefaultScoreMultiplier = 45.8f;

    [SerializeField] private float _playerScore = 0;
    [SerializeField] private float _platformJumpForce = DefaultJumpForce;
    [SerializeField] private float _scoreMultiplier = DefaultScoreMultiplier;

    public float PlayerScore { get => _playerScore; }
    public float BasePlatformJumpForce { get => _platformJumpForce; }
    public float CurrentTimeScale { get => _currentTimeScale; }

    private void OnEnable()
    {
        BoostEventManager.OnJumpBoost += ApplyJumpBoost;
        BoostEventManager.OnScoreMultiplierBoost += ApplyScoreMultiplierBoost;
        BoostEventManager.OnTimeSpeedUpBoost += ApplyTimeSpeedUpBoost;
        _boostTimer.OnTimerFinish += ResetBoostedValues;
        _player.OnPlayerGoUp += AddScore;
        _player.OnPlayerDeath += HasPlayerDeath;
    }

    private void OnDisable()
    {
        BoostEventManager.OnJumpBoost -= ApplyJumpBoost;
        BoostEventManager.OnScoreMultiplierBoost -= ApplyScoreMultiplierBoost;
        BoostEventManager.OnTimeSpeedUpBoost -= ApplyTimeSpeedUpBoost;
        _boostTimer.OnTimerFinish -= ResetBoostedValues;
        _player.OnPlayerGoUp -= AddScore;
        _player.OnPlayerDeath -= HasPlayerDeath;
    }

    private void ResetBoostedValues()
    {
        _platformJumpForce = DefaultJumpForce;
        _scoreMultiplier = DefaultScoreMultiplier;
        Time.timeScale = _currentTimeScale = DefaultTimeScale;
    }

    public void AddScore(float value)
    {
        _playerScore += value * _scoreMultiplier;
        CheckNewHighScore();
        OnScoreChanged?.Invoke(_playerScore);
    }

    private void CheckNewHighScore()
    {
        if (_playerScore >= PlayerPrefs.GetFloat("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", _playerScore);
        }
    }

    private void HasPlayerDeath()
    {
        _inputManager.enabled = false;
        Time.timeScale = 0;
    }

    private void ApplyJumpBoost(float jumpForce) => _platformJumpForce = jumpForce;

    private void ApplyScoreMultiplierBoost(float multiplier) => _scoreMultiplier = multiplier;

    private void ApplyTimeSpeedUpBoost(float timeScale) => Time.timeScale = _currentTimeScale = timeScale;
}
