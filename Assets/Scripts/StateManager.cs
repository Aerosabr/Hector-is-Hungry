using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();

    protected BaseState<EState> CurrentState;

    protected bool IsTransitioningState = false;

    void Start() 
    {
        CurrentState.EnterState();
    }
    void Update() 
    {
        EState nextStateKey = CurrentState.GetState();
        if(!IsTransitioningState && nextStateKey.Equals(CurrentState.StateKey))
        {
            CurrentState.UpdateState();
        }
        else
        {
            TransitionToState(nextStateKey);
        }
    }
    public void TransitionToState(EState statekey)
    {
        Debug.Log(statekey.ToString());
        IsTransitioningState = true;
        CurrentState.ExitState();
        CurrentState = States[statekey];
        CurrentState.EnterState();
        IsTransitioningState = false;
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        CurrentState.OnTriggerEnter2D(other);
    }
    void OnTriggerStay2D(Collider2D other)
	{
		CurrentState.OnTriggerStay2D(other);
	}
	void OnTriggerExit2D(Collider2D other)
	{
		CurrentState.OnTriggerExit2D(other);
	}

}
