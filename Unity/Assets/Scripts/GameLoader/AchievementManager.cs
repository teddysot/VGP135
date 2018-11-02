using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager
{
    public void Initialize()
    {
        Vector3 v1 = Vector3.up;
        Vector3 v2 = Vector3.right;

        // Add 2 vectors together a million times
        for (int i = 0; i < 100000000; ++i)
        {
            AddVector(ref v1, ref v2);
        }
    }

    private Vector3 AddVector(ref Vector3 v1, ref Vector3 v2)
    {
        return v1 + v2;
    }
}
