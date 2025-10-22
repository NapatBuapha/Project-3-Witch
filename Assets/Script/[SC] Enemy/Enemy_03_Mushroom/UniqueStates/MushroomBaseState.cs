using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MushroomBaseState
{
   public abstract void EnterState(Enemy_03_StateManager enemy);

   public abstract void UpdateState(Enemy_03_StateManager enemy);

   public abstract void FixedUpdateState(Enemy_03_StateManager enemy);
}
