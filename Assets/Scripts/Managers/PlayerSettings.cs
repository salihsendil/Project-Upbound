using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public static float DEFAULT_MUSIC_LEVEL = 0.1f;
    public static float DEFAULT_SFX_LEVEL = 0.15f;

    private float _musicLevel = DEFAULT_MUSIC_LEVEL;
    private float _sfxLevel = DEFAULT_SFX_LEVEL;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("MusicLevel"))
        {
            PlayerPrefs.SetFloat("MusicLevel", _musicLevel);
        }
        else
        {
            _musicLevel = PlayerPrefs.GetFloat("MusicLevel");
        }

        if (!PlayerPrefs.HasKey("SFXLevel"))
        {
            PlayerPrefs.SetFloat("SFXLevel", _sfxLevel);
        }
        else
        {
            _sfxLevel = PlayerPrefs.GetFloat("SFXLevel");
        }
    }


    private void OnEnable()
    {
        OptionsHandler.OnMusicLevelChanged += SetMusicLevel;
        OptionsHandler.OnSFXLevelChanged += SetSFXLevel;
    }

    private void OnDisable()
    {
        OptionsHandler.OnMusicLevelChanged -= SetMusicLevel;
        OptionsHandler.OnSFXLevelChanged = SetSFXLevel;
    }

    public void SetMusicLevel(float volume)
    {
        _musicLevel = volume;
        PlayerPrefs.SetFloat("MusicLevel", volume);
    }

    public void SetSFXLevel(float volume)
    {
        _sfxLevel = volume;
        PlayerPrefs.SetFloat("SFXLevel", volume);
    }

    public void SetHighScore(float highScore)
    {
        PlayerPrefs.SetFloat("HighScore", highScore);
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetFloat("HighScore", 0);
    }
}
