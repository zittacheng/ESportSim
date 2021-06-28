using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_AddShield : Signal_AddStatus {

        public override void StartEffect()
        {
            if (HasKey("Shield"))
                SetKey("Shield", GetShieldValue());
            base.StartEffect();
        }

        public override void EndEffect()
        {
            List<string> AddKeys = new List<string>();
            foreach (string s in InheritKeys)
            {
                if (HasKey(s))
                {
                    if (s == "Shield")
                        AddKeys.Add(KeyBase.Compose(s, GetKey("Shield")));
                    else
                        AddKeys.Add(KeyBase.Compose(s, GetKey(s)));
                }
            }
            for (int i = 0; i < StatusPrefabs.Count; i++)
                Target.AddStatus(StatusPrefabs[i].GetComponent<Mark_Status>(), AddKeys);
        }

        public virtual float GetShieldValue()
        {
            float a = GetKey("Shield");
            if (HasKey("MaxLifeRate"))
                a += Source.GetMaxLife() * GetKey("MaxLifeRate");
            if (HasKey("LifeRate"))
                a += Source.GetLife() * GetKey("LifeRate");
            return a;
        }

        public override void CommonKeys()
        {
            // "Shield": Ini shield amount
            // "MaxLifeRate": Amount of max life add to shield
            // "LifeRate": Amount of life add to shield
            base.CommonKeys();
        }
    }
}