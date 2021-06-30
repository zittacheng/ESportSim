using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class AIControlUnit_RolePass : AIControlUnit {
        public int RequiredRole;
        public AIControlUnit AddUnit;

        public override void Execute(CardGroup Source, bool Victory)
        {
            if (Source.GetCurrentCard().GetKey("Role") == RequiredRole)
            {
                if (AddUnit)
                    AddUnit.Execute(Source, Victory);
            }
            base.Execute(Source, Victory);
        }
    }
}