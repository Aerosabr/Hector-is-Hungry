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
    }
}
