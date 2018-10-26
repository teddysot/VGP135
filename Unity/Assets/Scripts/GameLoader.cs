using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    private GameSystems gameSystems = null;

    private void Start()
    {
        gameSystems = new GameSystems();

        //LoadSynchronously();
        LoadAsynchronously();
    }

    private void LoadAsynchronously()
    {
        // Run LoadAsyncRoutine
        this.StartCoroutine(LoadAsyncRoutine());
    }

    private IEnumerator LoadAsyncRoutine()
    {
        // Create a stop watch to see how long it takes for this to load
        Stopwatch watch = new Stopwatch();
        watch.Start();

        // Create and register screen manager to our game systems table
        // Load Screen Manager prefab from our assets dynamically
        GameObject screenManagerPrefab = Resources.Load("ScreenManager") as GameObject;

        // Instantiate an object of screen manager ( because we cannot new Monobehaviours )
        ScreenManager screenManager = GameObject.Instantiate(screenManagerPrefab).GetComponent<ScreenManager>();

        screenManager.PushScreen("LoadingScreen");

        // Counter for completed tasks
        int pendingManagers = 0;

        // Create and register audio manager to our game systems table
        AudioManager audioManager = new AudioManager();
        gameSystems.Register(audioManager);
        // Create task to initialize audio manager on separate thread
        pendingManagers += 1;
        Task.Run(() => {
            audioManager.Initialize();
        }).ContinueWith((Task t)=> {
            pendingManagers -= 1;
        });

        // Create and register achievement manager to our game systems table
        AchievementManager achievementManager = new AchievementManager();
        gameSystems.Register(achievementManager);
        pendingManagers += 1;
        Task.Run(() => {
            achievementManager.Initialize();
        }).ContinueWith( (Task t)=> {
            pendingManagers -= 1;
        });


        // Register screen manager to game systems
        gameSystems.Register(screenManager);


        // Wait for pending managers to finish loading
        while (pendingManagers != 0)
        {
            yield return null;
        }

        // Stop the watch and log out the time it took
        watch.Stop();
        UnityEngine.Debug.Log("Loading async took " + watch.ElapsedMilliseconds + "ms");

        // Boot into Landing screen
        screenManager.PopScreen();
        screenManager.PushScreen("LandingScreen");
        yield break;
    }

    private void LoadSynchronously()
    {
        // Create a stop watch to see how long it takes for this to load
        Stopwatch watch = new Stopwatch();
        watch.Start();
        
        // Create and register audio manager to our game systems table
        AudioManager audioManager = new AudioManager();
        gameSystems.Register(audioManager);
        audioManager.Initialize();

        // Create and register achievement manager to our game systems table
        AchievementManager achievementManager = new AchievementManager();
        gameSystems.Register(achievementManager);
        achievementManager.Initialize();


        // Create and register screen manager to our game systems table
        // Load Screen Manager prefab from our assets dynamically
        GameObject screenManagerPrefab = Resources.Load("ScreenManager") as GameObject;

        // Instantiate an object of screen manager ( because we cannot new Monobehaviours )
        ScreenManager screenManager = GameObject.Instantiate(screenManagerPrefab).GetComponent<ScreenManager>();

        // Register screen manager to game systems
        gameSystems.Register(screenManager);

        // Stop the watch and log out the time it took
        watch.Stop();
        UnityEngine.Debug.Log("Loading synchronously took " + watch.ElapsedMilliseconds + "ms");
    }
}
