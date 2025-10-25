using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MushroomBossBaseState
{
   public abstract void EnterState(Boss_01_StateManager enemy);

   public abstract void UpdateState(Boss_01_StateManager enemy);

   public abstract void FixedUpdateState(Boss_01_StateManager enemy);
}
