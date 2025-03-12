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
    /// Rastgele platform türü için Perlin Noise deðeri ayarlar.
    /// </summary>
    public float PlatformTypePerlinNoise(float time, float yPos)
    {
        _difficultyFactor = Mathf.Clamp(_difficultyFactor, _minDiffTypeValue, _maxDiffTypeValue);
        return Mathf.PerlinNoise(time, yPos) * _diffScale;
    }

    /// <summary>
    /// Zaman ilerledikçe platformlar arasýndaki mesafeyi artýran deðeri döndürür.
    /// </summary>
    /// <returns>Ek offset deðeri döndürür.</returns>
    public float PlatformOffsetIncreaser()
    {
        //FIX IT AFTER
        return Mathf.Lerp(_minDiffOffsetValue, _maxDiffOffsetValue, Time.time * _timeScale);
    }

}
