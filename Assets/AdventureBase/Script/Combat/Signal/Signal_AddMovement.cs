using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_AddMovement : Signal {
        public GameObject MovementPrefab;
        public List<string> InheritKeys;

        public override void EndEffect()
        {
            List<string> AddKeys = new List<string>();
            foreach (string s in InheritKeys)
                if (HasKey(s))
                    AddKeys.Add(KeyBase.Compose(s, GetKey(s)));
            if (HasKey("TargetPositionX"))
                AddKeys.Add(KeyBase.Compose("TargetPositionX", GetKey("TargetPositionX")));
            if (HasKey("TargetPositionY"))
                AddKeys.Add(KeyBase.Compose("TargetPositionY", GetKey("TargetPositionY")));
            Target.AddMovement(MovementPrefab.GetComponent<Movement>(), AddKeys);
            base.EndEffect();
        }
    }
}