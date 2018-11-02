using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidFunctions : MonoBehaviour {

	// Use this for initialization
	public void ShowToastMessage(string message)
    {
        // Grab Unity Class
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        // Grab the Unity activity from the class via static function
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        // Call Android system popup or ui thread
        unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(()=>
        {
            Debug.Log("Running on UI thread");
            // Get application context for activity
            AndroidJavaObject applicationContext = unityActivity.Call<AndroidJavaObject>("getApplicationContext");

            // Instantiate a Toast object using Android's api
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");

            // Conver our message string as a java string
            AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", message);

            //Debug.Log("Toast : short length = " + toastClass.CallStatic<int>())

            // Create a toast object instance using the 'makeText' static function from 'android.widget.Toast'
            AndroidJavaObject toastInstance = toastClass.CallStatic<AndroidJavaObject>(
                "makeText",
                applicationContext,
                javaString,
                toastClass.GetStatic<int>("LENGTH_SHORT"));

            // Show the toast
            toastInstance.Call("show");
        }));
    }

    public void ShowStaticHelloWorldLog()
    {
        // Get java class from my plugin
        AndroidJavaClass androidLibraryUtility =
            new AndroidJavaClass("com.saharat.utilitybridge.UtilityBridgeMain");

        // Call static function
        androidLibraryUtility.CallStatic("HelloWorld");
    }
}
