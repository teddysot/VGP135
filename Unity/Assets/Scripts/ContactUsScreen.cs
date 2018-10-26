using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContactUsScreen : MonoBehaviour
{
    [SerializeField]
    private Button backButton;

    [SerializeField]
    private Button facebookButton;

    private void Start()
    {
        backButton.onClick.AddListener(CloseScreen);
        facebookButton.onClick.AddListener(OpenFacebook);
    }

    private void OnDestroy()
    {
        backButton.onClick.RemoveListener(CloseScreen);
        facebookButton.onClick.RemoveListener(OpenFacebook);
    }

    private void CloseScreen()
    {
        ScreenManager.Instance().PopScreen();
    }

    private void OpenFacebook()
    {
        ScreenManager.Instance().PopScreen();
    }
}
