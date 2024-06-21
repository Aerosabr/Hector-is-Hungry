using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class PigStateMachine : StateManager<PigStateMachine.EPigState>
{
	public enum EPigState
	{
		Idle,
		Roam,
		Follow,
		Transport,
		Home,
	}

	public Pig pig;

	public void Awake()
	{
		InitializeStates();
	}

	private void InitializeStates()
	{
		States.Add(EPigState.Idle, new PigIdleState(this, pig, EPigState.Idle));
		States.Add(EPigState.Roam, new PigRoamState(this, pig, EPigState.Roam));
		States.Add(EPigState.Follow, new PigFollowState(this, pig, EPigState.Follow));
		States.Add(EPigState.Transport, new PigTransportState(this, pig, EPigState.Transport));
		States.Add(EPigState.Home, new PigHomeState(this, pig, EPigState.Home));
		CurrentState = States[EPigState.Idle];
	}

	public void ChangeState(EPigState state)
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
