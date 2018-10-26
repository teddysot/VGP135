using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandingScreen : MonoBehaviour
{
    [SerializeField]
    private Button startGameButton;

    private void Start()
    {
        startGameButton.onClick.AddListener(StartButtonClickedCallback);
    }

    private void OnDestroy()
    {
        startGameButton.onClick.RemoveListener(StartButtonClickedCallback);
    }

    private void StartButtonClickedCallback()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void OpenSettingsScreen()
    {
        ScreenManager.Instance().PushScreen("SettingsScreen");
    }
}
