package com.nluu.tools;

import android.app.Activity;
import android.app.AlarmManager;
import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Build;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.util.Log;
import android.widget.Toast;

import static android.app.Notification.DEFAULT_LIGHTS;
import static android.app.Notification.DEFAULT_SOUND;
import static android.app.Notification.DEFAULT_VIBRATE;

public class Utility extends BroadcastReceiver {
    public Utility(){

    }

    // static reference to object- there should only be one
    private static Utility instance = null;

    public static Utility Create(Activity activity, Context applicationContext) {
        instance = new Utility();
        instance.applicationContext = applicationContext;
        instance.activity = activity;
        return instance;
    }

    private Context applicationContext = null;
    private Activity activity = null;

    public static void HelloWorldStatic(){
        Log.d("Unity", "Hello World");
    }

    public void ShowToastMessage( String message )
    {
        Toast.makeText(applicationContext, message, Toast.LENGTH_SHORT).show();
    }

    public void ShowNotification(String message, int delayInMilliseconds) {
        Log.d("Unity","Show notifications");
        NotificationManager notificationManager =
                (NotificationManager)applicationContext.getSystemService(Context.NOTIFICATION_SERVICE);

        Notification.Builder notificationBuilder = new Notification.Builder(applicationContext);

        // The resources database provided by the application context
        Resources resources = applicationContext.getResources();

        // Convert our icon to a bitmap for the newer Android devices
        Bitmap largeIcon = BitmapFactory.decodeResource(resources, R.drawable.ic_stat_name);

        notificationBuilder
                .setContentTitle("Title")
                .setContentText(message)
                .setSmallIcon(R.drawable.ic_stat_name)
                .setLargeIcon(largeIcon)
                .setDefaults(DEFAULT_SOUND | DEFAULT_VIBRATE | DEFAULT_LIGHTS)
                .setVibrate(new long[]{0, 1000});

        // Give the notification data to notification manager
        if( delayInMilliseconds == 0 ) {
            Log.d("Unity","Show instant notifications");
            notificationManager.notify((int)System.currentTimeMillis(), notificationBuilder.build());
        }
        else
        {
            Log.d("Unity","Show delayed notifications");
            AlarmManager alarmManager =
                    (AlarmManager)applicationContext.getSystemService(Context.ALARM_SERVICE);

            // Build intent data object to be used in the future
            Intent intent = new Intent(applicationContext, Utility.class);
            // Store the notification as data
            intent.putExtra("notificationData", notificationBuilder.build());

            // Bind the Intent to a Pending Intent to be used for future use
            PendingIntent pendingIntent =
                    PendingIntent.getBroadcast(applicationContext, 0, intent, PendingIntent.FLAG_UPDATE_CURRENT);

            // Tell AlarmManager to raise this intent after milliseconds
            if(Build.VERSION.SDK_INT >= Build.VERSION_CODES.KITKAT)
            {
                alarmManager.setExact(AlarmManager.RTC_WAKEUP, delayInMilliseconds, pendingIntent);
            }
            else
            {
                alarmManager.set(AlarmManager.RTC_WAKEUP, delayInMilliseconds, pendingIntent);
            }
        }
    }

    @Override
    public void onReceive(Context context, Intent intent) {
        Log.d("Unity", "Broadcast Receiver for Utility was given an intent");

        NotificationManager notificationManager =
                (NotificationManager)context.getSystemService(Context.NOTIFICATION_SERVICE);

        Notification notification = intent.getParcelableExtra("notificationData");
        notificationManager.notify((int)System.currentTimeMillis(), notification);
    }

    public int HasPermission(String permission) {
        return ContextCompat.checkSelfPermission(applicationContext, permission);
    }

    public void RequestPermission (String permission) {
        if( HasPermission(permission) != PackageManager.PERMISSION_GRANTED ) {
            // We do not have access to this permission, request it now
            ActivityCompat.requestPermissions(
                    activity,
                    new String[]{ permission },
                    0
            );
        }
    }
}