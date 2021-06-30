using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class AIControlUnit_RandomSwitch : AIControlUnit {
        public float Chance;
        public List<string> Keys;

        public override void Execute(CardGroup Source, bool Victory)
        {
            if (Random.Range(0.01f, 0.99f) <= Chance)
            {
                if (Source.GetAIControl() && Source.GetAIControl().GetComponent<AIControl_CoinBased>())
                {
                    AIControl_CoinBased AC = (AIControl_CoinBased)Source.GetAIControl();
                    List<string> NewKeys = new List<string>();
                    foreach (string s in Keys)
                        if (Source.CanSwitch(s))
                            NewKeys.Add(s);
                    AC.Switch(NewKeys[Random.Range(0, NewKeys.Count)]);
                }
            }
            base.Execute(Source, Victory);
        }
    }
}