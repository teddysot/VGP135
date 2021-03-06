﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class ScreenManager : MonoBehaviour
{
    private Canvas rootCanvas = null;

    private Stack<GameObject> screenStack = new Stack<GameObject>();

    // Container to hold all of our instances of screens by name
    private Dictionary<string, Stack<GameObject>> screenInstances 
        = new Dictionary<string, Stack<GameObject>>();

    private static ScreenManager instance = null;
    public static ScreenManager Instance()
    {
        return instance;
    }

    private void Awake()
    {
        rootCanvas = this.GetComponent<Canvas>();
        if (instance == null)
        {
            // Assign my instance
            instance = this;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }
    
    public GameObject PushScreen(string screenName)
    {
        GameObject screenInstance = null;

        Stack<GameObject> pool = null;
        // Attempt to retrieve gameobject pool
        if (screenInstances.TryGetValue(screenName, out pool) == false)
        {
            // Ensure there is always an instance of gameobject pool
            pool = new Stack<GameObject>(4);
            screenInstances.Add(screenName, pool);
        }


        if (pool.Count > 0)
        {
            // Retrieve from pool
            screenInstance = pool.Pop();
        }
        else
        {
            // Create new resource
            GameObject screenPrefab = Resources.Load(screenName) as GameObject;
            if (screenPrefab != null)
            {
                screenInstance = GameObject.Instantiate(screenPrefab, rootCanvas.transform) as GameObject;
                screenInstance.name = screenName;
            }
        }

        screenStack.Push(screenInstance);

        // Retrieve all tweens and reset them
        TweenBase[] tweens = screenInstance.GetComponents<TweenBase>();
        for (int i = 0; i < tweens.Length; ++i)
        {
            tweens[i].ResetToBeginning();
            tweens[i].Play();
        }

        screenInstance.gameObject.SetActive(true);

        return screenInstance;
    }

    public void PopScreen()
    {
        if (screenStack.Count > 0)
        {
            GameObject screenInstance = screenStack.Pop();


            // Retrieve all tweens and reset them
            TweenBase[] tweens = screenInstance.GetComponents<TweenBase>();
            int pendingTweens = tweens.Length;
            for (int i = 0; i < tweens.Length; ++i)
            {
                // Listen to tweens for completion
                tweens[i].RegisterActionOnComplete(() => {
                    --pendingTweens;

                    // When all screens are done ...
                    if (pendingTweens == 0)
                    {
                        string screenName = screenInstance.name;
                        Stack<GameObject> pool = null;
                        // Attempt to retrieve gameobject pool
                        if (screenInstances.TryGetValue(screenName, out pool) == false)
                        {
                            // Ensure there is always an instance of gameobject pool
                            pool = new Stack<GameObject>(4);
                            screenInstances.Add(screenName, pool);
                        }

                        pool.Push(screenInstance);
                        screenInstance.gameObject.SetActive(false);
                    }
                });
                tweens[i].ResetToEnd();
                tweens[i].Play();
            }
        }
    }
}