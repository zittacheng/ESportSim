using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Damage_Both : Signal_Damage {
        public GameObject SignalPrefab;

        public override void EndEffect()
        {
            Source.ChangeLife(GetKey("Damage"));
            KeyBase KB = gameObject.AddComponent<KeyBase>();
            KB.Ini();
            KB.SetKey("Damage", GetKey("Damage"));
            //SendSignal(SignalPrefab, KB);
            base.EndEffect();
        }
    }
}