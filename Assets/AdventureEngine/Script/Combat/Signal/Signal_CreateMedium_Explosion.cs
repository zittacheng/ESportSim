﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_CreateMedium_Explosion : Signal {
        public GameObject MediumPrefab;

        public override void EndEffect()
        {
            GameObject G = Instantiate(MediumPrefab);
            Medium M = G.GetComponent<Medium>();
            if (GetKey("LockTarget") != 0 && Target)
            {
                M.SetKey("PositionX", Target.GetPosition().x);
                M.SetKey("PositionY", Target.GetPosition().y);
            }
            else
            {
                M.SetKey("PositionX", GetKey("TargetPositionX"));
                M.SetKey("PositionY", GetKey("TargetPositionY"));
            }
            if (HasKey("Range"))
                M.SetKey("Range", GetKey("Range"));
            M.Ini(Source, Target);
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "Range": Range of the created Medium explosion
            // "LockTarget": Whether follow the target's current position
            base.CommonKeys();
        }
    }
}