using System;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    private bool _isGameStarted = false;
    public event Action OnGameStarted;

    private void Start()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        if (!_isGameStarted && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))/*DELETE MOUSE CONTROLL, DONT NEEDED*/
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        _isGameStarted = true;
        Time.timeScale = GameManager.DefaultTimeScale;
        OnGameStarted?.Invoke();
    }

}
