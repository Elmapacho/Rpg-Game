using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElMapacho
{
	public class IdleState : AIState
	{
        public IdleState(Fighter fighterToControl) : base(fighterToControl)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {

        }
    }
}
