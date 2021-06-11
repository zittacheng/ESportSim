using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_CreateMedium_Hitscan : Signal {
        public GameObject MediumPrefab;

        public override void EndEffect()
        {
            GameObject G = Instantiate(MediumPrefab);
            Medium M = G.GetComponent<Medium>();
            M.SetKey("PositionX", GetKey("TargetPositionX"));
            M.SetKey("PositionY", GetKey("TargetPositionY"));
            if (HasKey("Range"))
                M.SetKey("Range", GetKey("Range"));
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