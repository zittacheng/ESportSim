using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class AIControlUnit_RolePass : AIControlUnit {
        public string RequiredKey;
        public AIControlUnit AddUnit;

        public override void Execute(CardGroup Source, bool Victory)
        {
            if (Source.GetCurrentCard().GetKey(RequiredKey) == 1)
            {
                if (AddUnit)
                    AddUnit.Execute(Source, Victory);
            }
            base.Execute(Source, Victory);
        }
    }
}