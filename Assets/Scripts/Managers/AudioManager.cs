using System.Collections.Generic;
using UnityEngine;

public enum SoundType{
    Jump,
    GameOver,
    PlayerDeathByEnemy,
    StartGameBeep,
    ButtonPressed,
    BoostCollected
}

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public struct SoundData
    {
        public SoundType soundType;
        public AudioClip clip;
    }

    [Header("Settings")]
    private AudioSource _audioSource;
 
    [Header("Audio Clips")]
    [SerializeField] private SoundData[] _sounds;
    private Dictionary<SoundType, AudioClip> _soundDict = new Dictionary<SoundType, AudioClip>();


    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        #region SingletonPattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        #endregion

        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = PlayerSettings.DEFAULT_SFX_LEVEL;
    }

    private void OnEnable()
    {
        OptionsHandler.OnSFXLevelChanged += SetVolume;
    }

    private void OnDisable()
    {
        OptionsHandler.OnSFXLevelChanged -= SetVolume;
    }

    private void Start()
    {
        FillDictionary();
    }

    private void FillDictionary()
    {
        foreach (var soundData in _sounds)
        {
            if (!_soundDict.ContainsKey(soundData.soundType))
            {
                _soundDict.Add(soundData.soundType, soundData.clip);
            }
        }
    }

    public void PlaySound(SoundType soundType)
    {
        if (_soundDict.TryGetValue(soundType, out AudioClip clip))
        {
            _audioSource.PlayOneShot(clip);
        }

        else
        {
            Debug.LogWarning($"AudioManager: {soundType} türünde ses bulunamadý!");
        }
    }

    private void SetVolume(float volume)
    {
        _audioSource.volume = volume;
    }

}
