using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class TweenBase : MonoBehaviour
{
    public virtual void ResetToBeginning()
    {

    }
}

abstract public class Tween<T> : TweenBase
{
    [SerializeField]
    protected T from = default(T);
    [SerializeField]
    protected T to = default(T);
    [SerializeField]
    protected float duration = 1.0f;
    [SerializeField]
    protected bool loop = false;
    [SerializeField]
    protected bool resetOnEnable = true;

    protected float timer = 0.0f;

    private void OnEnable()
    {
        if (resetOnEnable)
        {
            timer = 0.0f;
        }
    }

    [ContextMenu("Execute")]
    private void Execute()
    {
        timer = 0.0f;
    }

    private void Update()
    {
        // Update while timer is less than total duration
        if (timer < duration)
        {
            // Add in time 
            timer += Time.deltaTime;

            UpdateProperty(timer / duration);

            if (timer >= duration && loop)
            {
                timer = 0.0f;
            }
        }
    }

    protected virtual void UpdateProperty(float progress) // Progress between 0 and 1.00f
    {

    }

    public override void ResetToBeginning()
    {
        UpdateProperty(0.0f);
    }
}