using System.Collections.Generic;
using Zenject;
using UnityEngine;
using UnityEngine.UI;

public class BoostTimerUI : MonoBehaviour
{
    [Inject] private BoostTimer _boostTimer;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _sliderImage;

    [SerializeField] private List<Image> _boostImgList;

    private void Awake()
    {
        _sliderImage.enabled = false;
    }

    private void OnEnable()
    {
        BoostEventManager.OnBoostTimerUI += SetTimerUI;
        _boostTimer.OnTimerChanged += UpdateTimerSlider;
        _boostTimer.OnTimerFinish += HasBoostEnded;
    }

    private void OnDisable()
    {
        BoostEventManager.OnBoostTimerUI -= SetTimerUI;
        _boostTimer.OnTimerChanged -= UpdateTimerSlider;
        _boostTimer.OnTimerFinish -= HasBoostEnded;
    }

    private void UpdateTimerSlider(float value) => _slider.value = value;

    private void SetTimerUI(float value, Sprite sprite)
    {
        _slider.maxValue = _slider.value = value;
        if (!_sliderImage.enabled)
        {
            _sliderImage.enabled = true;
        }
        _sliderImage.sprite = sprite;
    }

    private void HasBoostEnded()
    {
        _sliderImage.enabled = false;
    }
}
