using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TweenPosition : Tween<Vector3> {

    protected override void UpdateProperty(float progress)
    {
        this.transform.position = Vector3.Lerp(from, to, progress);
    }
}
