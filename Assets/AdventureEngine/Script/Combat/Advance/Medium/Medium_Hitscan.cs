using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Medium_Hitscan : Medium {

        public override void Ini(Card S, Card T)
        {
            base.Ini(S, T);
            SetKey("SourcePositionX", S.GetPosition().x);
            SetKey("SourcePositionY", S.GetPosition().y);
        }

        public override void EffectUpdate(float Value)
        {
            ChangeKey("Delay", -Value);
            if (GetKey("Delay") <= 0)
            {
                Vector2 O = new Vector2(GetKey("SourcePositionX"), GetKey("SourcePositionY"));
                Vector2 T = new Vector2(GetKey("PositionX"), GetKey("PositionY"));
                T = O + (T - O).normalized * GetKey("Range");
                HitEffect(O, T, out Vector2 Contact, out Card Tar, out _);
                SetKey("PositionX", Contact.x);
                SetKey("PositionY", Contact.y);
                Effect(Tar);
                EndEffect();
            }
        }

        public void HitEffect(Vector2 O, Vector2 T, out Vector2 Contact, out Card Target, out bool Hit)
        {
            if ((T - O).magnitude <= 0.0001f)
                T += new Vector2(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f));
            List<Card> All = new List<Card>();
            for (int i = CombatControl.Main.Cards.Count - 1; i >= 0; i--)
            {
                Card C = CombatControl.Main.Cards[i];
                if (!C || !C.CardActive())
                    continue;
                if (C.GetSide() == Source.GetSide() && GetKey("TargetFriendly") <= 0)
                    continue;
                if (C.GetSide() != Source.GetSide() && GetKey("TargetEnemies") <= 0)
                    continue;
                if (C == Source && GetKey("IgnoreSource") > 0)
                    continue;
                All.Add(C);
            }
            bool CanHit = PathControl.Main.CanHit(O, T, out Vector2 TC);
            if (!CanHit)
                T = TC;
            Hit = !CanHit;
            List<Vector2> Contacts = new List<Vector2>();
            List<Card> ContactCards = new List<Card>();
            for (int i = All.Count - 1; i >= 0; i--)
            {
                Card C = All[i];
                if (C.LineCollisionCheck(O, T, out Vector2 TempContact))
                {
                    Contacts.Add(TempContact);
                    ContactCards.Add(C);
                }
            }
            if (ContactCards.Count <= 0)
            {
                Contact = T;
                Target = null;
                return;
            }
            else if (ContactCards.Count == 1)
            {
                Hit = true;
                Contact = Contacts[0];
                Target = ContactCards[0];
                return;
            }
            else
            {
                float D = Mathf.Infinity;
                int I = 0;
                for (int i = 0; i < ContactCards.Count; i++)
                {
                    if ((Contacts[i] - O).magnitude < D)
                    {
                        D = (Contacts[i] - O).magnitude;
                        I = i;
                    }
                }
                Hit = true;
                Contact = Contacts[I];
                Target = ContactCards[I];
                return;
            }
        }

        public override void CommonKeys()
        {
            // "Range": Max range of the hitscan
            // "SourcePositionX": Source position X when the medium is created
            // "SourcePositionY": Source position Y when the medium is created
            // "TargetEnemies": Whether the medium should target enemies
            // "TargetFriendly": Whether the medium should target friendly
            // "IgnoreSource": Whether the medium should exclude the source
            // "IgnoreTerrain": Whether the medium should ignore terrain
            base.CommonKeys();
        }
    }
}