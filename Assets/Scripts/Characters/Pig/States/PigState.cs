using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PigState : BaseState<PigStateMachine.EPigState>
{
	protected Pig Pig;
	protected PigStateMachine StateMachine;

	public PigState(PigStateMachine stateMachine, Pig pig, PigStateMachine.EPigState stateKey) : base(stateKey)
	{
		StateMachine = stateMachine;
		Pig = pig;
	}
}
