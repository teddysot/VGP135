﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadLoop : MonoBehaviour
{
    public void Start()
    {
        Vector3 v1 = Vector3.up;
        Vector3 v2 = Vector3.right;

        // Add 2 vectors together a million times
        for (int i = 0; i < 10000000; ++i)
        {
            AddVector(v1, v2);
        }
    }

    private Vector3 AddVector(Vector3 v1, Vector3 v2)
    {
        return v1 + v2;
    }
}