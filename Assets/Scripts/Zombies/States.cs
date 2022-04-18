using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States
{
    protected ZombieStateMachine zombieSM;

    public float UpdateInterval { get; protected set; } = 1.0f;

    protected States(ZombieStateMachine _zombieSM)
    {
        zombieSM = _zombieSM;
    }

    public virtual void Start()
    {

    }

    public virtual void IntervalUpdate()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}
