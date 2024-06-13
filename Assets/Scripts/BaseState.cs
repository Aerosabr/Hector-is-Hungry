using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<EState> where EState : enum
{
	public BaseState(EState key)
	{
		StateKey = key;
	}

	public EState StateKey { get; private set; }
	public abstract void EnterState();
	public abstract void ExitState();
	public abstract void UpdateState();
	public abstract EState GetState();
	public abstract void OnTriggerEnter(Collider other);
	public abstract void OnTriggerStay(Collider other);
	public abstract void OnTriggerExit(Collider other);
	
}
