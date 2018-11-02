using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementScreen : MonoBehaviour
{

    [SerializeField]
    private Button backButton;

    private void Start()
    {
        backButton.onClick.AddListener(CloseScreen);
    }

    private void OnDestroy()
    {
        backButton.onClick.RemoveListener(CloseScreen);
    }

    private void CloseScreen()
    {
        ScreenManager.Instance().PopScreen();
    }
}
