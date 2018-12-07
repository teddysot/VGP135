using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DJASKLJDAKLS.MyNameSpace1;

namespace DJASKLJDAKLS.MyNameSpace1
{
    public class Foo
    {
        public int test;
    }
}

namespace MyNameSpace2
{
    public class Foo
    {
        public float test;
    }
}

public class AndroidFunctions : MonoBehaviour {

    private void Test()
    {
        Foo instance = new Foo();
        MyNameSpace2.Foo otherInstance = new MyNameSpace2.Foo();

        //otherInstance.test;
    }

    bool enabledNotification = true;

    public void Start()
    {
#if UNITY_EDITOR
        Debug.Log("Start");
#elif UNITY_ANDROID
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject applicationContext = unityActivity.Call<AndroidJavaObject>("getApplicationContext");

        AndroidJavaClass androidLibraryUtility = new AndroidJavaClass("com.nluu.tools.Utility");

        AndroidJavaObject utilityPlugin =
            androidLibraryUtility.CallStatic<AndroidJavaObject>("Create", unityActivity, applicationContext);

        utilityPlugin.CallStatic("ShowToastMessage", "My Toast");

        enabledNotification = PlayerPrefs.GetInt("enabledNotification") == 1;
#endif
    }
    public void isNotification(bool n)
    {
        enabledNotification = n;
#if UNITY_EDITOR
        Debug.Log("Enable Notification " + n);
#elif UNITY_ANDROID
        PlayerPrefs.SetInt("enabledNotification", enabledNotification ? 1:0);
        PlayerPrefs.Save();
#endif
    }

    public void ShowStaticHelloWorldLog()
    {
        // Get java class from my plugin
        AndroidJavaClass androidLibraryUtility = 
            new AndroidJavaClass("com.nluu.tools.Utility");

        // Call static function
        androidLibraryUtility.CallStatic("HelloWorldStatic");
    }

    public void ShowNotification()
    {
        if (enabledNotification == false)
        {
            return;
        }

#if UNITY_EDITOR
        Debug.Log("ShowNotification");
#elif UNITY_ANDROID
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject applicationContext = unityActivity.Call<AndroidJavaObject>("getApplicationContext");

        AndroidJavaClass androidLibraryUtility = new AndroidJavaClass("com.nluu.tools.Utility");

        AndroidJavaObject utilityPlugin =
            androidLibraryUtility.CallStatic<AndroidJavaObject>("Create", unityActivity, applicationContext);

       
        // Call static function
        utilityPlugin.Call("ShowNotification","Message", 0);
#endif
    }

    public void ShowDelayedNotification()
    {
        if (enabledNotification == false)
        {
            return;
        }

#if UNITY_EDITOR
        Debug.Log("ShowDelayedNotification");
#elif UNITY_ANDROID
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject applicationContext = unityActivity.Call<AndroidJavaObject>("getApplicationContext");

        AndroidJavaClass androidLibraryUtility = new AndroidJavaClass("com.nluu.tools.Utility");

        AndroidJavaObject utilityPlugin =
            androidLibraryUtility.CallStatic<AndroidJavaObject>("Create", unityActivity, applicationContext);

        // Call static function
        utilityPlugin.Call("ShowNotification", "Message", 1000);
#endif
    }

    public void ShowToastMessage(string message)
    {
        if (enabledNotification == false)
        {
            return;
        }
#if UNITY_EDITOR
        Debug.Log("ShowToasyMessage");
#elif UNITY_ANDROID
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
            Debug.Log("Running on UI thread");

            AndroidJavaObject applicationContext = unityActivity.Call<AndroidJavaObject>("getApplicationContext");

            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");

            AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", message);

            AndroidJavaObject toastInstance = toastClass.CallStatic<AndroidJavaObject>("makeText", applicationContext, message, toastClass.GetStatic<int>("LENGTH_SHORT"));

            toastInstance.Call("show");
        }));
#endif
    }
}