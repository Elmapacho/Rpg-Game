using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElMapacho;

public abstract class AIState
{
    protected Fighter fighter;

    public AIState(Fighter fighterToControl)
    {
        fighter = fighterToControl;
    }

    public virtual void OnEnter()
    {
        
    }
        
    public virtual void OnUpdate()
    {

    }
    public virtual void OnExit()
    {

    }
}
