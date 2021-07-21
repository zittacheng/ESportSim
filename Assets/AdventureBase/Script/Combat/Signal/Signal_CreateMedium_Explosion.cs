using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_CreateMedium_Explosion : Signal {
        public GameObject MediumPrefab;
        public List<string> InheritKeys;

        public override void EndEffect()
        {
            GameObject G = Instantiate(MediumPrefab);
            Medium M = G.GetComponent<Medium>();
            if (GetKey("LockTarget") == 1 && Target)
            {
                M.SetKey("PositionX", Target.GetPosition().x);
                M.SetKey("PositionY", Target.GetPosition().y);
            }
            else if (GetKey("LockSource") == 1 && Source)
            {
                M.SetKey("PositionX", Source.GetPosition().x);
                M.SetKey("PositionY", Source.GetPosition().y);
            }
            else
            {
                M.SetKey("PositionX", GetKey("TargetPositionX"));
                M.SetKey("PositionY", GetKey("TargetPositionY"));
            }
            if (HasKey("Range"))
                M.SetKey("Range", GetKey("Range"));
            foreach (string s in InheritKeys)
            {
                if (HasKey(s))
                    M.SetKey(s, GetKey(s));
            }
            M.Ini(Source, Target);
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "Range": Range of the created Medium explosion
            // "LockTarget": Whether to follow the target's current position
            // "LockSource": Whether to follow the source's position instead of the target's
            base.CommonKeys();
        }
    }
}