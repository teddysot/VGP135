using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsScreen : MonoBehaviour {

    public AudioMixer audioMixer;

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

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
