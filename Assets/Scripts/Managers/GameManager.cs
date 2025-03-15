using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _playerScore = 0;
    [SerializeField] private float _basePlatformJumpForce = 7.5f;
    [SerializeField] private float _scoreMultiplier = 2.1f;

    public float BasePlatformJumpForce { get => _basePlatformJumpForce; set => _basePlatformJumpForce = value; }
    public float ScoreMultiplier { get => _scoreMultiplier; set => _scoreMultiplier = value; }
    public float PlayerScore { get => _playerScore; set => _playerScore = value; }

    public void AddScore(float score)
    {
        _playerScore += score;
    }

    public void TestDebug()
    {
        Debug.Log("I am the game manager. I connected");
    }
}
