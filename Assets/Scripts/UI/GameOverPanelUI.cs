using TMPro;
using UnityEngine;
using Zenject;

public class GameOverPanelUI : MonoBehaviour
{
    [Inject] private PlayerController _playerController;
    [Inject] private GameManager _gameManager;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _highScoreText;


    private void OnEnable()
    {
        SetGameOverPanel();
        _playerController.enabled = false;
        Time.timeScale = 0;
    }

    private void SetGameOverPanel()
    {
        _scoreText.text = "Score: " + (int)_gameManager.PlayerScore;
        _highScoreText.text = "Hi-Score: " + (int)PlayerPrefs.GetFloat("HighScore");
    }

}
