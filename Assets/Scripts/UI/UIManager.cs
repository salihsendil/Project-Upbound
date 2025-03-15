using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIManager : MonoBehaviour
{
    [Inject] private StartManager _startManager;
    [Inject] private PlayerController _player;

    [SerializeField] private int _score = 0;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private float _scoreMultiplier = 2.1f;

    [SerializeField] private TMP_Text _startText;
    [SerializeField] private float _flickerTime = 0.5f;
    private Coroutine _flickerCoroutine;

    [SerializeField] private Slider _slider;

    void Start()
    {
        _startManager.OnGameStarted += HasGameStarted;
        _flickerCoroutine = StartCoroutine(StartTextFlickering());
    }

    void Update()
    {
        UpdateScore();
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        _scoreText.text = "Score: " + _score.ToString();
    }

    private void UpdateScore()
    {
        _score = (int)(_player.HighestYPosition * _scoreMultiplier);
    }

    private void HasGameStarted(object sender, EventArgs e)
    {
        StopCoroutine(_flickerCoroutine);
        _startText.enabled = false;
    }

    IEnumerator StartTextFlickering()
    {
        while (true)
        {
            _startText.enabled = !_startText.enabled;
            yield return new WaitForSecondsRealtime(_flickerTime);
        }
    }

}