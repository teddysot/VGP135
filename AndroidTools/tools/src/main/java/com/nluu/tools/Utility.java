package com.nluu.tools;

import android.app.AlarmManager;
import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Build;
import android.util.Log;
import android.widget.Toast;

import static android.app.Notification.DEFAULT_SOUND;
import static android.app.Notification.DEFAULT_VIBRATE;
import static android.app.Notification.DEFAULT_LIGHTS;


public class Utility extends BroadcastReceiver {

    // private constructor so nothing can create this object without us knowing
    public Utility(){}

    // static reference to object- there should only be one
    private static Utility instance = null;

    public static Utility Create(Context applicationContext) {
        instance = new Utility();
        instance.applicationContext = applicationContext;
        return instance;
    }

    private Context applicationContext = null;

    public static void HelloWorldStatic(){
        Log.d("Unity", "Hello World");
    }

    public void ShowToastMessage( String message)
    {
        Toast.makeText(applicationContext, message, Toast.LENGTH_SHORT).show();
    }

    public void ShowNotification(String message, int delayInMilliseconds ) {
        NotificationManager notificationManager =
                (NotificationManager) applicationContext.getSystemService(Context.NOTIFICATION_SERVICE);

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
                .setDefaults(DEFAULT_SOUND| DEFAULT_VIBRATE | DEFAULT_LIGHTS)
                .setVibrate(new long[]{0, 1000});

        // Give the notifications data to notification manager
        if (delayInMilliseconds == 0)
        {
            notificationManager.notify((int)System.currentTimeMillis(), notificationBuilder.build());
        }
        else {
            AlarmManager alarmManager = (AlarmManager) applicationContext.getSystemService(Context.ALARM_SERVICE);

            // Build intent data object to be used in the future
            Intent intent = new Intent(applicationContext, Utility.class);
            // Store the notification as data
            intent.putExtra("notificationData", notificationBuilder.build());

            // Bind the Intent to a Pending Intent to be used for future use
            PendingIntent pendingIntent =
                    PendingIntent.getBroadcast(applicationContext, 0, intent, PendingIntent.FLAG_UPDATE_CURRENT);

            // Tell AlarmManager to raise this intent after milliseconds
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.KITKAT)
            {
                alarmManager.setExact(AlarmManager.RTC_WAKEUP, delayInMilliseconds, pendingIntent);
            }
            else
            {
                alarmManager.set(AlarmManager.RTC_WAKEUP, delayInMilliseconds, pendingIntent);
            }
        }
    }

    public void onReceive(Context context, Intent intent)
    {
        Log.d("Unity", "Broadcast Receiver for Utility was given at intent");
        NotificationManager notificationManager =
                (NotificationManager)context.getSystemService(Context.NOTIFICATION_SERVICE);

        Notification notification = intent.getParcelableExtra("notificationData");
        notificationManager.notify((int)System.currentTimeMillis(), notification);
    }
}
