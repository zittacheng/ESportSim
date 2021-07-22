using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Medium : Mark {
        [HideInInspector] public Card Target;
        public List<string> InheritKeys;
        public List<GameObject> Signals;

        public virtual void Ini(Card S, Card T)
        {
            Source = S;
            Target = T;
            if (Target && GetKey("IgnoreTargetPosition") == 0)
            {
                SetKey("PositionX", Target.GetPosition().x);
                SetKey("PositionY", Target.GetPosition().y);
            }
            CombatControl.Main.AddMedium(this);
        }

        public override void Update()
        {
            TimePassed(CombatControl.Main.CombatTime());
            base.Update();
        }

        public override void TimePassed(float Value)
        {
            if (GetKey("AlreadyDead") != 1 && Source)
                EffectUpdate(Value);
            else
                InterruptEffect();
            base.TimePassed(Value);
        }

        public virtual void EffectUpdate(float Value)
        {

        }

        public virtual void InterruptEffect()
        {

        }

        public virtual void Effect(Card Target)
        {
            if (!Source || !Source.CardActive())
                return;
            List<string> AddKeys = new List<string>();
            AddKeys.Add(KeyBase.Compose("TargetPositionX", GetKey("PositionX")));
            AddKeys.Add(KeyBase.Compose("TargetPositionY", GetKey("PositionY")));
            foreach (string s in InheritKeys)
            {
                if (HasKey(s))
                    AddKeys.Add(KeyBase.Compose(s, GetKey(s)));
            }
            foreach (GameObject G in Signals)
                Source.SendSignal(G, AddKeys, Target);
        }

        public virtual void EndEffect()
        {
            if (GetKey("AlreadyDead") != 0)
                return;
            SetKey("AlreadyDead", 1);
            CombatControl.Main.RemoveMedium(this);
            Destroy(gameObject, 5);
        }

        public override void CommonKeys()
        {
            // "PositionX": Target position X
            // "PositionY": Target position Y
            // "AlreadyDead": Whether the effect has ended
            base.CommonKeys();
        }
    }
}