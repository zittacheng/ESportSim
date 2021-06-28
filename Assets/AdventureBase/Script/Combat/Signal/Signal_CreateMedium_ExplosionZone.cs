using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_CreateMedium_ExplosionZone : Signal {
        public GameObject MediumPrefab;
        public List<string> InheritKeys;

        public override void EndEffect()
        {
            GameObject G = Instantiate(MediumPrefab);
            Medium M = G.GetComponent<Medium>();
            M.Ini(Source, Target);
            M.SetKey("PositionX", GetKey("TargetPositionX"));
            M.SetKey("PositionY", GetKey("TargetPositionY"));
            M.SetKey("Range", GetKey("Range"));
            M.SetKey("Duration", GetKey("Duration"));
            foreach (string s in InheritKeys)
            {
                if (HasKey(s))
                    M.SetKey(s, GetKey(s));
            }
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "Duration": Duration of the created explosion zone
            base.CommonKeys();
        }
    }
}