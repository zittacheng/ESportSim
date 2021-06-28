using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_RandomTrigger : Signal {
        public List<GameObject> PositiveSignals;
        public List<GameObject> NegativeSignals;
        [HideInInspector] public List<string> WaitingKeys;

        public override void StartEffect()
        {
            SetKey("RandomRoll", Random.Range(0.001f, 0.999f));
            base.StartEffect();
        }

        public override void EndEffect()
        {
            if (GetKey("RandomRoll") <= GetKey("Chance"))
            {
                foreach (GameObject G in PositiveSignals)
                    Source.SendSignal(G, WaitingKeys, Target);
            }
            else
            {
                foreach (GameObject G in NegativeSignals)
                    Source.SendSignal(G, WaitingKeys, Target);
            }
            base.EndEffect();
        }

        public override void IniAddKeys(List<string> Keys)
        {
            WaitingKeys = Keys;
        }

        public override void CommonKeys()
        {
            // "Chance": Chance to trigger PositiveSignals
            // "CurrentRoll": Current random roll
            base.CommonKeys();
        }
    }
}