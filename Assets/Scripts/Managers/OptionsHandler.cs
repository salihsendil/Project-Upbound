using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsHandler : MonoBehaviour
{
    public static Action<float> OnMusicLevelChanged;
    public static Action<float> OnSFXLevelChanged;

    [SerializeField] private TMP_Text _musicText;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Image _musicImage;
    [SerializeField] private Material _musicOnMaterial;
    [SerializeField] private Material _musicOffMaterial;
    [SerializeField] private TMP_Text _sfxText;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Image _sfxImage;
    [SerializeField] private Material _sfxOnMaterial;
    [SerializeField] private Material _sfxOffMaterial;

    private void Start()
    {
        _musicSlider.value = PlayerPrefs.GetFloat("MusicLevel");
        _sfxSlider.value = PlayerPrefs.GetFloat("SFXLevel");
        UpdateAudioOption(_musicSlider, _musicText, _musicImage, _musicOnMaterial, _musicOffMaterial);
        UpdateAudioOption(_sfxSlider, _sfxText, _sfxImage, _sfxOnMaterial, _sfxOffMaterial);
    }

    public void SetMusicOptionLevel()
    {
        UpdateAudioOption(_musicSlider, _musicText, _musicImage, _musicOnMaterial, _musicOffMaterial);
        OnMusicLevelChanged?.Invoke(_musicSlider.value);
    }

    public void SetSfxOptionLevel()
    {
        UpdateAudioOption(_sfxSlider, _sfxText, _sfxImage, _sfxOnMaterial, _sfxOffMaterial);
        OnSFXLevelChanged?.Invoke(_sfxSlider.value);
    }

    private void UpdateAudioOption(Slider slider, TMP_Text text, Image image, Material onMat, Material offMat)
    {
        float value = slider.value;
        text.text = ((int)(value*100)).ToString();
        image.material = value <= 0 ? offMat : onMat;
    }
}
