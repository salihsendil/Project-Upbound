using UnityEngine;

public class DifficultyManager
{
    private const float _minDiffValue = 0.01f;
    private const float _maxDiffValue = 5f;
    private const float _diffScale = 100f;
    private float _difficultyFactor = 0.01f;

    /// <summary>
    /// Rastgele platform türü için Perlin Noise deðeri ayarlar.
    /// </summary>
    public float PlatformTypePerlinNoise(float time, float yPos)
    {
        _difficultyFactor = Mathf.Clamp(_difficultyFactor, _minDiffValue, _maxDiffValue);
        return Mathf.PerlinNoise(time, yPos) * _diffScale;
    }
}
