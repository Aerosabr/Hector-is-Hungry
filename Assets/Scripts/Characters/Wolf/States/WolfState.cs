using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class WolfState : BaseState<WolfStateMachine.EWolfState>
{
    public WolfState(WolfStateMachine.EWolfState stateKey) : base(stateKey)
    {

    }

}
