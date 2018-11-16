package com.nluu.androidtools;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import com.nluu.tools.Utility;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        // Create utility instance given this app's application context
        final Utility utilityInstance = Utility.Create(getApplicationContext());

        // Get the debug button by id
        Button helloWorldButton =  findViewById(R.id.helloWorldButton);
        helloWorldButton.setOnClickListener(new View.OnClickListener(){
            public void onClick(View v){
                Utility.HelloWorldStatic();
            }
        });

        Button showToastButton = findViewById(R.id.showToastButton);
        showToastButton.setOnClickListener(new View.OnClickListener(){
            public void onClick(View v) {
                utilityInstance.ShowToastMessage("This is my toast");
            }
        });

        Button showNotificationButton = findViewById(R.id.showNotificationButton);
        showNotificationButton.setOnClickListener(new View.OnClickListener(){
            public void onClick(View v) {
                utilityInstance.ShowNotification("This is my Notification", 0);
            }
        });

        Button showDelayedNotificationButton = findViewById(R.id.showDelayedNotificationButton);
        showDelayedNotificationButton.setOnClickListener(new View.OnClickListener(){
            public void onClick(View v) {
                utilityInstance.ShowNotification("This is my Delayed Notification", 5000);
            }
        });
    }
}
