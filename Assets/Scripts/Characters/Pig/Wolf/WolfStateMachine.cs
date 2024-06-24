using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfStateMachine : StateManager<WolfStateMachine.EWolfState>
{
    public enum EWolfState
    {
        Idle,
        Eat,
        Destroy,
		Stun,
    }

    public Wolf wolf;

	public void Awake()
	{
        InitializeStates();
	}

    private void InitializeStates()
    {
        States.Add(EWolfState.Idle, new WolfIdleState(this, wolf, EWolfState.Idle));
		States.Add(EWolfState.Eat, new WolfEatState(this, wolf, EWolfState.Eat));
		States.Add(EWolfState.Stun, new WolfStunState(this, wolf, EWolfState.Stun));
		States.Add(EWolfState.Destroy, new WolfDestroyState(this, wolf, EWolfState.Destroy));
        CurrentState = States[EWolfState.Idle];
	}

    public void ChangeState(EWolfState state)
    {
		if (States.ContainsKey(state))
		{
			TransitionToState(state);
		}
		else
		{
			Debug.LogError($"State {state} does not exist in the state machine.");
		}
	}
}
