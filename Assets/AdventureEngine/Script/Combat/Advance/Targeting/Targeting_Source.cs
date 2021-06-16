using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Targeting_Source : Targeting {

        public override Card FindTarget(Card Source)
        {
            if (!Source)
                return null;
            Card C = Source.GetTarget();
            if (!C || !C.CardActive() || !Source || !Source.CardActive())
                return null;
            if (HasKey("Range") && (C.GetPosition() - Source.GetPosition()).magnitude > GetKey("Range"))
                return null;
            if (GetKey("CanSee") > 0 && !PathControl.Main.CanSee(Source.GetPosition(), C.GetPosition()))
                return null;
            else if (C == Source && (GetKey("TargetSelf") > 0 || !HasKey("TargetSelf")))
                return C;
            else if (C.GetSide() == Source.GetSide() && (GetKey("TargetFriendly") > 0 || !HasKey("TargetFriendly")))
                return C;
            else if (C.GetSide() != Source.GetSide() && (GetKey("TargetEnemies") > 0 || !HasKey("TargetEnemies")))
                return C;
            else if (!C.CombatActive() && (GetKey("TargetNonCombat") > 0 || !HasKey("TargetNonCombat")))
                return C;
            return null;
        }

        public override bool CheckTarget(Card Source, Card Target)
        {
            return base.CheckTarget(Source, Target) && Source && Source.GetTarget() == Target;
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