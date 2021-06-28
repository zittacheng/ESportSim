using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class AIControlUnit_SwitchHero : AIControlUnit {
        public string Target;

        public override void Execute(CardGroup Source, bool Victory)
        {
            if (Target == "")
                return;
            string s = Target;
            Source.SwitchCard(s);
            base.Execute(Source, Victory);
        }
    }
}