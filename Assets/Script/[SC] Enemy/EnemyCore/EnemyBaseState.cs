using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState
{
   public abstract void EnterState(BaseEnemyStateManager enemy);

   public abstract void UpdateState(BaseEnemyStateManager enemy);

   public abstract void FixedUpdateState(BaseEnemyStateManager enemy);
}
