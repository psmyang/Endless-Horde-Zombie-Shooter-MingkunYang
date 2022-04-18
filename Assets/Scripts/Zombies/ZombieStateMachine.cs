using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateMachine : MonoBehaviour
{
    public States currentState { get; private set; }
    protected Dictionary<ZombieStateType, States> states;
    bool isIdle = true;

    private void Awake()
    {
        states = new Dictionary<ZombieStateType, States>();
    }

    public void Initialize(ZombieStateType startingState)
    {
        if (states.ContainsKey(startingState))
        {
            ChangeState(startingState);
        }
    }

    public void AddState(ZombieStateType stateName, States state)
    {
        if (states.ContainsKey(stateName)) return;
        states.Add(stateName, state);
    }
    public void RemoveState(ZombieStateType stateName)
    {
        if (!states.ContainsKey(stateName)) return;
        states.Remove(stateName);
        
    }

    public void ChangeState(ZombieStateType nextState)
    {
        if (!isIdle)
        {
            //stop current state
            StopRunningState();
        }
        if (!states.ContainsKey(nextState)) return;

        currentState = states[nextState];
        currentState.Start();

        if (currentState.UpdateInterval > 0)
        {
            InvokeRepeating(nameof(IntervalUpdate), 0, currentState.UpdateInterval);
        }
        isIdle = false;
    }

    void StopRunningState()
    {
        isIdle = true;
        currentState.Exit();
        CancelInvoke(nameof(IntervalUpdate));
    }

    private void IntervalUpdate()
    {
        if (!isIdle)
        {
            currentState.IntervalUpdate();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isIdle)
        {
            currentState.Update();
        }
    }
}
