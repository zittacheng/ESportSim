using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_CreateMedium : Signal {
        public GameObject MediumPrefab;

        public override void EndEffect()
        {
            GameObject G = Instantiate(MediumPrefab);
            Medium M = G.GetComponent<Medium>();
            M.Ini(Source, Target);
            base.EndEffect();
        }
    }
}