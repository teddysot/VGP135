using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScreen : MonoBehaviour {
    public void CloseScreen()
    {
        ScreenManager.Instance().PopScreen();
    }
}
