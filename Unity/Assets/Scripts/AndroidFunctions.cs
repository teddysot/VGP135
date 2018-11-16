using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    public void ShowToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        unityActivity.Call("runOnUiThread",new AndroidJavaRunnable(()=> {
            Debug.Log("Running on UI thread");

            AndroidJavaObject applicationContext = unityActivity.Call<AndroidJavaObject>("getApplicationContext");

            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");

            AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String",message);

            AndroidJavaObject toastInstance = toastClass.CallStatic<AndroidJavaObject>("makeText",applicationContext,message,toastClass.GetStatic<int>("LENGTH_SHORT"));

            toastInstance.Call("show");
        }));
    }

    public void ShowStaticHelloWorldLog()
    {
        // Get java class from my plugin
        AndroidJavaClass androidLibraryUtility = 
            new AndroidJavaClass("com.nluu.utilitybridge.UtilityBridgeMain");

        // Call static function
        androidLibraryUtility.CallStatic("HelloWorld");
    }
	
		
}