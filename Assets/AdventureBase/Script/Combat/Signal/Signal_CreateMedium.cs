using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_CreateMedium : Signal {
        public GameObject MediumPrefab;
        public List<string> InheritKeys;

        public override void EndEffect()
        {
            GameObject G = Instantiate(MediumPrefab);
            Medium M = G.GetComponent<Medium>();
            foreach (string s in InheritKeys)
            {
                if (HasKey(s))
                    M.SetKey(s, GetKey(s));
            }
            M.Ini(Source, Target);
            base.EndEffect();
        }
    }
}