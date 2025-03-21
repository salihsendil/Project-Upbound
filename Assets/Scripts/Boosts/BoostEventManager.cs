using System;
using UnityEngine;

public class BoostEventManager : MonoBehaviour
{
    public static Action<float> OnJumpBoost;
    public static Action<float> OnScoreMultiplierBoost;
    public static Action<float, Sprite> OnBoostTimerUI;
}
