using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class WolfState : BaseState<WolfStateMachine.EWolfState>
{
    protected Wolf Wolf;
    protected WolfStateMachine StateMachine;

    public WolfState(WolfStateMachine stateMachine, Wolf wolf, WolfStateMachine.EWolfState stateKey) : base(stateKey)
    {
        StateMachine = stateMachine;
        Wolf = wolf;
    }

}
