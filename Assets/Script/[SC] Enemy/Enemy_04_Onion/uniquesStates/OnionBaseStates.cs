using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class OnionBaseStates
{
    public abstract void EnterState(Enemy_04_StateManager enemy);

   public abstract void UpdateState(Enemy_04_StateManager enemy);

   public abstract void FixedUpdateState(Enemy_04_StateManager enemy);
}
