using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIManager : MonoBehaviour
{
    [Inject] private StartManager _startManager;
    [Inject] private GameManager _gameManager;
    [Inject] private PlayerController _playerController;

    [SerializeField] private TMP_Text _startText;
    [SerializeField] private float _flickerTime = 0.5f;
    private Coroutine _flickerCoroutine;

    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _gameOverMenu;

    private void Awake()
    {
        SetPanels();
    }

    void Start()
    {
        _startManager.OnGameStarted += HasGameStarted;
        _playerController.OnPlayerDeath += HasPlayerDeath;
        _flickerCoroutine = StartCoroutine(StartTextFlickering());
    }

    private void OnDisable()
    {
        _playerController.OnPlayerDeath -= HasPlayerDeath;
    }

    public void SetGamePauseSituation()
    {
        bool isPaused = Time.timeScale == 0;
        if (isPaused)
        {
            Time.timeScale = _gameManager.CurrentTimeScale;
            _playerController.enabled = true;
            _pauseMenu.SetActive(false);
        }

        else
        {
            Time.timeScale = 0;
            _playerController.enabled = false;
            _pauseMenu.SetActive(true);
        }
    }

    private void HasPlayerDeath()
    {
        _gameOverMenu.SetActive(true);
    }

    private void SetPanels()
    {
        _pauseMenu.SetActive(false);
        _gameOverMenu.SetActive(false);
    }

    private void HasGameStarted()
    {
        StopCoroutine(_flickerCoroutine);
        _startText.enabled = false;
    }

    IEnumerator StartTextFlickering()
    {
        while (true)
        {
            _startText.enabled = !_startText.enabled;
            if (_startText.enabled)
            {
                AudioManager.Instance.PlaySound(SoundType.StartGameBeep);
            }
            yield return new WaitForSecondsRealtime(_flickerTime);
        }
    }
}