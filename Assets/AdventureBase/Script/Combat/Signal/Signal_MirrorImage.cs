using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_MirrorImage : Signal {
        public GameObject CardPrefab;

        public override void EndEffect()
        {
            GameObject G = Instantiate(CardPrefab);
            Card C = G.GetComponent<Card>();
            C.SourceCard = Source;
            C.SetPosition(Source.GetPosition() + Random.Range(GetKey("MinRange"), GetKey("MaxRange")) * Source.GetDirection().normalized);
            C.Side = Source.GetSide();
            C.SetMaxLife((Source.GetMaxLife() - Source.GetLife()));
            C.Life = C.MaxLife;
            C.SetDirection(Source.GetDirection());
            CombatControl.Main.AddCard(C);
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "MinRange": Minimum random position
            // "MaxRange": Maximum random position
            base.CommonKeys();
        }
    }
}