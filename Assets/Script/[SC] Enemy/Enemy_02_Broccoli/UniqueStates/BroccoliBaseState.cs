using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BroccoliBaseState
{
   public abstract void EnterState(Enemy_02_StateManager enemy);

   public abstract void UpdateState(Enemy_02_StateManager enemy);

   public abstract void FixedUpdateState(Enemy_02_StateManager enemy);
}
