using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class AIControlUnit_ChangePosition : AIControlUnit {
        public int Position;

        public override void Execute(CardGroup Source, bool Victory)
        {
            if (Position == 1)
            {
                if (Source.Side == 0)
                    Source.GetCurrentCard().GetAnim().ForcePosition(CombatControl.Main.GetFriendlyPosition(0) + new Vector2(0, 0.1f));
                else if (Source.Side == 1)
                    Source.GetCurrentCard().GetAnim().ForcePosition(CombatControl.Main.GetEnemyPosition(0) + new Vector2(0, 0.1f));
            }
            else if (Position == 2)
            {
                if (Source.Side == 0)
                    Source.GetCurrentCard().GetAnim().ForcePosition(CombatControl.Main.GetFriendlyPosition(1) + new Vector2(0, 0.1f));
                else if (Source.Side == 1)
                    Source.GetCurrentCard().GetAnim().ForcePosition(CombatControl.Main.GetEnemyPosition(1) + new Vector2(0, 0.1f));
            }
            else if (Position == 3)
            {
                if (Source.Side == 0)
                    Source.GetCurrentCard().GetAnim().ForcePosition(CombatControl.Main.GetFriendlyPosition(2) - new Vector2(0, 0.1f));
                else if (Source.Side == 1)
                    Source.GetCurrentCard().GetAnim().ForcePosition(CombatControl.Main.GetEnemyPosition(2) - new Vector2(0, 0.1f));
            }
            base.Execute(Source, Victory);
        }
    }
}