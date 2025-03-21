using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIManager : MonoBehaviour
{
    [Inject] private StartManager _startManager;
    [Inject] private BoostTimer _boostTimer;
    [Inject] private GameManager _gameManager;

    [SerializeField] private int _score = 0;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private float _scoreMultiplier = 2.1f;

    [SerializeField] private TMP_Text _startText;
    [SerializeField] private float _flickerTime = 0.5f;
    private Coroutine _flickerCoroutine;

    [SerializeField] private Slider _slider;
    [SerializeField] private Image _sliderImg;

    private void OnEnable()
    {
        BoostEventManager.OnBoostTimerUI += SetTimerUIElement;
        _boostTimer.OnTimerFinish += HasBoostEnded;
    }

    private void OnDisable()
    {
        BoostEventManager.OnBoostTimerUI -= SetTimerUIElement;
        _boostTimer.OnTimerFinish -= HasBoostEnded;
    }

    void Start()
    {
        SetUIElementsAtStart();

        _startManager.OnGameStarted += HasGameStarted;
        _flickerCoroutine = StartCoroutine(StartTextFlickering());
    }

    void Update()
    {
        UpdateUIElements();
    }

    private void SetUIElementsAtStart()
    {
        _sliderImg.enabled = false;
    }

    private void UpdateUIElements()
    {
        _slider.value = _boostTimer.TimerDuration;
        _scoreText.text = "Score: " + ((int)_gameManager.PlayerScore).ToString();
    }

    public void SetTimerUIElement(float value, Sprite sprite)
    {
        _slider.maxValue = value;
        if (!_sliderImg.enabled)
        {
            _sliderImg.enabled = true;
        }
        _sliderImg.sprite = sprite;
    }

    private void HasBoostEnded(object sender, EventArgs e)
    {
        _sliderImg.enabled = false;
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