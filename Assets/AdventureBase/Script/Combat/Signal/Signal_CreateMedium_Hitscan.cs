using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_CreateMedium_Hitscan : Signal {
        public GameObject MediumPrefab;
        public List<string> InheritKeys;

        public override void EndEffect()
        {
            GameObject G = Instantiate(MediumPrefab);
            Medium M = G.GetComponent<Medium>();
            M.SetKey("PositionX", GetKey("TargetPositionX"));
            M.SetKey("PositionY", GetKey("TargetPositionY"));
            foreach (string s in InheritKeys)
            {
                if (HasKey(s))
                    M.SetKey(s, GetKey(s));
            }
            M.Ini(Source, null);
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "Range": Max range of the hitscan
            base.CommonKeys();
        }
    }
}