using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Targeting_Selection : Targeting {

        public override Card FindTarget(Card Source)
        {
            Card C = CombatControl.Main.SelectingCard;
            if (!C || !C.CardActive() || !Source || !Source.CardActive())
                return null;
            if (HasKey("Range") && (C.GetPosition() - Source.GetPosition()).magnitude > GetKey("Range"))
                return null;
            if (GetKey("CanSee") > 0 && !MapControl.Main.CanSee(Source.GetPosition(), C.GetPosition()))
                return null;
            else if (C == Source && GetKey("TargetSelf") > 0)
                return C;
            else if (C.GetSide() == Source.GetSide() && GetKey("TargetFriendly") > 0)
                return C;
            else if (C.GetSide() != Source.GetSide() && GetKey("TargetEnemies") > 0)
                return C;
            else if (!C.CombatActive() && GetKey("TargetNonCombat") > 0)
                return C;
            return null;
        }

        public override void CommonKeys()
        {
            // "TargetSelf": Whether the skill can target source
            // "TargetFriendly": Whether the skill can target friendly
            // "TargetEnemies": Whether the skill can target enemies
            // "TargetNonCombat": Whether the skill can target non-combat active cards
            // "Range": Max targeting range
            // "CanSee": Whether target need to be seen by source
            base.CommonKeys();
        }
    }
}