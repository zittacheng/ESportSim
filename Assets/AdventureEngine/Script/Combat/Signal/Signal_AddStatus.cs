using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_AddStatus : Signal {
        public List<GameObject> StatusPrefabs;
        public List<string> InheritKeys;

        public override void EndEffect()
        {
            List<string> AddKeys = new List<string>();
            foreach (string s in InheritKeys)
            {
                if (HasKey(s))
                {
                    if ((s == "Damage" || s == "Heal") && GetKey("BaseScaling") != 0)
                        AddKeys.Add(KeyBase.Compose(s, GetKey(s) * Source.GetBaseDamage()));
                    else
                        AddKeys.Add(KeyBase.Compose(s, GetKey(s)));
                }
            }
            for (int i = 0; i < StatusPrefabs.Count; i++)
                Target.AddStatus(StatusPrefabs[i].GetComponent<Mark_Status>(), AddKeys);
        }
    }
}