using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_CreateMediums : Signal {
        public List<GameObject> MediumPrefabs;

        public override void EndEffect()
        {
            foreach (GameObject MediumPrefab in MediumPrefabs)
            {
                GameObject G = Instantiate(MediumPrefab);
                Medium M = G.GetComponent<Medium>();
                M.Ini(Source, Target);
            }
            base.EndEffect();
        }
    }
}