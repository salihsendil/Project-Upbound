using TMPro;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _hiScoreText;

    private void Awake()
    {
        SetHighScoreText();
    }

    private void SetHighScoreText()
    {
        _hiScoreText.text = "Hi-Score: " + ((int)PlayerPrefs.GetFloat("HighScore")).ToString();
    }
}
