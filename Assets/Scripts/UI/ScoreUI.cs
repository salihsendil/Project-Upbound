using TMPro;
using UnityEngine;
using Zenject;

public class ScoreUI : MonoBehaviour
{
    [Inject] private GameManager _gameManager;
    [SerializeField] private TMP_Text _scoreText;

    private void OnEnable()
    {
        _gameManager.OnScoreChanged += UpdateScoreText;
    }

    private void OnDisable()
    {
        _gameManager.OnScoreChanged -= UpdateScoreText;
    }

    public void UpdateScoreText(float score)
    {
        _scoreText.text = "Score: " + ((int)score).ToString();
    }
}
