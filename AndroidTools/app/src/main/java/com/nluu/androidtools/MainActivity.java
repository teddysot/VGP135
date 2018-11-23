package com.nluu.androidtools;

import android.Manifest;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;

import com.nluu.tools.Utility;

public class MainActivity extends AppCompatActivity {

    private Utility utilityInstance = null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        // Create utility instance given this app's application context
        utilityInstance = Utility.Create(this, getApplicationContext());

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
                utilityInstance.ShowNotification("Hello World", 0);
            }
        });

        Button showDelayedNotificationButton = findViewById(R.id.showDelayedNotificationButton);
        showDelayedNotificationButton.setOnClickListener(new View.OnClickListener(){
            public void onClick(View v) {
                utilityInstance.ShowNotification("Hello World",1000);
            }
        });
    }

    public void OnPermissionRequestButtonClicked(View v) {
        Log.d("Unity","Permission button pressed");
        utilityInstance.RequestPermission(Manifest.permission.READ_CONTACTS);

    }
}