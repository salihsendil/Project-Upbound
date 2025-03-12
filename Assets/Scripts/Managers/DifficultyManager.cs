using UnityEngine;

public class DifficultyManager
{
    private const float _minDiffTypeValue = 0.01f;
    private const float _maxDiffTypeValue = 5f;
    private const float _minDiffOffsetValue = 0.01f;
    private const float _maxDiffOffsetValue = 0.5f;
    private const float _timeScale = 0.01f;
    private const float _diffScale = 100f;
    private float _difficultyFactor = 0.01f;

    /// <summary>
    /// Rastgele platform t�r� i�in Perlin Noise de�eri ayarlar.
    /// </summary>
    public float PlatformTypePerlinNoise(float time, float yPos)
    {
        _difficultyFactor = Mathf.Clamp(_difficultyFactor, _minDiffTypeValue, _maxDiffTypeValue);
        return Mathf.PerlinNoise(time, yPos) * _diffScale;
    }

    /// <summary>
    /// Zaman ilerledik�e platformlar aras�ndaki mesafeyi art�ran de�eri d�nd�r�r.
    /// </summary>
    /// <returns>Ek offset de�eri d�nd�r�r.</returns>
    public float PlatformOffsetIncreaser()
    {
        //FIX IT AFTER
        return Mathf.Lerp(_minDiffOffsetValue, _maxDiffOffsetValue, Time.time * _timeScale);
    }

}
