using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenRotation : Tween<Vector3>
{
    protected override void UpdateProperty(float progress)
    {
        this.transform.eulerAngles = Vector3.Lerp(from, to, progress);
    }
}