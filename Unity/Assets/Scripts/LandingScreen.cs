using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandingScreen : MonoBehaviour
{
    [SerializeField]
    private Button startGameButton;

    [SerializeField]
    private Button settingGameButton;

    [SerializeField]
    private Button contactGameButton;

    [SerializeField]
    private Button achievementGameButton;

    [SerializeField]
    private Button inventoryGameButton;

    private void Start()
    {
        startGameButton.onClick.AddListener(StartButtonClickedCallback);
        settingGameButton.onClick.AddListener(OpenSettingsScreen);
        contactGameButton.onClick.AddListener(OpenContactScreen);
        achievementGameButton.onClick.AddListener(OpenAchievementScreen);
        inventoryGameButton.onClick.AddListener(OpenInventoryScreen);
    }

    private void OnDestroy()
    {
        startGameButton.onClick.RemoveListener(StartButtonClickedCallback);
        settingGameButton.onClick.RemoveListener(OpenSettingsScreen);
        contactGameButton.onClick.RemoveListener(OpenContactScreen);
        achievementGameButton.onClick.RemoveListener(OpenAchievementScreen);
        inventoryGameButton.onClick.RemoveListener(OpenInventoryScreen);
    }

    private void StartButtonClickedCallback()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    private void OpenSettingsScreen()
    {
        ScreenManager.Instance().PushScreen("SettingsScreen");
    }

    private void OpenContactScreen()
    {
        ScreenManager.Instance().PushScreen("ContactUsScreen");
    }

    private void OpenAchievementScreen()
    {
        ScreenManager.Instance().PushScreen("AchievementScreen");
    }

    private void OpenInventoryScreen()
    {
        ScreenManager.Instance().PushScreen("InventoryScreen");
    }
}
