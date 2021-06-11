using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_AoeCircle : Signal {
        public GameObject EffectPrefab;
        public Color EffectColor;

        public override void EndEffect()
        {
            EffectActive(new Vector2(GetKey("TargetPositionX"), GetKey("TargetPositionY")));
            base.EndEffect();
        }

        public void EffectActive(Vector2 Position)
        {
            GameObject G = Instantiate(EffectPrefab);
            AoeCircle AC = G.GetComponent<AoeCircle>();
            AC.Position = Position;
            AC.MainColor = EffectColor;
            AC.AddDelay = GetKey("AddDelay");
            AC.Size = GetKey("Size");
            AC.Ini();
        }

        public override void CommonKeys()
        {
            // "Size": Size of aoe effect
            // "AddDelay": Aoe effect delay
            base.CommonKeys();
        }
    }
}