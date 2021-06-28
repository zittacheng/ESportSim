using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class AIControlUnit_Random : AIControlUnit {
        public List<AIControlUnit> VictoryUnits;
        public List<AIControlUnit> DefeatUnits;

        public override void Execute(CardGroup Source, bool Victory)
        {
            if (Victory)
            {
                if (VictoryUnits.Count > 0)
                {
                    AIControlUnit ACU = VictoryUnits[Random.Range(0, VictoryUnits.Count)];
                    ACU.Execute(Source, Victory);
                }
            }
            else
            {
                if (DefeatUnits.Count > 0)
                {
                    AIControlUnit ACU = DefeatUnits[Random.Range(0, DefeatUnits.Count)];
                    ACU.Execute(Source, Victory);
                }
            }
            base.Execute(Source, Victory);
        }
    }
}